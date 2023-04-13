import axios from 'axios';

const uriparams = document.location.pathname.split('/');
const staticAxios = axios.create();

export const apiPath = (document.location.origin + uriparams[1] + '/api')
  .replace(':8080', ':7150')
  .replace('http://', 'https://');

staticAxios.interceptors.request.use(
  (config) => {
    (config.baseURL = apiPath), (config.withCredentials = true);

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default staticAxios;