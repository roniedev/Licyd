import { IGetAllRequest } from 'src/components/tables/interfaces/IGetAllRequest';
import { IGetAllResponse } from 'src/components/tables/interfaces/IGetAllResponse';
import { ITableFilter } from 'src/components/tables/interfaces/ITableFilter';
import { notificate } from 'src/helpers/NotificationHelper';
import { IBaseCrudService } from 'src/interfaces/IBaseCrudService';
import { IBaseEntity } from 'src/interfaces/IBaseEntity';
import { IBaseInactivate } from 'src/interfaces/IBaseInactivate';
import AxiosService from 'src/services/AxiosService';

export abstract class BaseCrudService implements IBaseCrudService {
  protected readonly urlApi: string;
  protected readonly axiosService: AxiosService;

  constructor(urlApi: string) {
    this.urlApi = urlApi;
    this.axiosService = new AxiosService();
  }

  async getUpdateLayout(): Promise<void> {
    const url = `${this.urlApi}/GetUpdateLayout`;
    const response = await this.axiosService.getDownload(url);

    if (response.success) {
      const urlDownload = window.URL.createObjectURL(response.data as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'layout-atualizar.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.message = 'Download do layout realizado com sucesso.';
      notificate(response.message);
    }
  }

  async getInactivateLayout(): Promise<void> {
    const url = `${this.urlApi}/GetInactivateLayout`;
    const response = await this.axiosService.getDownload(url);

    if (response.success) {
      const urlDownload = window.URL.createObjectURL(response.data as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'layout-inativar.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.message = 'Download do layout realizado com sucesso.';
      notificate(response.message);
    }
  }

  async getInsertLayout(): Promise<void> {
    const url = `${this.urlApi}/GetInsertLayout`;
    const response = await this.axiosService.getDownload(url);

    if (response.success) {
      const urlDownload = window.URL.createObjectURL(response.data as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'layout-inserir.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.message = 'Download do layout realizado com sucesso.';
      notificate(response.message);
    }
  }

  async getActivateLayout(): Promise<void> {
    const url = `${this.urlApi}/GetActivateLayout`;
    const response = await this.axiosService.getDownload(url);

    if (response.success) {
      const urlDownload = window.URL.createObjectURL(response.data as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'layout-ativar.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.message = 'Download do layout realizado com sucesso.';
      notificate(response.message);
    }
  }

  async getDeleteLayout(): Promise<void> {
    const url = `${this.urlApi}/GetDeleteLayout`;
    const response = await this.axiosService.getDownload(url);

    if (response.success) {
      const urlDownload = window.URL.createObjectURL(response.data as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'layout-excluir.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.message = 'Download do layout realizado com sucesso.';
      notificate(response.message);
    }
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

  async update(item: IBaseEntity): Promise<boolean> {
    const result = await this.axiosService.put(this.urlApi, item);

    if (result.success) {
      result.message = 'Registro atualizado com sucesso.';
      notificate(result.message);
    }

    return result.success;
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
    const url = `${this.urlApi}/ExportToExcel`;
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

  async insert(item: any): Promise<boolean> {
    const result = await this.axiosService.post(this.urlApi, item);

    if (result.success) {
      result.message = 'Registro inserido com sucesso.';
      notificate(result.message);
    }

    return result.success;
  }

  async getById(id: number): Promise<any> {
    const url = `${this.urlApi}/${id}`;
    const result = await this.axiosService.get(url);
    return result.data;
  }

  async getAll(data: IGetAllRequest): Promise<IGetAllResponse | null> {
    const url = `${this.urlApi}/GetAllPaginated`;
    const response = await this.axiosService.post(url, data);
    return response.data;
  }

  async insertByImport(item: any) {
    const url = `${this.urlApi}/InsertByImport`;

    const formData = new FormData();
    formData.append('file', item.file);

    const response = await this.axiosService.postFormData(url, formData);

    if (response.success) {
      const urlDownload = window.URL.createObjectURL(response.data as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'logimportacao-inserir.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.message = 'Processamento finalizado, favor verificar os logs.';
      notificate(response.message, 'positive', 'top', 5000);
    }
  }

  async updateByImport(item: any) {
    const url = `${this.urlApi}/UpdateByImport`;

    const formData = new FormData();
    formData.append('file', item.file);

    const response = await this.axiosService.postFormData(url, formData);

    if (response.success) {
      const urlDownload = window.URL.createObjectURL(response.data as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'logimportacao-atualizar.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.message = 'Processamento finalizado, favor verificar os logs.';
      notificate(response.message, 'positive', 'top', 5000);
    }
  }

  async inactivateByImport(item: any) {
    const url = `${this.urlApi}/InactivateByImport`;

    const formData = new FormData();
    formData.append('file', item.file);

    const response = await this.axiosService.postFormData(url, formData);

    if (response.success) {
      const urlDownload = window.URL.createObjectURL(response.data as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'logimportacao-inativar.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.message = 'Processamento finalizado, favor verificar os logs.';
      notificate(response.message, 'positive', 'top', 5000);
    }
  }

  async activateByImport(item: any) {
    const url = `${this.urlApi}/ActivateByImport`;

    const formData = new FormData();
    formData.append('file', item.file);

    const response = await this.axiosService.postFormData(url, formData);

    if (response.success) {
      const urlDownload = window.URL.createObjectURL(response.data as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'logimportacao-ativar.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.message = 'Processamento finalizado, favor verificar os logs.';
      notificate(response.message, 'positive', 'top', 5000);
    }
  }

  async deleteByImport(item: any) {
    const url = `${this.urlApi}/DeleteByImport`;

    const formData = new FormData();
    formData.append('file', item.file);

    const response = await this.axiosService.postFormData(url, formData);

    if (response.success) {
      const urlDownload = window.URL.createObjectURL(response.data as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'logimportacao-excluir.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.message = 'Processamento finalizado, favor verificar os logs.';
      notificate(response.message, 'positive', 'top', 5000);
    }
  }
}
