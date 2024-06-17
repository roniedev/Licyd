import {
  IObjetivoPeriodoMesSubdivisaoRequest,
  IObjetivoPeriodoMesSubdivisaoResponse,
} from 'src/interfaces/IObjetivoPeriodoMesSubdivisao';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class ObjetivoPeriodoMesSubdivisaoService extends BaseCrudServiceOld<
  IObjetivoPeriodoMesSubdivisaoRequest,
  IObjetivoPeriodoMesSubdivisaoResponse
> {}

export default new ObjetivoPeriodoMesSubdivisaoService(
  '/ObjetivoPeriodoMesSubdivisao'
);
