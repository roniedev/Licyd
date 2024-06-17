import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface ISetorClientesRequest extends IBaseRequest {
  clienteId: number;
  setorId: number;
  dataVigenciaInicial: Date;
  dataVigenciaFinal?: Date;
  codigoAssociacaoSetor: number;
}

export interface ISetorClientesResponse extends IBaseCrudResponse {
  clienteId: number;
  setorId: number;
  dataVigenciaInicial: Date;
  dataVigenciaFinal?: Date;
  codigoAssociacaoSetor: number;
}
