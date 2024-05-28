import axios from 'axios';
import { hideLoading, showLoading } from './helpers/LoadingHelper';

export function getAxiosLogin(loading = true) {
  const api = axios.create({
    baseURL: process.env.API_LOGIN_URL,
  });

  api.interceptors.request.use((config) => {
    if (loading) showLoading();
    return config;
  });

  api.interceptors.response.use(
    (response) => {
      if (loading) hideLoading();
      return response;
    },
    (err) => {
      if (loading) hideLoading();
      return Promise.reject(err);
    }
  );

  return api;
}
