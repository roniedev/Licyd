import { IFormElement } from './IFormElement';

export interface IJumpOptionGroup extends IFormElement {
  options: Array<any>;
  value?: Array<string> | any;
  leftLabel?: boolean;
  inline?: boolean;
  size?: 'xs' | 'sm' | 'md' | 'lg' | 'xl';
}
