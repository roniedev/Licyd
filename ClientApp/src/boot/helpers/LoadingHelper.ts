import { Loading, QSpinnerHourglass } from 'quasar';

export function showLoading() {
  Loading.show({
    spinner: QSpinnerHourglass,
  });
}

export function hideLoading() {
  Loading.hide();
}
