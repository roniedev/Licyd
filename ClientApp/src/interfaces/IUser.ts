import { IUsuarioModulo } from 'src/components/menu/interfaces/IUsuarioModulo';
import { IRoute } from './IRoute';

export interface IUser {
  id: string;
  nome: string;
  userName: string;
  email: string;
  token: string;
  isHospede: boolean;
  dataEntrada: Date;
  dataSaida: Date;
  modulos: Array<IUsuarioModulo>;
  routes: Array<IRoute>;
}
