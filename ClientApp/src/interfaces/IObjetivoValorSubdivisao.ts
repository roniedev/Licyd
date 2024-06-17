import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IObjetivoValorSubdivisaoRequest extends IBaseRequest {
  divisaoId: number;
  subdivisaoId: number;
  anoReferencia: number;
  mesReferencia: number;
  valor: number;
}

export interface IObjetivoValorSubdivisaoResponse extends IBaseCrudResponse {
  divisaoId: number;
  divisaoDescricao: string;
  subdivisaoId: number;
  subdivisaoDescricao: string;
  anoReferencia: number;
  mesReferencia: number;
  valor: number;
}
