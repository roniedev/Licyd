import axios from 'axios';
import { hideLoading, showLoading } from 'src/helpers/Loading';
import { notificar } from 'src/helpers/Notificacao';
import useAutenticacaoStore from 'src/stores/autenticacao.store';

const api = axios.create({
  baseURL: 'https://localhost:7156/',
  //baseURL: 'https://cml0036piloto.teuto.com.br/api/',
  //baseURL: 'https://cml0036.teuto.com.br/api/',
});

api.interceptors.request.use((request) => {
  showLoading();

  const useAutenticacao = useAutenticacaoStore();
  const hasState = useAutenticacao.authenticated;

  if (hasState) {
    request.headers['X-Identificador-Aplicacao'] = 'CML0036';
    request.headers['Authorization'] = `Bearer ${useAutenticacao.token}`;
    request.headers['X-Nome-Usuario'] = useAutenticacao.user?.nome;
    request.headers['X-Usuario-Rede'] = useAutenticacao.username;
  }

  return request;
});

api.interceptors.response.use(
  (response) => {
    hideLoading();
    return response;
  },
  (error) => {
    hideLoading();

    if (error.message === 'Network Error') {
      notificar(
        'O servidor de aplicação não está online, favor entrar em contato com o DTI',
        'negative',
        false,
        10000,
        'top'
      );

      hideLoading();

      return error;
    }

    if (error.response.status === 401) {
      let retorno = [];
      const data = error.response.data;
      retorno = data;

      if (retorno instanceof ArrayBuffer) {
        const textDecoder = new TextDecoder('utf-8');
        const jsonString = textDecoder.decode(data);
        retorno = JSON.parse(jsonString);
      }

      const semAcessoNaAcao = retorno.some((item: any) => {
        return item.chave === 'SemAcessoNaAcao';
      });

      if (semAcessoNaAcao) {
        notificar(retorno[0].valor, 'warning', false, 10000, 'top');
      } else {
        const useAutenticacao = useAutenticacaoStore();
        useAutenticacao.delete();
        notificar(
          'Identificamos que o seu token expirou. Por favor, faça o login novamente para continuar.',
          'warning',
          false,
          10000,
          'top'
        );

        redirecionarLogin();

        return error;
      }
    } else if (error.response.status === 400 || error.response.status === 500) {
      let errors = '';
      let data;

      try {
        data = JSON.parse(new TextDecoder().decode(error.response?.data));
      } catch {
        data = error.response?.data;
      }

      data.forEach((item: { chave: string; valor: string }) => {
        errors += `${item.valor} <br />`;
      });

      notificar(
        errors,
        error.response.status === 400 ? 'warning' : 'negative',
        false,
        error.response.status === 400 ? 10000 : 0,
        'top'
      );
    }

    hideLoading();
    return error.response;
  }
);

export function obterUrlBase(): string {
  const portaHospedada = window.location.port ? `:${window.location.port}` : '';

  const complementoSistema =
    window.location.pathname &&
    process.env.APLICACAO_CODIGO &&
    window.location.pathname
      .toLowerCase()
      .includes(
        process.env.APLICACAO_CODIGO
          ? process.env.APLICACAO_CODIGO.toLowerCase()
          : ''
      )
      ? `/${window.location.pathname.replace(/\//g, '')}`
      : '';

  const urlSistema = `${window.location.protocol}//${window.location.hostname}${
    portaHospedada === '' ? complementoSistema : portaHospedada
  }`;

  return urlSistema;
}

export function redirecionarLogin() {
  window.location.href = obterUrlBase() + '/#/login';
}

export default api;
