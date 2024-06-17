import { notificate } from 'src/helpers/NotificationHelper';
import AxiosService from 'src/services/AxiosService';

export default class RelatorioService {
  protected readonly axiosService: AxiosService;

  constructor() {
    this.axiosService = new AxiosService();
  }

  async MultiploAcesso(formValues: any) {
    const url = 'Relatorio/MultiploAcesso';
    const response = await this.axiosService.postDownload(url, formValues);

    if (response.success) {
      const urlDownload = window.URL.createObjectURL(response.data as Blob);

      const downloadLink = document.createElement('a');
      downloadLink.href = urlDownload;
      downloadLink.download = 'relatorio-multiplo-acesso.xlsx';

      document.body.appendChild(downloadLink);
      downloadLink.click();

      document.body.removeChild(downloadLink);
      window.URL.revokeObjectURL(urlDownload);

      response.message = 'Relatório gerado com sucesso!';
      notificate(response.message);
    }
  }
}
