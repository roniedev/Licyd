import { IActionPage } from './IActionPage';

export interface IPageMenu {
  id: number;
  nomePagina: string;
  icone: string;
  componente: string;
  acoes: Array<IActionPage>;
}
