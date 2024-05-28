import { IBaseCrudService } from 'src/interfaces/IBaseCrudService';
import { BaseCrudService } from 'src/services/BaseCrudService';

export class PaginaAcaoService
  extends BaseCrudService
  implements IBaseCrudService {}

export default PaginaAcaoService;
