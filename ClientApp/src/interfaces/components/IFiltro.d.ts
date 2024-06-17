export interface IFiltroProps {
  label: string;
  value: string;
  type:
    | 'text'
    | 'number'
    | 'boolean'
    | 'decimal'
    | 'date'
    | 'rangedate'
    | 'select'
    | 'multiselect'
    | 'custom';
  options?: Array<IOptions>;
  onFetchServerData?: () => Array<IOptions>;
}

export interface IOptions {
  label: string;
  value: string;
}

export interface IQueryFiltro {
  propriedade: string;
  valor: string;
}

export interface IFiltro {
  propriedade: string;
  valor: string;
  label?: string;
  type?:
    | 'text'
    | 'number'
    | 'boolean'
    | 'decimal'
    | 'date'
    | 'rangedate'
    | 'select'
    | 'multiselect'
    | 'custom';
}
