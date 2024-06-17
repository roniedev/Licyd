export default new (class Util {
  isDate(value: string) {
    const regex = /^\d{4}-\d{2}-\d{2}$/;

    if (!regex.test(value)) {
      return false;
    }

    const date = new Date(value);
    const isValid = !isNaN(date.getTime());

    return isValid;
  }

  isDateTime(value: string) {
    const regex = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}$/;
    const isDateTime = regex.test(value);
    return isDateTime;
  }
})();
