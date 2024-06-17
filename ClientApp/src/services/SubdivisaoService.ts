import {
  ISubdivisaoRequest,
  ISubdivisaoResponse,
} from 'src/interfaces/ISubdivisao';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class SubdivisaoService extends BaseCrudServiceOld<
  ISubdivisaoRequest,
  ISubdivisaoResponse
> {}

export default new SubdivisaoService('/subdivisao');
