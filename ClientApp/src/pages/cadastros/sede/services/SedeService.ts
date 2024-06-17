import { IBaseCrudService } from 'src/interfaces/IBaseCrudService';
import { BaseCrudService } from 'src/services/BaseCrudService';

export default class SedeService
  extends BaseCrudService
  implements IBaseCrudService {}
