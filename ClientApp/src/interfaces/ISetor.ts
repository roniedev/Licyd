import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface ISetorRequest extends IBaseRequest {
  nivelHierarquicoId: number;
  divisaoId: number;
  subdivisaoId: number;
  setorSuperiorId?: number;
  descricao: string;
  estabelecimentoId: number;
  centroCustoId: string;
  dataVigenciaInicial: Date;
  dataVigenciaFinal?: Date;
  vagoPadraoId?: string;
  isAfetaPremiacao: boolean;
  isAfetaFaturamento: boolean;
  isVisualizarSetorMapaVendas: boolean;
  isSellIn: boolean;
}

export interface ISetorResponse extends IBaseCrudResponse {
  nivelHierarquicoId: number;
  divisaoId: number;
  subdivisaoId: number;
  setorSuperiorId?: number;
  descricao: string;
  estabelecimentoId: number;
  centroCustoId: string;
  dataVigenciaInicial: Date;
  dataVigenciaFinal?: Date;
  vagoPadraoId?: string;
  isAfetaPremiacao: boolean;
  isAfetaFaturamento: boolean;
  isVisualizarSetorMapaVendas: boolean;
  isSellIn: boolean;
}
