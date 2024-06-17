export interface ITabela {
  titulo: string;
  colunas: Array<ITabelaColuna>;
  linhas?: Array<any>;
}

export interface ITabelaColuna {
  name: string;
  required: boolean;
  label: string;
  align: 'left' | 'center' | 'right';
  field: string;
  sortable: boolean;
  type: 'text' | 'number' | 'boolean' | 'date' | 'datetime';
}
