import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface IDivisaoRequest extends IBaseRequest {
  descricao: string;
  transfereVendedorSetor: boolean;
  transfereVendedorSetorInativo: boolean;
  verClienteRelatorioAcompanhamentoVendas: boolean;
}

export interface IDivisaoResponse extends IBaseCrudResponse {
  descricao: string;
  transfereVendedorSetor: boolean;
  transfereVendedorSetorInativo: boolean;
  verClienteRelatorioAcompanhamentoVendas: boolean;
}
