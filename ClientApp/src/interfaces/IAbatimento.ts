import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IAbatimentoRequest extends IBaseRequest {
  divisaoId: number;
  subdivisaoId: number;
  setorId: number;
  motivoAbatimentoId: number;
  tipoDescontoId: number;
  representanteId: number;
  clienteId: number;
  pedidoId: number;
  estabelecimentoId: number;
  centroCustoId: string;
  tipoOperacaoId: number;
  anoReferencia: number;
  mesReferencia: number;
  valorFaturamento: number;
  percentualDesconto: number;
  notaFiscalSaida?: number;
  serieNotaFiscalSaida?: number;
  notaFiscalDevolucao: number;
  atendente: number;
  observacao: string;
}

export interface IAbatimentoResponse extends IBaseCrudResponse {
  divisaoId: number;
  divisaoDescricao: string;
  subdivisaoId: number;
  subdivisaoDescricao: number;
  setorId: number;
  setorDescricao: string;
  motivoAbatimentoId: number;
  tipoDescontoId: number;
  tipoDescontoDescricao: number;
  representanteId: number;
  representanteDescricao: string;
  clienteId: number;
  clienteDescricao: string;
  pedidoId: number;
  estabelecimentoId: number;
  centroCustoId: string;
  centroCustoDescricao: string;
  tipoOperacaoId: number;
  tipoOperacaoDescricao: string;
  anoReferencia: number;
  mesReferencia: number;
  valorFaturamento: number;
  percentualDesconto: number;
  notaFiscalSaida?: number;
  serieNotaFiscalSaida?: number;
  notaFiscalDevolucao: number;
  atendente: number;
  observacao: string;
}
