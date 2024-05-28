import { IJumpForm } from 'src/components/forms/interfaces/IJumpForm';
import IJumpTableCrud from 'src/components/tables/interfaces/IJumpTableCrud';

export default interface ICadastroMestreDetalhe {
  tableMaster: IJumpTableCrud;
  formMaster: IJumpForm;
  tableDetail: IJumpTableCrud;
  formDetail: IJumpForm;
  parentKey: string;
}
