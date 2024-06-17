import {
  IObjetivoPercentualExcecaoRequest,
  IObjetivoPercentualExcecaoResponse,
} from 'src/interfaces/IObjetivoPercentualExcecao';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class ObjetivoPercentualExcecaoService extends BaseCrudServiceOld<
  IObjetivoPercentualExcecaoRequest,
  IObjetivoPercentualExcecaoResponse
> {}

export default new ObjetivoPercentualExcecaoService(
  '/ObjetivoPercentualExcecao'
);
