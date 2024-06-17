import {
  IObjetivoPercentualPadraoRequest,
  IObjetivoPercentualPadraoResponse,
} from 'src/interfaces/IObjetivoPercentualPadrao';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class ObjetivoPercentualPadraoService extends BaseCrudServiceOld<
  IObjetivoPercentualPadraoRequest,
  IObjetivoPercentualPadraoResponse
> {}

export default new ObjetivoPercentualPadraoService('/ObjetivoPercentualPadrao');
