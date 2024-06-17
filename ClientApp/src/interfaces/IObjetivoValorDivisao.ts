import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IObjetivoValorDivisaoRequest extends IBaseRequest {
  divisaoId: number;
  anoReferencia: number;
  mesReferencia: number;
  valor: number;
  estabelecimentoId: number;
}

export interface IObjetivoValorDivisaoResponse extends IBaseCrudResponse {
  divisaoId: number;
  divisaoDescricao: string;
  anoReferencia: number;
  mesReferencia: number;
  valor: number;
  estabelecimentoId: number;
}
