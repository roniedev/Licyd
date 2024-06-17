import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IPedidoEnviadoClienteRequest extends IBaseRequest {
  clienteId: number;
  divisaoId: number;
  subdivisaoId: number;
  representanteId: number;
  dataPedido: Date;
  mesReferencia: number;
  anoReferencia: number;
  setorId: number;
  estabelecimentoId: number;
  valorPedido: number;
  valorFaturado: number;
  empenhoId: string;
  pedidoId: string;
  atendente: string;
  observacao: string;
}

export interface IPedidoEnviadoClienteResponse extends IBaseCrudResponse {
  clienteId: number;
  clienteDescricao: string;
  divisaoId: number;
  divisaoDescricao: string;
  subdivisaoId: number;
  subdivisaoDescricao: string;
  representanteId: number;
  representanteDescricao: string;
  dataPedido: Date;
  mesReferencia: number;
  anoReferencia: number;
  setorId: number;
  setorDescricao: string;
  estabelecimentoId: number;
  valorPedido: number;
  valorFaturado: number;
  empenhoId: string;
  pedidoId: string;
  atendente: string;
  observacao: string;
}
