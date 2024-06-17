import {
  ITipoDescontoRequest,
  ITipoDescontoResponse,
} from 'src/interfaces/ITipoDesconto';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class TipoDescontoService extends BaseCrudServiceOld<
  ITipoDescontoRequest,
  ITipoDescontoResponse
> {}

export default new TipoDescontoService('/TipoDesconto');
