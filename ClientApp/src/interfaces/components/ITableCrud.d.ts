import { IFiltroProps } from './IFiltro';
import { IForm } from './IForm';

export interface ITableProps {
  titulo: string;
  colunas: Array<ITableColumn>;
  acoes: Array<ITableAction>;
  selecao: 'none' | 'single' | 'multiple';
  bordas: 'horizontal' | 'vertical' | 'cell' | 'none';
  filtros: Array<IFiltroProps>;
  formInativar?: IForm;
  formImportar?: IForm;
  isEditarVisible?: boolean;
  isAtivarVisible?: boolean;
  isInativarVisible?: boolean;
  isExcluirVisible?: boolean;
}

export interface ITableColumn {
  name: string;
  required: boolean;
  label: string;
  align: 'left' | 'center' | 'right';
  field: string;
  sortable: boolean;
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
}

export interface ITableAction {
  nome: string;
  icon?: string;
  onClick: (item: any) => void;
}
