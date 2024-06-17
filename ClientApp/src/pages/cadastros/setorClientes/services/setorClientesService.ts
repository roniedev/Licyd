import { IBaseCrudService } from 'src/interfaces/IBaseCrudService';
import { BaseCrudService } from 'src/services/BaseCrudService';

export class SetorClientesService
  extends BaseCrudService
  implements IBaseCrudService {}

export default SetorClientesService;
