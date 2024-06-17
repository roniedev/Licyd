import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IObjetivoPeriodoMesSubdivisaoRequest extends IBaseRequest {
  divisaoId: number;
  subdivisaoId: number;
  anoReferencia: number;
  mesReferencia: number;
  estabelecimentoId: number;
  dataVigenciaInicial: Date;
  dataVigenciaFinal: Date;
}

export interface IObjetivoPeriodoMesSubdivisaoResponse
  extends IBaseCrudResponse {
  divisaoId: number;
  divisaoDescricao: string;
  subdivisaoId: number;
  subdivisaoDescricao: string;
  anoReferencia: number;
  mesReferencia: number;
  estabelecimentoId: number;
  dataVigenciaInicial: Date;
  dataVigenciaFinal: Date;
}
