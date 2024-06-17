import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface ITipoDescontoRequest extends IBaseRequest {
  descricao: string;
  divisaoId: string;
  isNotaFiscalSaida: boolean;
  isNotaFiscalDevolucao: boolean;
}

export interface ITipoDescontoResponse extends IBaseCrudResponse {
  descricao: string;
  divisaoId: string;
  divisaoDescricao: string;
  isNotaFiscalSaida: boolean;
  isNotaFiscalDevolucao: boolean;
}
