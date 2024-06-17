import { AxiosInstance } from 'axios';
import { getAxiosInstance } from 'src/boot/axios';
import { IAxiosResult } from 'src/interfaces/IAxiosResult';

export default class AxiosService {
  private readonly api: AxiosInstance;

  constructor() {
    this.api = getAxiosInstance();
  }

  get = async (url: string): Promise<IAxiosResult> => {
    const response = await this.api.get(url);
    return {
      success: response.status === 200,
      data: response.status === 200 ? response.data : null,
      status: response.status,
    };
  };

  getDownload = async (url: string): Promise<IAxiosResult> => {
    const response = await this.api.get(url, {
      responseType: 'blob',
    });

    return {
      success: response.status === 200,
      data: response.status === 200 ? response.data : null,
      status: response.status,
    };
  };

  post = async <TResponse>(url: string, data: any): Promise<IAxiosResult> => {
    const response = await this.api.post(url, data);

    return {
      success: response.status === 200 || response.status === 201,
      data:
        response.status === 200 || response.status === 201
          ? (response.data as TResponse)
          : null,
      status: response.status,
    };
  };

  postFormData = async (
    url: string,
    formData: FormData
  ): Promise<IAxiosResult> => {
    const response = await this.api.post(url, formData, {
      responseType: 'arraybuffer',
    });

    const blob = new Blob([response.data], {
      type: 'application/octet-stream',
    });

    return {
      success: response.status === 200 || response.status === 201,
      data: response.status === 200 ? (blob as Blob) : null,
      status: response.status,
    };
  };

  postDownload = async <TRequest>(
    url: string,
    data?: TRequest
  ): Promise<IAxiosResult> => {
    const response = await this.api.post(url, data, {
      responseType: 'arraybuffer',
    });

    const blob = new Blob([response.data], {
      type: 'application/octet-stream',
    });

    return {
      success: response.status === 200 || response.status === 201,
      data: blob as Blob,
      status: response.status,
    };
  };

  patch = async (url: string, data: any = null): Promise<IAxiosResult> => {
    const response = data
      ? await this.api.patch(url, data)
      : await this.api.patch(url);

    return {
      success: response.status === 200 || response.status === 201,
      data:
        response.status === 200 || response.status === 201
          ? response.data
          : null,
      status: response.status,
    };
  };

  put = async (url: string, form: any): Promise<IAxiosResult> => {
    const response = await this.api.put(url, form);

    return {
      success: response.status === 200 || response.status === 201,
      data:
        response.status === 200 || response.status === 201
          ? response.data
          : null,
      status: response.status,
    };
  };

  delete = async (url: string): Promise<IAxiosResult> => {
    const response = await this.api.delete(url);

    return {
      success: response.status === 200 || response.status === 201,
      data:
        response.status === 200 || response.status === 201
          ? response.data
          : null,
      status: response.status,
    };
  };
}
