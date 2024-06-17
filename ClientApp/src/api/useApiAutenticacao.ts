import axios from 'axios';

const apiAutenticacao = axios.create({
  //baseURL: 'https://controleacesso.teuto.com.br/autenticacao/api',
  baseURL: 'https://controleacessopiloto.teuto.com.br/autenticacao/api',
});

export default apiAutenticacao;
