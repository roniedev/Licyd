import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface ISubdivisaoRequest extends IBaseRequest {
  divisaoId: number;
  descricao: string;
  tipoVenda: number;
}

export interface ISubdivisaoResponse extends IBaseCrudResponse {
  divisaoId: number;
  divisaoDescricao: number;
  descricao: string;
  tipoVenda: number;
}
