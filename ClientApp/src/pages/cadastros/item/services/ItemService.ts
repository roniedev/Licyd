import { IBaseCrudService } from 'src/interfaces/IBaseCrudService';
import { BaseCrudService } from 'src/services/BaseCrudService';

export class ItemService
  extends BaseCrudService
  implements IBaseCrudService {}

export default ItemService;
