import { IJumpForm } from 'src/components/forms/interfaces/IJumpForm';
import IJumpTableCrud from 'src/components/tables/interfaces/IJumpTableCrud';

export interface IJumpCrud {
  jumpCrudTable: IJumpTableCrud;
  jumpForm: IJumpForm;
}
