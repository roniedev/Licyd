import { IJumpForm } from 'src/components/forms/interfaces/IJumpForm';
import { IJumpTable } from './IJumpTable';
import { IBaseCrudService } from 'src/interfaces/IBaseCrudService';

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
  showInsertImport?: boolean;
  showUpdateImport?: boolean;
  showInactivateImport?: boolean;
  showActivateImport?: boolean;
  showDeleteImport?: boolean;
}
