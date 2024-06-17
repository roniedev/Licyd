import {
  IDivisaoSubdivisaoClasseItemRequest,
  IDivisaoSubdivisaoClasseItemResponse,
} from 'src/interfaces/IDivisaoSubdivisaoClasseItem';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class DivisaoSubdivisaoClasseItemService extends BaseCrudServiceOld<
  IDivisaoSubdivisaoClasseItemRequest,
  IDivisaoSubdivisaoClasseItemResponse
> {}

export default new DivisaoSubdivisaoClasseItemService(
  '/DivisaoSubdivisaoClasseItem'
);
