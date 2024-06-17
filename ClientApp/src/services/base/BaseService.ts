import { AxiosError } from 'axios';
import { notificar } from 'src/helpers/Notificacao';
import { IAxiosResultOld } from 'src/interfaces/IAxiosResultOld';
import { IBaseService } from 'src/interfaces/base/IBaseService';

export default abstract class BaseService implements IBaseService {
  protected readonly url: string;

  constructor(url: string) {
    this.url = url;
  }

  obterErros = (error: AxiosError): string => {
    let errors = '';

    if (error.response?.status === 405) {
      errors += `Ocorreu um erro ao comunicar com o servidor: ${error.message}`;
    } else {
      const mensagens = JSON.parse(error.request.response);

      if (error.response?.status === 400) {
        if (mensagens[0].Chave && mensagens[0].Valor) {
          mensagens.forEach((item: { Chave: string; Valor: string }) => {
            errors += `${item.Valor}<br />`;
          });
        } else {
          errors += `Ocorreu um erro ao comunicar com o servidor: ${error.message}`;
        }
      } else {
        mensagens.forEach((item: { Chave: string; Valor: string }) => {
          errors += `${item.Chave}: ${item.Valor}<br />`;
        });
      }
    }

    return errors;
  };

  notificar = (response: IAxiosResultOld): void => {
    const type =
      response.status >= 400 && response.status <= 499
        ? 'warning'
        : response.status >= 500 && response.status <= 599
        ? 'negative'
        : 'positive';

    const timeout = type === 'negative' ? 0 : 7000;

    notificar(response.mensagem, type, false, timeout);
  };
}
