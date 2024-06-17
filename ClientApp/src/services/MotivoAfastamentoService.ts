import {
  IMotivoAbatimentoRequest,
  IMotivoAbatimentoResponse,
} from 'src/interfaces/IMotivoAbatimento';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class MotivoAfastamentoService extends BaseCrudServiceOld<
  IMotivoAbatimentoRequest,
  IMotivoAbatimentoResponse
> {}

export default new MotivoAfastamentoService('/MotivoAfastamento');
