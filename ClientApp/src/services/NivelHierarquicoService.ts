import {
  INivelHierarquicoRequest,
  INivelHierarquicoResponse,
} from 'src/interfaces/INivelHierarquico';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class NivelHierarquicoService extends BaseCrudServiceOld<
  INivelHierarquicoRequest,
  INivelHierarquicoResponse
> {}

export default new NivelHierarquicoService('/nivelhierarquico');
