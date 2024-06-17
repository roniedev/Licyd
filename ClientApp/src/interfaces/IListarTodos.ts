export interface IListarTodosRequest {
  skip: number;
  take: number;
  filtros: Array<IFiltros>;
}

export interface IFiltros {
  propriedade: string;
  valor: string;
}

export interface IListarTodosResponse {
  registros: any;
  totalRegistros: number;
  totalPaginas: number;
}
