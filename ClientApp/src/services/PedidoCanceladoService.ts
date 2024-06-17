import {
  IPedidoCanceladoRequest,
  IPedidoCanceladoResponse,
} from 'src/interfaces/IPedidoCancelado';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class PedidoCanceladoService extends BaseCrudServiceOld<
  IPedidoCanceladoRequest,
  IPedidoCanceladoResponse
> {}

export default new PedidoCanceladoService('/PedidoCancelado');
