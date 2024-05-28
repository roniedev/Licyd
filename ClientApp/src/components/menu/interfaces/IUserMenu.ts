import { IPageMenu } from './IPageMenu';

export interface IUserMenu {
  idMenu: number;
  nomeMenu: string;
  idMenuPai: number;
  icone: string;
  menusFilhos: Array<IUserMenu>;
  paginas: Array<IPageMenu>;
  urlApiProducao: string;
  urlApiPiloto: string;
  urlApiDev: string;
}
