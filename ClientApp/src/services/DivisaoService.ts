import { IDivisaoRequest, IDivisaoResponse } from 'src/interfaces/IDivisao';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class DivisaoService extends BaseCrudServiceOld<
  IDivisaoRequest,
  IDivisaoResponse
> {}

export default new DivisaoService('/divisao');
