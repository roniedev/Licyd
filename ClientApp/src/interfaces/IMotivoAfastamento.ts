import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IMotivoAfastamentoRequest extends IBaseRequest {
  descricao: string;
}

export interface IMotivoAfastamentoResponse extends IBaseCrudResponse {
  descricao: string;
}
