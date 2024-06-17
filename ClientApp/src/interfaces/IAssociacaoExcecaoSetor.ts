import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IAssociacaoExcecaoSetorRequest extends IBaseRequest {
  excecaoId: number;
  setorId: number;
}

export interface IAssociacaoExcecaoSetorResponse extends IBaseCrudResponse {
  excecaoId: number;
  setorId: number;
  setorDescricao: string;
}
