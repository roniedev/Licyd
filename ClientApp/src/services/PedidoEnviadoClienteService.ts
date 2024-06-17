import BaseCrudServiceOld from './BaseCrudServiceOld';
import {
  IPedidoEnviadoClienteRequest,
  IPedidoEnviadoClienteResponse,
} from 'src/interfaces/IPedidoEnviadoCliente';

class PedidoEnviadoClienteService extends BaseCrudServiceOld<
  IPedidoEnviadoClienteRequest,
  IPedidoEnviadoClienteResponse
> {}

export default new PedidoEnviadoClienteService('/PedidoEnviadoCliente');
