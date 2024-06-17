import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IObjetivoPercentualPadraoRequest extends IBaseRequest {
  divisaoId: number;
  subdivisaoId: number;
  estabelecimentoId: number;
  objetivoPercentualBase: number;
  objetivoPercentual1: number;
  objetivoPercentual2: number;
  objetivoPercentual3: number;
  objetivoPercentual4: number;
  dataInicial: Date;
  dataFinal: Date;
}

export interface IObjetivoPercentualPadraoResponse extends IBaseCrudResponse {
  divisaoId: number;
  divisaoDescricao: string;
  subdivisaoId: number;
  subdivisaoDescricao: string;
  estabelecimentoId: number;
  objetivoPercentualBase: number;
  objetivoPercentual1: number;
  objetivoPercentual2: number;
  objetivoPercentual3: number;
  objetivoPercentual4: number;
  dataInicial: Date;
  dataFinal: Date;
}
