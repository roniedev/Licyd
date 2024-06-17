import { ITableFilter } from 'src/components/tables/interfaces/ITableFilter';
import { IBaseEntity } from './IBaseEntity';
import { IBaseInactivate } from './IBaseInactivate';
import { IGetAllRequest } from 'src/components/tables/interfaces/IGetAllRequest';
import { IGetAllResponse } from 'src/components/tables/interfaces/IGetAllResponse';

export interface IBaseCrudService {
  activate(id: number): Promise<boolean>;
  update(item: IBaseEntity): Promise<boolean>;
  delete(id: string): Promise<boolean>;
  export(filters: Array<ITableFilter>, filename?: string): Promise<void>;
  inactivate(item: IBaseInactivate): Promise<boolean>;
  insert(item: any): Promise<boolean>;
  getAll(data: IGetAllRequest): Promise<IGetAllResponse | null>;
  getById(id: number): Promise<any | null>;
  getInsertLayout(): Promise<void>;
  getUpdateLayout(): Promise<void>;
  getInactivateLayout(): Promise<void>;
  getDeleteLayout(): Promise<void>;
  getActivateLayout(): Promise<void>;
  insertByImport(item: any): Promise<void>;
  updateByImport(item: any): Promise<void>;
  inactivateByImport(item: any): Promise<void>;
  deleteByImport(item: any): Promise<void>;
  activateByImport(item: any): Promise<void>;
}
