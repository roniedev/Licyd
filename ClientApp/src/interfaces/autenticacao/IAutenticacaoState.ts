import { IUsuario } from './IUsuario';

export interface IAutenticacaoState {
  usuario: IUsuario | null;
  isAutenticado: boolean;
}
