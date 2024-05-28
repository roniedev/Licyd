import { IBaseCrudService } from 'src/interfaces/IBaseCrudService';
import { BaseCrudService } from 'src/services/BaseCrudService';

export class PaginaService
  extends BaseCrudService
  implements IBaseCrudService {}

export default PaginaService;
