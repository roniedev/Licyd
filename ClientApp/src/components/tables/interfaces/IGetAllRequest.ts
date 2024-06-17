import { ITableFilter } from 'src/components/tables/interfaces/ITableFilter';

export interface IGetAllRequest {
  skip: number;
  take: number;
  orderBy: string;
  isAscending: boolean;
  filters: Array<ITableFilter>;
}
