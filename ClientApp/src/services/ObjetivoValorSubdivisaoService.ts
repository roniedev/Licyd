import {
  IObjetivoValorSubdivisaoRequest,
  IObjetivoValorSubdivisaoResponse,
} from 'src/interfaces/IObjetivoValorSubdivisao';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class ObjetivoValorSubdivisaoService extends BaseCrudServiceOld<
  IObjetivoValorSubdivisaoRequest,
  IObjetivoValorSubdivisaoResponse
> {}

export default new ObjetivoValorSubdivisaoService('/ObjetivoValorSubdivisao');
