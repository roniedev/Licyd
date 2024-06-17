import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IObjetivoPercentualExcecaoRequest extends IBaseRequest {
  descricao: string;
  objetivoPercentualBase: number;
  objetivoPercentual1: number;
  objetivoPercentual2: number;
  objetivoPercentual3: number;
  objetivoPercentual4: number;
  dataInicial: Date;
  dataFinal: Date;
}

export interface IObjetivoPercentualExcecaoResponse extends IBaseCrudResponse {
  descricao: string;
  objetivoPercentualBase: number;
  objetivoPercentual1: number;
  objetivoPercentual2: number;
  objetivoPercentual3: number;
  objetivoPercentual4: number;
  dataInicial: Date;
  dataFinal: Date;
}
