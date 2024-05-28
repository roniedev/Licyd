import { IFormElement } from './IFormElement';

export interface IJumpToggle extends IFormElement {
  value?: boolean;
  leftLabel?: boolean;
  inline?: boolean;
  size?: 'xs' | 'sm' | 'md' | 'lg' | 'xl';
}
