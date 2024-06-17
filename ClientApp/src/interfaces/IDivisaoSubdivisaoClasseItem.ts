import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IDivisaoSubdivisaoClasseItemRequest extends IBaseRequest {
  divisaoId: number;
  subdivisaoId: number;
  classeItemId: string;
  estabelecimentoId: number;
  dataVigenciaInicial: Date;
  dataVigenciaFinal?: Date;
}

export interface IDivisaoSubdivisaoClasseItemResponse
  extends IBaseCrudResponse {
  divisaoId: number;
  subdivisaoId: number;
  classeItemId: string;
  estabelecimentoId: number;
  dataVigenciaInicial: Date;
  dataVigenciaFinal?: Date;
}
