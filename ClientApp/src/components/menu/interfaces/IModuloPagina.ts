import { IPaginaAcao } from './IPaginaAcao';

export interface IModuloPagina {
  id: number;
  nome: string;
  icone: string;
  componente: string;
  acoes: Array<IPaginaAcao>;
}
