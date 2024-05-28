import { Notify } from 'quasar';

export function notificate(
  message: string,
  type: 'positive' | 'negative' | 'warning' | 'info' | 'ongoing' = 'positive',
  position?:
    | 'bottom'
    | 'bottom-left'
    | 'bottom-right'
    | 'center'
    | 'left'
    | 'right'
    | 'top'
    | 'top-left'
    | 'top-right',
  timeout = 10000,
  isHtml = false,
  multiLine = false
) {
  Notify.create({
    message: message,
    multiLine: multiLine,
    position: position || 'top',
    progress: true,
    timeout: timeout,
    type: type,
    html: isHtml,
    actions: [{ icon: 'close', color: 'white', round: true }],
  });
}
