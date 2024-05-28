export interface IPedido {
  itensPedido: IItemPedido[];
}

export interface IItemPedido {
  itemId: number;
  valorUnitario: number;
  quantidade: number;
  categoriaId: number;
  nome: string;
  descricao: string;
  adicionais?: IItemPedido[];
  observacao?: string;
}
