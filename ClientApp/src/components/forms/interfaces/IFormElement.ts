import { FormElementType } from '../enums/FormElementType';
import { ICustomRule } from './ICustomRule';

export interface IFormElement {
  name: string;
  label?: string;
  type?: FormElementType;
  style?: 'outlined' | 'standout' | 'filled';
  dense?: boolean;
  borderless?: boolean;
  rounded?: boolean;
  square?: boolean;
  stackLabel?: boolean;
  beforeIcon?: string;
  appendIcon?: string;
  prependIcon?: string;
  cols?: number;
  validationRules?: ((
    val: number | string | null | undefined
  ) => boolean | string)[];
  readonly?: boolean;
  readonlyRules?: Array<ICustomRule>;
  disabled?: boolean;
  disabledRules?: Array<ICustomRule>;
  visible?: boolean;
  visibleRules?: Array<ICustomRule>;
  hideBottomSpace?: boolean;
  hint?: string;
  putFieldInNewLine?: boolean;
  hidden?: boolean;
  onBlur?: (val: any) => void;
  onFocus?: (val: any) => void;
}
