import { IGetAllRequest } from 'src/interfaces/IGetAllRequest';
import { IGetAllResponse } from 'src/interfaces/IGetAllResponse';
import { ITableColumn } from './ITableColumn';
import { ITableAction } from './ITableAction';

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
