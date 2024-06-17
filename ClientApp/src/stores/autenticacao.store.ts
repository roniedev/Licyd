import { defineStore } from 'pinia';
import { notificate } from 'src/helpers/NotificationHelper';
import { IPageMenu } from 'src/components/menu/interfaces/IPageMenu';
import { IUserMenu } from 'src/components/menu/interfaces/IUserMenu';
import { IAuthState } from 'src/interfaces/IAuthState';
import { IUser } from 'src/interfaces/IUser';

const useAutenticacaoStore = defineStore('userAuth', {
  state: (): IAuthState => ({
    user: null,
    isAuth: false,
  }),

  getters: {
    authenticated: (state) => state.isAuth,
    modules: (state) => (state.user ? state.user.usuarioMenu : []),
    routes: (state) => (state.user ? state.user.paginasAplicacao : []),
    username: (state) => (state.user ? state.user.usuarioRede : ''),
    name: (state) => (state.user ? state.user.nome : ''),
    token: (state) => (state.user ? state.user.token : ''),
    localStorageKey: () => process.env.LOCALSTORAGE_KEY || '',
  },

  actions: {
    delete() {
      if (this.isLocalStorageKeyValid()) {
        this.user = null;
        this.isAuth = false;
        localStorage.removeItem(this.localStorageKey);
      }
    },

    insert(user: IUser) {
      if (this.isLocalStorageKeyValid()) {
        this.user = user;
        this.isAuth = true;
        localStorage.setItem(this.localStorageKey, JSON.stringify(this.user));
      }
    },

    getAuthenticatedUser(): IUser | null {
      if (this.isLocalStorageKeyValid()) {
        const item = localStorage.getItem(this.localStorageKey);
        if (!item) return null;
        return JSON.parse(item) as IUser;
      }

      return null;
    },

    isLocalStorageKeyValid(): boolean {
      const isValid = this.localStorageKey?.length > 0;

      if (!isValid) {
        notificate(
          'A chave do local storage não está configurada, favor entrar em contato com o DTI.',
          'negative',
          'bottom-left',
          7000
        );
      }

      return isValid;
    },

    validarPaginaAcao(componente: string, nomeAcao: string): boolean {
      const result = this.temAcesso(
        this.user?.usuarioMenu || [],
        componente,
        nomeAcao
      );

      return result;
    },

    temAcesso(subMenus: IUserMenu[], componente: string, nomeAcao: string) {
      for (const submenu of subMenus) {
        if (
          submenu.paginas.some((pagina) => pagina.componente === componente)
        ) {
          if (
            submenu.paginas.some((pagina) =>
              this.temAcessoNaPagina(pagina, nomeAcao)
            )
          ) {
            return true;
          }
        }
        if (submenu.menusFilhos && submenu.menusFilhos.length > 0) {
          if (this.temAcesso(submenu.menusFilhos, componente, nomeAcao)) {
            return true;
          }
        }
      }
      return false;
    },

    temAcessoNaPagina(pagina: IPageMenu, nomeAcao: string): boolean {
      if (pagina.acoes) {
        return pagina.acoes.some((acao) => acao.nome === nomeAcao);
      }
      return false;
    },
  },
});

export default useAutenticacaoStore;
