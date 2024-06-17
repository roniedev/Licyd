export function fieldIsRequired() {
  return function (val: number | string | null | undefined): boolean | string {
    return val !== null && val !== undefined && val !== ''
      ? true
      : 'O campo é obrigatório';
  };
}

export function fieldMaxLength(maxLength: number) {
  return function (val: number | string | null | undefined): boolean | string {
    if (val) {
      if (typeof val !== 'string') {
        return 'O valor deve ser uma string';
      }

      return (
        val.length <= maxLength ||
        `O valor deve ter no máximo ${maxLength} caracteres`
      );
    }

    return true;
  };
}
