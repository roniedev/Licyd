import { AxiosError } from 'axios';
import { IUser } from 'src/interfaces/IUser';
import { getAxiosLogin } from 'src/boot/axiosLogin';
import { notificate } from 'src/helpers/NotificationHelper';

import ILogin from 'src/interfaces/ILogin';
import useAutenticacaoStore from 'src/stores/autenticacao.store';

class AutenticacaoService {
  login = async (login: ILogin) => {
    try {
      const url = 'autenticacao/login';
      const apiAutenticacao = getAxiosLogin();
      const response = await apiAutenticacao.post(url, login);

      const useAutenticacao = useAutenticacaoStore();
      useAutenticacao.insert(response.data as IUser);

      return true;
    } catch (error: any) {
      tratarRetornoErro(error);
      return false;
    }
  };
}

function tratarRetornoErro(error: AxiosError | any) {
  const errorMessage = error.response?.data;
  if (Array.isArray(errorMessage)) {
    const mensagem = Array.from(errorMessage)
      .map((x) => x.Valor)
      .join(',');

    notificate(mensagem, 'warning', 'bottom-left', 7000, false);
    return;
  }

  notificate(errorMessage, 'negative', 'bottom-left', 0, false);
  return;
}

export default new AutenticacaoService();
