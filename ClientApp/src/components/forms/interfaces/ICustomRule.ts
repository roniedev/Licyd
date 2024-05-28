export interface ICustomRule {
  name: string;
  value: string | number | boolean | undefined | null | CustomRuleFunction;
  grouper?: number;
}

export type CustomRuleFunction = (value: any) => boolean;
