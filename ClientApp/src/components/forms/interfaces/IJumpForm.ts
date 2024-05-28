import { ICustomRule } from './ICustomRule';
import { IJumpInput } from './IJumpInput';
import { IJumpInputFile } from './IJumpInputFile';
import { IJumpOptionGroup } from './IJumpOptionGroup';
import { IJumpSelectClient } from './IJumpSelectClient';
import { IJumpSelectServer } from './IJumpSelectServer';
import { IJumpToggle } from './IJumpToggle';

export interface IJumpForm {
  fields: Array<
    | IJumpInput
    | IJumpSelectClient
    | IJumpSelectServer
    | IJumpInputFile
    | IJumpOptionGroup
    | IJumpToggle
  >;
  title?: string;
  data?: any;
  showActions?: boolean;
  labelSubmitButton?: string;
  iconSubmitButton?: string;
  showSubmitButton?: boolean;
  useToolipOnSubmitButton?: boolean;
  tooltipTextSubmitButton?: string;
  labelCancelButton?: string;
  iconCancelButton?: string;
  showCancelButton?: boolean;
  useToolipOnCancelButton?: boolean;
  tooltipTextCancelButton?: string;
  disabled?: boolean;
  disabledRules?: Array<ICustomRule>;
  cols?: number;
}
