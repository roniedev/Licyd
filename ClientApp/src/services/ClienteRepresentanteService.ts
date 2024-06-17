import {
  IClienteRepresentanteRequest,
  IClienteRepresentanteResponse,
} from 'src/interfaces/IClienteRepresentante';
import BaseCrudServiceOld from './BaseCrudServiceOld';

class ClienteRepresentanteService extends BaseCrudServiceOld<
  IClienteRepresentanteRequest,
  IClienteRepresentanteResponse
> {}

export default new ClienteRepresentanteService('/ClienteRepresentante');
