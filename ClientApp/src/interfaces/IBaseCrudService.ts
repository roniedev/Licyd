import { ITableFilter } from 'src/components/tables/interfaces/ITableFilter';
import { IBaseEntity } from './IBaseEntity';
import { IBaseInactivate } from './IBaseInactivate';
import { IFilter } from './IFilter';
import { IGetAllRequest } from './IGetAllRequest';
import { IGetAllResponse } from './IGetAllResponse';

export interface IBaseCrudService {
  activate(id: number): Promise<boolean>;
  update(item: IBaseEntity): Promise<any | null>;
  delete(id: string): Promise<boolean>;
  export(filters: Array<IFilter>, filename?: string): Promise<void>;
  inactivate(item: IBaseInactivate): Promise<boolean>;
  insert(item: any): Promise<any | null>;
  getAll(data: ITableFilter[]): Promise<any[]>;
  getAllPaginated(data: IGetAllRequest): Promise<IGetAllResponse | null>;
  getById(id: number): Promise<any | null>;
}
