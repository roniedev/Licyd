export interface IButtonDropdown {
  items: IButtonDropdownItem[];
  label?: string;
  icon?: string;
  visible?: boolean | ((() => void) & ((data: any) => boolean));
  dense?: boolean;
  flat?: boolean;
}

export interface IButtonDropdownItem {
  label: string;
  icon?: string;
  onClick: (() => void) & ((data: any) => void);
  visible?: boolean | ((() => void) & ((data: any) => boolean));
}
