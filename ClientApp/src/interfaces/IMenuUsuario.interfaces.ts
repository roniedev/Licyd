export interface IMenuUsuario {
  idMenu: number;
  nomeMenu: string;
  idMenuPai: number;
  icone: string;
  menusFilhos: Array<IMenuUsuario>;
  paginas: Array<IPagina>;
}

export interface IPagina {
  id: number;
  nomePagina: string;
  icone: string;
  componente: string;
  acoes: IPaginaAcao[];
}

export interface IPaginaAcao {
  nome: string;
}
