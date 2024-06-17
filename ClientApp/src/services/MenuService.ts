import apiAutenticacao from 'src/api/useApiAutenticacao';

class MenuService {
  private readonly url: string;

  constructor(url: string) {
    this.url = url;
  }

  obterMenuUsuario = async (usuarioDeRede: string) => {
    const url = `${this.url}/obtermenususuario`;
    const response = await apiAutenticacao.post(url, { usuarioDeRede });
    return response.data;
  };

  obterMenusAplicacao = async (idAplicacao: number) => {
    const url = `${this.url}/obtermenusaplicacao`;
    const response = await apiAutenticacao.post(url, { idAplicacao });
    return response.data;
  };
}

export default new MenuService('menu');
