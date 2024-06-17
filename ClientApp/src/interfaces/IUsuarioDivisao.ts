import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IUsuarioDivisaoRequest extends IBaseRequest {
  usuarioRede: string;
  divisaoId: number;
}

export interface IUsuarioDivisaoResponse extends IBaseCrudResponse {
  usuarioRede: string;
  divisaoId: number;
}
