import { IModuloPagina } from './IModuloPagina';

export interface IUsuarioModulo {
  id: number;
  nome: string;
  icone: string;
  subModulos: Array<IUsuarioModulo>;
  paginas: Array<IModuloPagina>;
}
