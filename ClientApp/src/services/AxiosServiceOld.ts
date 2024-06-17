import api from 'src/api/useApi';
import { IAxiosResultOld } from 'src/interfaces/IAxiosResultOld';

class AxiosServiceOld {
  get = async <TResponse>(url: string): Promise<IAxiosResultOld> => {
    const response = await api.get(url);
    return {
      sucesso: response.status === 200,
      mensagem: '',
      objeto: response.status === 200 ? (response.data as TResponse) : null,
      status: response.status,
    };
  };

  postNoBody = async <TResponse>(
    url: string
  ): Promise<IAxiosResultOld | undefined> => {
    const response = await api.post(url);

    return {
      sucesso: response.status === 200,
      mensagem: '',
      objeto: response.status === 200 ? (response.data as TResponse) : null,
      status: response.status,
    };
  };

  postWithBody = async <TRequest, TResponse>(
    url: string,
    form: TRequest
  ): Promise<IAxiosResultOld | undefined> => {
    const response = await api.post(url, form);

    return {
      sucesso: response.status === 200,
      mensagem: '',
      objeto: response.status === 200 ? (response.data as TResponse) : null,
      status: response.status,
    };
  };

  postWithFormData = async (
    url: string,
    formData: FormData
  ): Promise<IAxiosResultOld> => {
    const response = await api.post(url, formData, {
      responseType: 'arraybuffer',
    });

    const blob = new Blob([response.data], {
      type: 'application/octet-stream',
    });

    return {
      sucesso: response.status === 200,
      mensagem: '',
      objeto: response.status === 200 ? (blob as Blob) : null,
      status: response.status,
    };
  };

  downloadPost = async <TRequest>(url: string, data?: TRequest) => {
    const response = await api.post(url, data, {
      responseType: 'arraybuffer',
    });

    const blob = new Blob([response.data], {
      type: 'application/octet-stream',
    });

    return {
      sucesso: response.status === 200,
      mensagem: '',
      objeto: response.status === 200 ? (blob as Blob) : null,
      status: response.status,
    };
  };

  download = async (url: string) => {
    const response = await api.get(url, { responseType: 'arraybuffer' });

    const blob = new Blob([response.data], {
      type: 'application/octet-stream',
    });

    return {
      sucesso: response.status === 200,
      mensagem: '',
      objeto: response.status === 200 ? (blob as Blob) : null,
      status: response.status,
    };
  };

  // patch = async <TResponse>(url: string) => {
  //   const response = await api.patch(url);

  //   return {
  //     sucesso: response.status === 200,
  //     mensagem: '',
  //     objeto: response.status === 200 ? (response.data as TResponse) : null,
  //     status: response.status,
  //   };
  // };

  patch = async (url: string, data: any = null): Promise<IAxiosResultOld> => {
    const response = data ? await api.patch(url, data) : await api.patch(url);

    return {
      sucesso: response.status === 200 || response.status === 201,
      objeto:
        response.status === 200 || response.status === 201
          ? response.data
          : null,
      status: response.status,
      mensagem: '',
    };
  };

  put = async <TRequest, TResponse>(url: string, form: TRequest) => {
    const response = await api.put(url, form);

    return {
      sucesso: response.status === 200,
      mensagem: '',
      objeto: response.status === 200 ? (response.data as TResponse) : null,
      status: response.status,
    };
  };

  delete = async <TResponse>(url: string): Promise<IAxiosResultOld> => {
    const response = await api.delete(url);

    return {
      sucesso: response.status === 200,
      mensagem: '',
      objeto: response.status === 200 ? (response.data as TResponse) : null,
      status: response.status,
    };
  };

  postDownload = async <TRequest>(
    url: string,
    data?: TRequest
  ): Promise<IAxiosResultOld> => {
    const response = await api.post(url, data, {
      responseType: 'arraybuffer',
    });

    const blob = new Blob([response.data], {
      type: 'application/octet-stream',
    });

    return {
      sucesso: response.status === 200 || response.status === 201,
      objeto: blob as Blob,
      status: response.status,
      mensagem: '',
    };
  };
}

export default new AxiosServiceOld();
