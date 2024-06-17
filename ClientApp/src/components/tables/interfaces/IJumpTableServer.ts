import { ITableColumn } from './ITableColumn';
import { ITableAction } from './ITableAction';
import { IGetAllRequest } from './IGetAllRequest';
import { IGetAllResponse } from './IGetAllResponse';

export interface IJumpTableServer {
  columns: Array<ITableColumn>;
  getRows: (data: IGetAllRequest) => Promise<IGetAllResponse>;
  actions?: Array<ITableAction>;
  title?: string;
  selection?: 'none' | 'single' | 'multiple';
  separator?: 'horizontal' | 'vertical' | 'cell' | 'none';
  dense?: boolean;
  selectedRows?: Array<any>;
  rowKey?: string;
}
