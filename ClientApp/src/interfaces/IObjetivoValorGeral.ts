import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IObjetivoValorGeralRequest extends IBaseRequest {
  valor: number;
  dataInicialVigencia: Date;
  dataFinalVigencia: Date;
}

export interface IObjetivoValorGeralResponse extends IBaseCrudResponse {
  valor: number;
  dataInicialVigencia: Date;
  dataFinalVigencia: Date;
}
