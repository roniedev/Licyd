import {
  IObjetivoValorSetorRequest,
  IObjetivoValorSetorResponse,
} from 'src/interfaces/IObjetivoValorSetor';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class ObjetivoValorSetorService extends BaseCrudServiceOld<
  IObjetivoValorSetorRequest,
  IObjetivoValorSetorResponse
> {}

export default new ObjetivoValorSetorService('/ObjetivoValorSetor');
