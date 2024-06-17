import { IBaseCrudResponse, IBaseResponse } from './IBaseCrudServiceOld';

export interface IObjetivoValorSetorRequest extends IBaseResponse {
  setorId: number;
  anoReferencia: number;
  mesReferencia: number;
  valorObjetivo: number;
  valorCompromisso: number;
}

export interface IObjetivoValorSetorResponse extends IBaseCrudResponse {
  setorId: number;
  setorDescricao: number;
  anoReferencia: number;
  mesReferencia: number;
  valorObjetivo: number;
  valorCompromisso: number;
}
