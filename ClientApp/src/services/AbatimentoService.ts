import {
  IAbatimentoRequest,
  IAbatimentoResponse,
} from 'src/interfaces/IAbatimento';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class AbatimentoService extends BaseCrudServiceOld<
  IAbatimentoRequest,
  IAbatimentoResponse
> {
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
    const url = `${this.url}/GetInativarLayout`;
    const response = await this.AxiosServiceOld.download(url);

    if (response.sucesso) {
      const urlDownload = window.URL.createObjectURL(response.objeto as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'layout-inativar-abatimentos.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.mensagem = 'Download do layout realizado com sucesso.';
    }

    this.notificar(response);
  };
}

export default new AbatimentoService('/abatimento');
