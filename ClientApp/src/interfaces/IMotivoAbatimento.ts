import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IMotivoAbatimentoRequest extends IBaseRequest {
  descricao: string;
  divisaoId: string;
  isNotaFiscalSaida: boolean;
  isNotaFiscalDevolucao: boolean;
}

export interface IMotivoAbatimentoResponse extends IBaseCrudResponse {
  descricao: string;
  divisaoId: string;
  divisaoDescricao: string;
  isNotaFiscalSaida: boolean;
  isNotaFiscalDevolucao: boolean;
}
