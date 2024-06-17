import { ITableColumn } from './ITableColumn';
import { ITableAction } from './ITableAction';
import { ITablePagination } from './ITablePagination';
import { ITableFilter } from './ITableFilter';
import { IGetAllRequest } from './IGetAllRequest';
import { IGetAllResponse } from './IGetAllResponse';

export interface IJumpTable {
  columns: Array<ITableColumn>;
  rows: Array<any> | (() => Array<any>);
  title?: string;
  actions?: Array<ITableAction>;
  pagination?: ITablePagination;
  selection?: 'none' | 'single' | 'multiple';
  separator?: 'horizontal' | 'vertical' | 'cell' | 'none';
  dense?: boolean;
  selectedRows?: Array<any>;
  rowKey?: string;
  isServerRows?: boolean;
  maxColumnWidth?: number;
  filters?: ITableFilter[];
  getServerRows?: (data: IGetAllRequest) => Promise<IGetAllResponse | null>;
}
