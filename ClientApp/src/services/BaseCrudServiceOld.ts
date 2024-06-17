import { notificar } from 'src/helpers/Notificacao';
import AxiosServiceOld from './AxiosServiceOld';
import {
  IBaseCrudResponse,
  IBaseCrudServiceOld,
  IBaseInativacao,
  IBaseRequest,
  IBaseResponse,
} from 'src/interfaces/IBaseCrudServiceOld';
import {
  IListarTodosRequest,
  IListarTodosResponse,
} from 'src/interfaces/IListarTodos';
import { IAxiosResultOld } from 'src/interfaces/IAxiosResultOld';
import { IQueryFiltro } from 'src/interfaces/components/IFiltro';
import { IBaseEntity } from 'src/interfaces/IBaseEntity';
import { IBaseInactivate } from 'src/components/tables/interfaces/IBaseInactivate';
import { IGetAllRequest } from 'src/components/tables/interfaces/IGetAllRequest';
import { IGetAllResponse } from 'src/components/tables/interfaces/IGetAllResponse';
import { notificate } from 'src/helpers/Notificate';
import { ITableFilter } from 'src/components/tables/interfaces/ITableFilter';

export default abstract class BaseCrudServiceOld<
  TBaseRequest extends IBaseRequest,
  TBaseResponse extends IBaseCrudResponse
> implements IBaseCrudServiceOld
{
  protected readonly url: string;
  protected readonly AxiosServiceOld = AxiosServiceOld;

  constructor(url: string) {
    this.url = url;
  }

  async ativar(id: number): Promise<boolean> {
    const url = `${this.url}/ativar/${id}`;
    const response = await this.AxiosServiceOld.patch(url);

    if (response.sucesso) {
      response.mensagem = 'Registro ativado com sucesso.';
      this.notificar(response);
    }

    return response.sucesso;
  }

  async atualizar(item: TBaseRequest): Promise<IBaseResponse | null> {
    const response = await this.AxiosServiceOld.put<
      TBaseRequest,
      TBaseResponse
    >(this.url, item);

    if (response.sucesso) {
      response.mensagem = 'Registro atualizado com sucesso.';
    }

    this.notificar(response);
    return response.objeto;
  }

  async clonar(id: number): Promise<IBaseResponse | null> {
    const url = `${this.url}/clone/${id}`;
    const response = await this.AxiosServiceOld.postNoBody<TBaseResponse>(url);

    if (response?.sucesso) {
      response.mensagem = 'Registro inserido com sucesso.';
      this.notificar(response);
      return response.objeto;
    }

    return null;
  }

  async excluir(id: number): Promise<boolean> {
    const url = `${this.url}/${id}`;
    const response = await this.AxiosServiceOld.delete(url);

    if (response.sucesso) response.mensagem = 'Registro excluído com sucesso.';

    this.notificar(response);

    return response.sucesso;
  }

  async exportar(
    filtros: Array<IQueryFiltro>,
    nomeArquivo: string
  ): Promise<void> {
    const url = `${this.url}/exportarexcel`;
    const response = await this.AxiosServiceOld.downloadPost<
      Array<IQueryFiltro>
    >(url, filtros);

    if (response.sucesso) {
      const urlDownload = window.URL.createObjectURL(response.objeto as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = `${nomeArquivo}.xlsx`;

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.mensagem = 'Exportação de dados realizada com sucesso.';
    }

    this.notificar(response);
  }

  async inativar(item: IBaseInativacao): Promise<boolean> {
    const url = `${this.url}/inativar`;

    const response = await this.AxiosServiceOld.postWithBody<
      IBaseInativacao,
      TBaseResponse
    >(url, item);

    if (response?.sucesso) {
      response.mensagem = 'Registro inativado com sucesso.';
      this.notificar(response);
      return response.sucesso;
    }

    return false;
  }

  async inserir(item: TBaseRequest): Promise<IBaseResponse | null> {
    const response = await this.AxiosServiceOld.postWithBody<
      TBaseRequest,
      TBaseResponse
    >(this.url, item);

    if (response?.sucesso) {
      response.mensagem = 'Registro inserido com sucesso.';
      this.notificar(response);
      return response.objeto;
    }

    return null;
  }

  async inserirViaImportacao(item: any, url?: string): Promise<void> {
    if (!url) {
      url = `${this.url}/InserirViaImportacao`;
    } else {
      url = `${this.url}/${url}`;
    }

    const formData = new FormData();
    formData.append('file', item.file);
    formData.append('inativarCadastroAnterior', item.inativarCadastroAnterior);

    const response = await this.AxiosServiceOld.postWithFormData(url, formData);

    if (response.sucesso) {
      const urlDownload = window.URL.createObjectURL(response.objeto as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'logimportacao.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.mensagem = 'Processamento finalizado, favor verificar os logs.';
      this.notificar(response);
    }
  }

  async obterInserirLayout(): Promise<void> {
    const url = `${this.url}/obterinserirlayout`;
    const response = await this.AxiosServiceOld.download(url);

    if (response.sucesso) {
      const urlDownload = window.URL.createObjectURL(response.objeto as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'layout.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.mensagem = 'Download do layout realizado com sucesso.';
      this.notificar(response);
    }
  }

  async obterPorId(id: number): Promise<TBaseResponse | null> {
    const url = `${this.url}/${id}`;
    const response = await this.AxiosServiceOld.get(url);

    if (response.sucesso) return response.objeto;

    this.notificar(response);

    return null;
  }

  async obterTodos(
    data: IListarTodosRequest
  ): Promise<IListarTodosResponse | null> {
    const url = `${this.url}/listartodos`;
    const response = await this.AxiosServiceOld.postWithBody(url, data);

    if (response?.sucesso) {
      return response.objeto;
    }

    return null;
  }

  protected notificar(response: IAxiosResultOld) {
    const type =
      response.status >= 400 && response.status <= 499
        ? 'warning'
        : response.status >= 500 && response.status <= 599
        ? 'negative'
        : 'positive';

    const timeout = type === 'negative' ? 0 : 7000;

    notificar(response.mensagem, type, false, timeout);
  }

  async activate(id: number): Promise<boolean> {
    const url = `${this.url}/activate/${id}`;
    const result = await this.AxiosServiceOld.patch(url);

    if (result.sucesso) {
      result.mensagem = 'Registro ativado com sucesso.';
      notificate(result.mensagem);
    }

    return result.sucesso;
  }

  async update(item: IBaseEntity): Promise<any> {
    const result = await this.AxiosServiceOld.put(this.url, item);

    if (result.sucesso) {
      result.mensagem = 'Registro atualizado com sucesso.';
      notificate(result.mensagem);
    }

    return result.objeto;
  }

  async delete(id: string): Promise<boolean> {
    const url = `${this.url}/${id}`;
    const result = await this.AxiosServiceOld.delete(url);

    if (result.sucesso) {
      result.mensagem = 'Registro excluído com sucesso.';
      notificate(result.mensagem);
    }

    return result.sucesso;
  }

  async export(filters: ITableFilter[], filename?: string): Promise<void> {
    const url = `${this.url}/export`;
    const response = await this.AxiosServiceOld.postDownload<
      Array<ITableFilter>
    >(url, filters);

    if (response.sucesso) {
      const urlDownload = window.URL.createObjectURL(response.objeto as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = `${filename || 'exportacao'}.xlsx`;

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.mensagem = 'Exportação de dados realizada com sucesso.';
      notificate(response.mensagem);
    }
  }

  async inactivate(item: IBaseInactivate): Promise<boolean> {
    const url = `${this.url}/inactivate`;
    const response = await this.AxiosServiceOld.patch(url, item);

    if (response.sucesso) {
      response.mensagem = 'Registro inativado com sucesso.';
      notificate(response.mensagem);
    }

    return response.sucesso;
  }

  async insert(item: any): Promise<any> {
    const result = await this.AxiosServiceOld.postWithBody(this.url, item);

    if (result?.sucesso) {
      result.mensagem = 'Registro inserido com sucesso.';
      notificate(result.mensagem);
    }

    return result?.objeto;
  }

  async getById(id: number): Promise<any> {
    const url = `${this.url}/${id}`;
    const result = await this.AxiosServiceOld.get(url);
    return result.objeto;
  }

  async getAll(data: IGetAllRequest): Promise<IGetAllResponse | null> {
    const url = `${this.url}/GetAllPaginated`;
    const response = await this.AxiosServiceOld.postWithBody(url, data);
    return response?.objeto;
  }
}
