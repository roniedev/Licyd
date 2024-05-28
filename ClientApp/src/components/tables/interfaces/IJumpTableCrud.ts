import { IJumpForm } from 'src/components/forms/interfaces/IJumpForm';
import { IBaseCrudService } from 'src/interfaces/IBaseCrudService';
import { IJumpTable } from './IJumpTable';

export default interface IJumpTableCrud extends IJumpTable {
  service: IBaseCrudService;
  componentName: string;
  inactivateForm?: IJumpForm;
  showAddButton?: boolean;
  isEditActiveRows?: boolean;
  isActivateInactivateVisible?: boolean;
  isDeleteVisible?: boolean;
  isEditVisible?: boolean;
  isShowVisible?: boolean;
}
