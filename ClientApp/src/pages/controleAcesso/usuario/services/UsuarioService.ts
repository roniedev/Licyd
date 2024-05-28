import { IBaseCrudService } from 'src/interfaces/IBaseCrudService';
import { BaseCrudService } from 'src/services/BaseCrudService';

export class UsuarioService
  extends BaseCrudService
  implements IBaseCrudService {}

export default UsuarioService;
