import { IBaseCrudResponse, IBaseRequest } from './IBaseCrudServiceOld';

export interface ISetorCadastroLxRequest extends IBaseRequest {
  setorId: number;
  codigoLX: number;
  matricula?: string;
  dataVigenciaInicial: Date;
  dataVigenciaFinal: Date;
}

export interface ISetorCadastroLxResponse extends IBaseCrudResponse {
  setorId: number;
  codigoLX: number;
  matricula?: string;
  dataVigenciaInicial: Date;
  dataVigenciaFinal: Date;
}
