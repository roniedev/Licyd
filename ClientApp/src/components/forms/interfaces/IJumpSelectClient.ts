import { IFormElement } from './IFormElement';

export interface IJumpSelectClient extends IFormElement {
  value?: string | number | boolean | null | undefined;
  options: Array<any>;
  multiselect?: boolean;
  selectedOptionLabel?: string;
  useChips?: boolean;
  optionLabel?: string;
  optionValue?: string;
  hideDropdownIcon?: boolean;
  newValueModel?: 'add' | 'add-unique' | 'toggle';
  inputDebounce?: number;
  emitValue?: boolean;
  clearable?: boolean;
  mapOptions?: boolean;
  useInput?: boolean;
  onChange?: (val: any) => void;
}
