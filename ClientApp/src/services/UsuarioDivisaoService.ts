import {
  IUsuarioDivisaoRequest,
  IUsuarioDivisaoResponse,
} from 'src/interfaces/IUsuarioDivisao';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class UsuarioDivisaoService extends BaseCrudServiceOld<
  IUsuarioDivisaoRequest,
  IUsuarioDivisaoResponse
> {}

export default new UsuarioDivisaoService('/UsuarioDivisao');
