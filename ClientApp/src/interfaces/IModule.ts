import { IModulePages } from './IModulePages';

export interface IModule {
  id: number;
  name: string;
  icon: string;
  subModules: Array<IModule>;
  pages: Array<IModulePages>;
}
