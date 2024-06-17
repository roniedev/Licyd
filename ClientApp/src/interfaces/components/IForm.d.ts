export interface IForm {
  title: string;
  fields: Array<IFormElement>;
  btnSubmitLabel?: string;
  onSubmit?: (formValues: any) => void;
}

export interface IFormElement {
  label: string;
  name: string;
  type:
    | 'text'
    | 'password'
    | 'textarea'
    | 'email'
    | 'search'
    | 'tel'
    | 'file'
    | 'number'
    | 'url'
    | 'time'
    | 'date'
    | 'select'
    | 'multiselect'
    | 'checkbox'
    | 'toggle'
    | 'checkbox'
    | 'radio';
  cols: number;
  dense: boolean;
  required: boolean;
  design?: 'standart' | 'outlined' | 'filled';
  requiredRules?: ValidationRule;
  urlRequestOption?: string;
  options?: Array<IOption>;
  labelSelect?: string;
  icon?: string;
  step?: number;
  radioInline?: boolean;
  validationRules?: ((val: any) => boolean | string)[];
}

export interface IFormProps {
  label: string;
  name: string;
  type:
    | 'text'
    | 'password'
    | 'textarea'
    | 'email'
    | 'search'
    | 'tel'
    | 'file'
    | 'number'
    | 'url'
    | 'time'
    | 'date'
    | 'select'
    | 'multiselect'
    | 'checkbox'
    | 'toggle'
    | 'checkbox'
    | 'radio';
  dense: boolean;
  newLine?: boolean;
  cols?: number;
  required: boolean;
  requiredRules?: ValidationRule;
  urlRequestOption?: string;
  options?: Array<IOption>;
  labelSelect?: string;
  labelFieldset?: string;
  step?: number;
  onFetchOptions?: () => Promise<Array<IOption>>;
}

export interface IOption {
  label: string;
  value: string | number;
}

export type ValidationRule =
  | ((val: any) => boolean | string)
  | ((val: any) => boolean | string)[];
