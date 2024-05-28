export interface ITableAction {
  name: string;
  icon?: string;
  onClick: (row: any) => void;
  visible?: boolean | ((row: any) => boolean);
}
