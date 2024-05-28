import { IModule } from './IModule';
import { IModulePages } from './IModulePages';

export interface IUserModules {
  modules: Array<IModule>;
  pages: Array<IModulePages>;
}
