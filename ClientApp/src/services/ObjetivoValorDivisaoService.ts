import {
  IObjetivoValorDivisaoRequest,
  IObjetivoValorDivisaoResponse,
} from 'src/interfaces/IObjetivoValorDivisao';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class ObjetivoValorDivisaoService extends BaseCrudServiceOld<
  IObjetivoValorDivisaoRequest,
  IObjetivoValorDivisaoResponse
> {}

export default new ObjetivoValorDivisaoService('/ObjetivoValorDivisao');
