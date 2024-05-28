import { IPageActions } from './IPageActions';

export interface IModulePages {
  id: number;
  moduleId: number;
  name: string;
  icon: string;
  component: string;
  apiController: string;
  actions: Array<IPageActions>;
}
