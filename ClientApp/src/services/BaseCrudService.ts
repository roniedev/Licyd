import { notificate } from 'src/boot/helpers/NotificationHelper';
import { ITableFilter } from 'src/components/tables/interfaces/ITableFilter';
import { IBaseCrudService } from 'src/interfaces/IBaseCrudService';
import { IBaseEntity } from 'src/interfaces/IBaseEntity';
import { IBaseInactivate } from 'src/interfaces/IBaseInactivate';
import { IGetAllRequest } from 'src/interfaces/IGetAllRequest';
import { IGetAllResponse } from 'src/interfaces/IGetAllResponse';
import AxiosService from 'src/services/AxiosService';

export abstract class BaseCrudService implements IBaseCrudService {
  protected readonly urlApi: string;
  protected readonly axiosService: AxiosService;

  constructor(urlApi: string) {
    this.urlApi = urlApi;
    this.axiosService = new AxiosService();
  }

  async activate(id: number): Promise<boolean> {
    const url = `${this.urlApi}/activate/${id}`;
    const result = await this.axiosService.patch(url);

    if (result.success) {
      result.message = 'Registro ativado com sucesso.';
      notificate(result.message);
    }

    return result.success;
  }

  async update(item: IBaseEntity): Promise<any> {
    const result = await this.axiosService.put(this.urlApi, item);

    if (result.success) {
      result.message = 'Registro atualizado com sucesso.';
      notificate(result.message);
    }

    return result.data;
  }

  async delete(id: string): Promise<boolean> {
    const url = `${this.urlApi}/${id}`;
    const result = await this.axiosService.delete(url);

    if (result.success) {
      result.message = 'Registro excluído com sucesso.';
      notificate(result.message);
    }

    return result.success;
  }

  async export(filters: ITableFilter[], filename?: string): Promise<void> {
    const url = `${this.urlApi}/export`;
    const response = await this.axiosService.postDownload<Array<ITableFilter>>(
      url,
      filters
    );

    if (response.success) {
      const urlDownload = window.URL.createObjectURL(response.data as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = `${filename || 'exportacao'}.xlsx`;

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.message = 'Exportação de dados realizada com sucesso.';
      notificate(response.message);
    }
  }

  async inactivate(item: IBaseInactivate): Promise<boolean> {
    const url = `${this.urlApi}/inactivate`;
    const response = await this.axiosService.patch(url, item);

    if (response.success) {
      response.message = 'Registro inativado com sucesso.';
      notificate(response.message);
    }

    return response.success;
  }

  async insert(item: any): Promise<any> {
    const result = await this.axiosService.post(this.urlApi, item);

    if (result.success) {
      result.message = 'Registro inserido com sucesso.';
      notificate(result.message);
    }

    return result.data;
  }

  async getById(id: number): Promise<any> {
    const url = `${this.urlApi}/${id}`;
    const result = await this.axiosService.get(url);
    return result.data;
  }

  async getAllPaginated(data: IGetAllRequest): Promise<IGetAllResponse | null> {
    const url = `${this.urlApi}/GetAllPaginated`;
    const response = await this.axiosService.post(url, data);
    return response.data;
  }

  async getAll(data: ITableFilter[] = []): Promise<any[]> {
    const url = `${this.urlApi}/GetAll`;
    const response = await this.axiosService.post(url, data);
    return response.data;
  }
}
