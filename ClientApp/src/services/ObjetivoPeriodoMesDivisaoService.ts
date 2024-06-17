import {
  IObjetivoPeriodoMesDivisaoRequest,
  IObjetivoPeriodoMesDivisaoResponse,
} from 'src/interfaces/IObjetivoPeriodoMesDivisao';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class ObjetivoPeriodoMesDivisaoService extends BaseCrudServiceOld<
  IObjetivoPeriodoMesDivisaoRequest,
  IObjetivoPeriodoMesDivisaoResponse
> {}

export default new ObjetivoPeriodoMesDivisaoService(
  '/ObjetivoPeriodoMesDivisao'
);
