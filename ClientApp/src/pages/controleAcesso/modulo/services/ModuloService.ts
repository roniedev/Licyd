import { IBaseCrudService } from 'src/interfaces/IBaseCrudService';
import { BaseCrudService } from 'src/services/BaseCrudService';

export class ModuloService
  extends BaseCrudService
  implements IBaseCrudService {}

export default ModuloService;
