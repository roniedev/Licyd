export interface ITablePagination {
  sortBy?: string | null;
  descending?: boolean;
  page?: number;
  rowsPerPage?: number;
  rowsNumber?: number;
}
