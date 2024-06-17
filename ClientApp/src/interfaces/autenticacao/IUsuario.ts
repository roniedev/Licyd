import { IMenuUsuario } from '../IMenuUsuario.interfaces';
import { IRoute } from '../IRoute';

export interface IUsuario {
  nome: string;
  usuarioRede: string;
  nomeUsuarioERP: string;
  email: string;
  matricula: string;
  token: string;
  usuarioMenu: Array<IMenuUsuario>;
  paginasAplicacao: Array<IRoute>;
}
