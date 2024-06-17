import {
  IAfastamentoRequest,
  IAfastamentoResponse,
} from 'src/interfaces/IAfastamento';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class AfastamentoService extends BaseCrudServiceOld<
  IAfastamentoRequest,
  IAfastamentoResponse
> {}

export default new AfastamentoService('/Afastamento');
