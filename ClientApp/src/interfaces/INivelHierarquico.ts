import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface INivelHierarquicoRequest extends IBaseRequest {
  descricao: string;
  sequenciaHierarquica: number;
  validarAssociacaoLX: boolean;
}

export interface INivelHierarquicoResponse extends IBaseCrudResponse {
  descricao: string;
  sequenciaHierarquica: number;
  validarAssociacaoLX: boolean;
}
