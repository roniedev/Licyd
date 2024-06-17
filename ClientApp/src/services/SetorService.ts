import { ISetorRequest, ISetorResponse } from 'src/interfaces/ISetor';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class Setor extends BaseCrudServiceOld<ISetorRequest, ISetorResponse> {
  cloneCompleto = async (id: number): Promise<boolean> => {
    const url = `${this.url}/clonecompleto/${id}`;
    const response = await this.AxiosServiceOld.postNoBody(url);

    if (response?.sucesso) {
      response.mensagem = response.objeto.mensagens[0].descricao;
      this.notificar(response);
      return response.sucesso;
    }

    return false;
  };

  async inativar(item: any): Promise<boolean> {
    const url = `${this.url}/inativarnovo`;

    const response = await this.AxiosServiceOld.postWithBody<any, any>(
      url,
      item
    );

    if (response?.sucesso) {
      response.mensagem = 'Registro inativado com sucesso.';
      this.notificar(response);
      return response.sucesso;
    }

    return false;
  }

  inativarViaImportacao = async (item: any) => {
    const url = `${this.url}/InativarImportacao`;

    const formData = new FormData();
    formData.append('formFile', item.file);

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
    }

    this.notificar(response);
  };

  obterInativarLayout = async () => {
    const url = `${this.url}/getinativarlayout`;
    const response = await this.AxiosServiceOld.download(url);

    if (response.sucesso) {
      const urlDownload = window.URL.createObjectURL(response.objeto as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'layout-inativar-setor.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.mensagem = 'Download do layout realizado com sucesso.';
    }

    this.notificar(response);
  };

  atualizarViaImportacao = async (item: any) => {
    const url = `${this.url}/AtualizarImportacao`;

    const formData = new FormData();
    formData.append('formFile', item.file);

    const response = await this.AxiosServiceOld.postWithFormData(url, formData);

    if (response.sucesso) {
      const urlDownload = window.URL.createObjectURL(response.objeto as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'logimportacao-atualizar-setor.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.mensagem = 'Processamento finalizado, favor verificar os logs.';
    }

    this.notificar(response);
  };

  obterAtualizarLayout = async () => {
    const url = `${this.url}/GetAtualizarLayout`;
    const response = await this.AxiosServiceOld.download(url);

    if (response.sucesso) {
      const urlDownload = window.URL.createObjectURL(response.objeto as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'layout-atualizar-setor.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.mensagem = 'Download do layout realizado com sucesso.';
    }

    this.notificar(response);
  };
}

export default new Setor('/setor');
