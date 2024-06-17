import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IClienteRepresentanteRequest extends IBaseRequest {
  clienteId: number;
  divisaoId: number;
  subdivisao: number;
  representanteId: number;
  dataIniclalVigencia: Date;
  dataFinalVigencia: Date;
}

export interface IClienteRepresentanteResponse extends IBaseCrudResponse {
  clienteId: number;
  clienteDescricao: string;
  divisaoId: number;
  divisaoDescricao: string;
  subdivisao: number;
  subdivisaoDescricao: string;
  representanteId: number;
  representanteDescricao: string;
  dataIniclalVigencia: Date;
  dataFinalVigencia: Date;
}
