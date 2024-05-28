import { AxiosError } from 'axios';
import { IUser } from 'src/interfaces/IUser';
import { notificate } from 'src/boot/helpers/NotificationHelper';

import ILogin from 'src/interfaces/ILogin';
import useAutenticacaoStore from 'src/stores/auth-store';
import { getAxiosInstance } from 'src/boot/axios';

class AutenticacaoService {
  login = async (login: ILogin) => {
    try {
      const url = 'auth/authenticate';
      const api = getAxiosInstance();
      const response = await api.post(url, login);

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
