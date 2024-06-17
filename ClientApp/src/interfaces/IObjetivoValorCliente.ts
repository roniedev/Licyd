import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IObjetivoValorClienteRequest extends IBaseRequest {
  clienteId: number;
  divisaoId: number;
  subdivisaoId: number;
  anoReferencia: number;
  mesReferencia: number;
  estabelecimentoId: number;
  valorObjetivo: number;
  valorCompromisso: number;
}

export interface IObjetivoValorClienteResponse extends IBaseCrudResponse {
  clienteId: number;
  clienteDescricao: string;
  divisaoId: number;
  divisaoDescricao: string;
  subdivisaoId: number;
  subdivisaoDescricao: string;
  anoReferencia: number;
  mesReferencia: number;
  estabelecimentoId: number;
  valorObjetivo: number;
  valorCompromisso: number;
}
