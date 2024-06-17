import {
  IAssociacaoExcecaoSetorRequest,
  IAssociacaoExcecaoSetorResponse,
} from 'src/interfaces/IAssociacaoExcecaoSetor';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class AssociacaoExcecaoSetorService extends BaseCrudServiceOld<
  IAssociacaoExcecaoSetorRequest,
  IAssociacaoExcecaoSetorResponse
> {}

export default new AssociacaoExcecaoSetorService('/AssociacaoExcecaoSetor');
