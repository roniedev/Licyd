import {
  IMotivoAbatimentoRequest,
  IMotivoAbatimentoResponse,
} from 'src/interfaces/IMotivoAbatimento';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class MotivoAbatimentoService extends BaseCrudServiceOld<
  IMotivoAbatimentoRequest,
  IMotivoAbatimentoResponse
> {}

export default new MotivoAbatimentoService('/MotivoAbatimento');
