import { IFormElement } from './IFormElement';

export interface IJumpInput extends IFormElement {
  value?: string | number | boolean | null | undefined;
  step?: number;
  mask?: string;
  unmaskedValue?: boolean;
  reverseFillMask?: boolean;
  fillMask?: string;
  lazyRules?: boolean;
}
