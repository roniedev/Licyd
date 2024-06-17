import { Notify } from 'quasar';

export function notificar(
  message: string,
  type: 'positive' | 'negative' | 'warning' | 'info' | 'ongoing' = 'positive',
  multiLine = false,
  timeout = 10000,
  position?:
    | 'bottom'
    | 'bottom-left'
    | 'bottom-right'
    | 'center'
    | 'left'
    | 'right'
    | 'top'
    | 'top-left'
    | 'top-right'
) {
  Notify.create({
    message: message,
    multiLine: multiLine,
    position: position || 'bottom-left',
    progress: true,
    timeout: timeout,
    type: type,
    html: true,
    actions: [{ icon: 'close', color: 'white', round: true }],
  });
}
