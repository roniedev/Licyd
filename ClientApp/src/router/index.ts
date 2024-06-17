import { route } from 'quasar/wrappers';
import {
  createMemoryHistory,
  createRouter,
  createWebHashHistory,
  createWebHistory,
} from 'vue-router';

import routes from './routes';
import { notificate } from 'src/helpers/NotificationHelper';
import RouterService from 'src/services/RouterService';
import useAutenticacaoStore from 'src/stores/autenticacao.store';

/*
 * If not building with SSR mode, you can
 * directly export the Router instantiation;
 *
 * The function below can be async too; either use
 * async/await or return a Promise which resolves
 * with the Router instance.
 */

export default route(function (/* { store, ssrContext } */) {
  const createHistory = process.env.SERVER
    ? createMemoryHistory
    : process.env.VUE_ROUTER_MODE === 'history'
    ? createWebHistory
    : createWebHashHistory;

  const Router = createRouter({
    scrollBehavior: () => ({ left: 0, top: 0 }),
    routes,

    // Leave this as is and make changes in quasar.conf.js instead!
    // quasar.conf.js -> build -> vueRouterMode
    // quasar.conf.js -> build -> publicPath
    history: createHistory(process.env.VUE_ROUTER_BASE),
  });

  Router.beforeEach((to, _from, next) => {
    const loginPage = 'LoginPage';
    const useAutenticacao = useAutenticacaoStore();

    const redirecionarusuarioLogado = () => {
      to.name === loginPage ? next({ name: 'IndexPage' }) : next();
    };

    const redirecionarusuarioNaoLogado = () => {
      to.name === loginPage ? next() : next({ name: loginPage });
    };

    try {
      if (useAutenticacao.authenticated) {
        redirecionarusuarioLogado();
        return;
      }

      const localStorageKey = process.env.LOCALSTORAGE_KEY;

      if (!localStorageKey) {
        notificate(
          'Não foram encontrados os dados de autenticação, favor realizar login novamente!',
          'negative',
          'top-left',
          0
        );

        useAutenticacao.delete();
        next({ name: loginPage });
      } else {
        const json = localStorage.getItem(localStorageKey || '');
        const routerService = new RouterService('menu', Router);
        const usuario = json ? JSON.parse(json) : undefined;

        if (usuario) {
          useAutenticacao.insert(usuario);
          routerService.removerRotasSemPermisao(useAutenticacao.routes);
          redirecionarusuarioLogado();
          return;
        }
      }

      redirecionarusuarioNaoLogado();
    } catch (error) {
      useAutenticacao.delete();
      redirecionarusuarioNaoLogado();
    }
  });

  return Router;
});
