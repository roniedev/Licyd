import { IFormElement } from './IFormElement';

export interface IJumpSelectServer extends IFormElement {
  urlGetOptions: string;
  value?: string | number | boolean | null | undefined;
  multiselect?: boolean;
  searchOnCharLength?: number;
  take?: number;
  options?: Array<any>;
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
  urlParams?: ISelectServerParams[];
  behavior?: 'dialog' | 'menu';
  labelNameField?: string;
  onChange?: (val: any) => void;
}

interface ISelectServerParams {
  fieldName: string;
  paramName?: string;
  value?: string;
}
