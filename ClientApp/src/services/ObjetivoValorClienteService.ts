import {
  IObjetivoValorClienteRequest,
  IObjetivoValorClienteResponse,
} from 'src/interfaces/IObjetivoValorCliente';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class ObjetivoValorClienteService extends BaseCrudServiceOld<
  IObjetivoValorClienteRequest,
  IObjetivoValorClienteResponse
> {}

export default new ObjetivoValorClienteService('ObjetivoValorCliente');
