import { boot } from 'quasar/wrappers';
import axios, { AxiosInstance } from 'axios';
import { hideLoading, showLoading } from 'src/helpers/Loading';
import { notificate } from 'src/helpers/Notificate';
import useAutenticacaoStore from 'src/stores/autenticacao.store';

declare module '@vue/runtime-core' {
  interface ComponentCustomProperties {
    $axios: AxiosInstance;
    $api: AxiosInstance;
  }
}

const getAxiosInstance = (urlApi?: string): AxiosInstance => {
  const url = urlApi ? urlApi : process.env.API_URL;
  const api = axios.create({ baseURL: url });

  api.interceptors.request.use((request) => {
    showLoading();
    const useAutenticacao = useAutenticacaoStore();

    if (useAutenticacao.authenticated) {
      request.headers['X-Identificador-Aplicacao'] = 'CML0036';
      request.headers['Authorization'] = `Bearer ${useAutenticacao.token}`;
      request.headers['X-User-Name'] = useAutenticacao.username;
      request.headers['X-Usuario-Nome'] = useAutenticacao.user?.nome;
      request.headers['X-Usuario-Email'] = useAutenticacao.user?.email;
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
        notificate(
          'O servidor de aplicação está offline, favor entrar em contato com o suporte.',
          'negative',
          'top'
        );

        return error;
      }

      let data = [];
      data = error.response.data;

      if (data instanceof ArrayBuffer) {
        const textDecoder = new TextDecoder('utf-8');
        const jsonString = textDecoder.decode(data);
        data = JSON.parse(jsonString);
      }

      if (error.response.status === 401) {
        if (data) {
          const isAlterarSenha = data.some((item: any) => {
            return item.code === 'AlterarSenha';
          });

          if (isAlterarSenha) {
            changePasswordRedirect(data[0].descricao);
          } else {
            const isSemAcessoAction = data.some((item: any) => {
              return item.code === 'SemAcessoAction';
            });

            if (isSemAcessoAction) {
              notificate(data[0].message, 'warning', 'top', 7000);
            }
          }
        } else {
          useAutenticacaoStore().delete();
          redirecionarLogin();
        }

        return error;
      }

      if (error.response.status === 400 || error.response.status === 500) {
        let errors = '';

        data.forEach((item: { message: string }) => {
          errors += `${item.message} <br />`;
        });

        const tipoNotificacao =
          error.response.status === 500 ? 'negative' : 'warning';
        notificate(errors, tipoNotificacao, 'top', 10000, true);

        return error;
      }
    }
  );

  return api;
};

export function getUrlBase(): string {
  const porta = window.location.port ? `:${window.location.port}` : '';
  const pathname = window.location.pathname.replace(/\//g, '');
  const url = `${window.location.protocol}//${window.location.hostname}${
    porta ? porta : pathname
  }`;

  return url;
}

export function redirecionarLogin() {
  notificate(
    'O token de acesso expirou, faça login novamente.',
    'positive',
    'top',
    7000
  );
  window.location.href = getUrlBase() + '/#/login';
}

export function changePasswordRedirect(mensagem: string) {
  notificate(mensagem, 'positive', 'top', 7000);
  window.location.href = getUrlBase() + '/#/user/changepassword';
}

export default boot(({ app }) => {
  // for use inside Vue files (Options API) through this.$axios and this.$api

  app.config.globalProperties.$axios = axios;
  // ^ ^ ^ this will allow you to use this.$axios (for Vue Options API form)
  //       so you won't necessarily have to import axios in each vue file

  app.config.globalProperties.$api = getAxiosInstance();
  // ^ ^ ^ this will allow you to use this.$api (for Vue Options API form)
  //       so you can easily perform requests against your app's API
});

export { getAxiosInstance };
