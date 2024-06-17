import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IObjetivoPeriodoMesDivisaoRequest extends IBaseRequest {
  divisaoId: number;
  mesReferencia: number;
  anoReferencia: number;
  estabelecimentoId: number;
  dataVigenciaInicial: Date;
  dataVigenciaFinal: Date;
}

export interface IObjetivoPeriodoMesDivisaoResponse extends IBaseCrudResponse {
  divisaoId: number;
  divisaoDescricao: string;
  mesReferencia: number;
  anoReferencia: number;
  estabelecimentoId: number;
  dataVigenciaInicial: Date;
  dataVigenciaFinal: Date;
}
