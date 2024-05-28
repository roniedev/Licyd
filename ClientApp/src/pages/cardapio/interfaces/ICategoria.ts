export interface ICategoria {
  id: number;
  nome: string;
  descricao: string;
  itens: IItem[];
}

export interface IItem {
  id: number;
  categoriaId: number;
  categoriaNome: string;
  nome: string;
  descricao: string;
  isItemAdicional: boolean;
  quantidade: number;
  valorUnitario: number;
}
