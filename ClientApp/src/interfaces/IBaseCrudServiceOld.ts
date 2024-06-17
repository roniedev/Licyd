import { ITableFilter } from 'src/components/tables/interfaces/ITableFilter';
import { IBaseEntity } from './IBaseEntity';
import { IListarTodosRequest, IListarTodosResponse } from './IListarTodos';
import { IQueryFiltro } from './components/IFiltro';
import { IBaseInactivate } from 'src/components/tables/interfaces/IBaseInactivate';
import { IGetAllRequest } from 'src/components/tables/interfaces/IGetAllRequest';
import { IGetAllResponse } from 'src/components/tables/interfaces/IGetAllResponse';

export interface IBaseRequest {}
export interface IBaseResponse {}

export interface IBaseCrudResponse extends IBaseResponse {
  id: number;
  status?: number;
  statusDescricao?: string;
  criadoPor: string;
  criadoEm: string;
  atualizadoPor: string;
  atualizadoEm: Date;
}

export interface IBaseInativacao {
  id: number;
  motivoInativacao: string;
}

export interface IBaseCrudServiceOld {
  ativar(id: number): Promise<boolean>;
  atualizar(item: IBaseRequest): Promise<IBaseResponse | null>;
  clonar(id: number): Promise<IBaseResponse | null>;
  excluir(id: number): Promise<boolean>;
  exportar(filtros: Array<IQueryFiltro>, nomeArquivo: string): Promise<void>;
  inativar(item: IBaseInativacao): Promise<boolean>;
  inserir(item: IBaseRequest): Promise<IBaseResponse | null>;
  inserirViaImportacao(item: any, url?: string): Promise<void>;
  obterInserirLayout(): Promise<void>;
  obterPorId(id: number): Promise<IBaseCrudResponse | null>;
  obterTodos(data: IListarTodosRequest): Promise<IListarTodosResponse | null>;

  activate(id: number): Promise<boolean>;
  update(item: IBaseEntity): Promise<any | null>;
  delete(id: string): Promise<boolean>;
  export(filters: Array<ITableFilter>, filename?: string): Promise<void>;
  inactivate(item: IBaseInactivate): Promise<boolean>;
  insert(item: any): Promise<any | null>;
  getById(id: number): Promise<any | null>;
  getAll(data: IGetAllRequest): Promise<IGetAllResponse | null>;
}
