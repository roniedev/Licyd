import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IAfastamentoRequest extends IBaseRequest {
  motivoAfastamentoId: number;
  matricula: string;
  dataInicial: Date;
  dataFinal?: Date;
}

export interface IAfastamentoResponse extends IBaseCrudResponse {
  motivoAfastamentoId: number;
  motivoAfastamentoDescricao: string;
  matricula: string;
  matriculaDescricao: string;
  dataInicial: Date;
  dataFinal?: Date;
}
