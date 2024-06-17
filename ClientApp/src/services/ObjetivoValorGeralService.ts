import {
  IObjetivoValorGeralRequest,
  IObjetivoValorGeralResponse,
} from 'src/interfaces/IObjetivoValorGeral';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class ObjetivoValorGeralService extends BaseCrudServiceOld<
  IObjetivoValorGeralRequest,
  IObjetivoValorGeralResponse
> {}

export default new ObjetivoValorGeralService('/ObjetivoValorGeral');
