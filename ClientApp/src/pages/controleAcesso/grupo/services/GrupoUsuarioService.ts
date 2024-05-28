import { IBaseCrudService } from 'src/interfaces/IBaseCrudService';
import { BaseCrudService } from 'src/services/BaseCrudService';

export class GrupoUsuarioService
  extends BaseCrudService
  implements IBaseCrudService {}

export default GrupoUsuarioService;
