import { IFormElement } from './IFormElement';

export interface IJumpInputFile extends IFormElement {
  value?: any;
  useChips?: boolean;
  lazyRules?: boolean;
}
