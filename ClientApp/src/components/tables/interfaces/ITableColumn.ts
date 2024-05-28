import { IJumpInput } from 'src/components/forms/interfaces/IJumpInput';
import { ColumnType } from '../enums/ColumnType';
import { IJumpSelectClient } from 'src/components/forms/interfaces/IJumpSelectClient';
import { IJumpOptionGroup } from 'src/components/forms/interfaces/IJumpOptionGroup';
import { IJumpToggle } from 'src/components/forms/interfaces/IJumpToggle';
import { IJumpSelectServer } from 'src/components/forms/interfaces/IJumpSelectServer';

export interface ITableColumn {
  name: string;
  label: string;
  field: string;
  type: ColumnType;
  align?: 'left' | 'center' | 'right';
  sortable?: boolean;
  visible?: boolean;
  filtrable?: boolean;
  maxWidth?: number;
  filter?:
    | IJumpInput
    | IJumpSelectClient
    | IJumpSelectServer
    | IJumpOptionGroup
    | IJumpToggle;
}
