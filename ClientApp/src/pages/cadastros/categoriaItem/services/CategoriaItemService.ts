import { IBaseCrudService } from 'src/interfaces/IBaseCrudService';
import { BaseCrudService } from 'src/services/BaseCrudService';

export class CategoriaItemService
  extends BaseCrudService
  implements IBaseCrudService {}

export default CategoriaItemService;
