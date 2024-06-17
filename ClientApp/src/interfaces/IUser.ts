import { IUserMenu } from 'src/components/menu/interfaces/IUserMenu';
import { IRoute } from './IRoute';

export interface IUser {
  id: string;
  nome: string;
  usuarioRede: string;
  usernameErp: string;
  email: string;
  matricula: string;
  token: string;
  usuarioMenu: Array<IUserMenu>;
  paginasAplicacao: Array<IRoute>;
}
