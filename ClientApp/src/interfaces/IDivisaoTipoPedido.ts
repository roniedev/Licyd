import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IDivisaoTipoPedidoRequest extends IBaseRequest {
  divisaoId: number;
  tipoPedidoId: number;
  estabelecimentoId: number;
  dataVigenciaInicial: Date;
  dataVigenciaFinal: Date;
}

export interface IDivisaoTipoPedidoResponse extends IBaseCrudResponse {
  divisaoId: number;
  tipoPedidoId: number;
  estabelecimentoId: number;
  dataVigenciaInicial: Date;
  dataVigenciaFinal: Date;
}
