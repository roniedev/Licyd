import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IPedidoCanceladoRequest extends IBaseRequest {
  pedidoId: number;
  divisaoId: number;
  subdivisaoId: number;
  representanteId: number;
  clienteId: number;
  setorId: number;
  estabelecimentoId: number;
  dataPedido: Date;
  dataCancelamento: Date;
  anoReferencia: number;
  mesReferencia: number;
  valorPedido: number;
  motivoCancelamento: string;
  situacao: string;
  Atendente: string;
}

export interface IPedidoCanceladoResponse extends IBaseCrudResponse {
  pedidoId: number;
  divisaoId: number;
  divisaoDescricao: string;
  subdivisaoId: number;
  subdivisaoDescricao: string;
  representanteId: number;
  representanteDescricao: string;
  clienteId: number;
  clienteDescricao: number;
  setorId: number;
  setorDescricao: number;
  estabelecimentoId: number;
  dataPedido: Date;
  dataCancelamento: Date;
  anoReferencia: number;
  mesReferencia: number;
  valorPedido: number;
  motivoCancelamento: string;
  situacao: string;
  atendente: string;
}
