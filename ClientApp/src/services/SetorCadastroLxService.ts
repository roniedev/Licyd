import {
  ISetorCadastroLxRequest,
  ISetorCadastroLxResponse,
} from 'src/interfaces/ISetorCadastroLx';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class SetorCadastroLxService extends BaseCrudServiceOld<
  ISetorCadastroLxRequest,
  ISetorCadastroLxResponse
> {
  async inativar(item: any): Promise<boolean> {
    const url = `${this.url}/inativarnovo`;

    const response = await this.AxiosServiceOld.postWithBody<
      ISetorCadastroLxRequest,
      ISetorCadastroLxResponse
    >(url, item);

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
      downloadLink.download = 'layout-inativar-setor-cadastro-lx.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.mensagem = 'Download do layout realizado com sucesso.';
    }

    this.notificar(response);
  };

  obterDataFinalVigenciaLayout = async () => {
    const url = `${this.url}/getinativarlayout`;
    const response = await this.AxiosServiceOld.download(url);

    if (response.sucesso) {
      const urlDownload = window.URL.createObjectURL(response.objeto as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'layout-datafinalvigencia-setorcadastrolx.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.mensagem = 'Download do layout realizado com sucesso.';
    }

    this.notificar(response);
  };
}

export default new SetorCadastroLxService('/setorcadastrolx');
