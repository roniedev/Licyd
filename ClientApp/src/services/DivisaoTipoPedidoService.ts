import {
  IDivisaoTipoPedidoRequest,
  IDivisaoTipoPedidoResponse,
} from 'src/interfaces/IDivisaoTipoPedido';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class DivisaoTipoPedidoService extends BaseCrudServiceOld<
  IDivisaoTipoPedidoRequest,
  IDivisaoTipoPedidoResponse
> {}

export default new DivisaoTipoPedidoService('/DivisaoTipoPedido');
