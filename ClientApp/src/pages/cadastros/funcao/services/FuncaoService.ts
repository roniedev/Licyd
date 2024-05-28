import { IBaseCrudService } from 'src/interfaces/IBaseCrudService';
import { BaseCrudService } from 'src/services/BaseCrudService';

export class FuncaoService
  extends BaseCrudService
  implements IBaseCrudService {}

export default FuncaoService;
