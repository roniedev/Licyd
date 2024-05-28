import { defineStore } from 'pinia';
import { notificate } from 'src/boot/helpers/NotificationHelper';
import { IModuloPagina } from 'src/components/menu/interfaces/IModuloPagina';
import { IUsuarioModulo } from 'src/components/menu/interfaces/IUsuarioModulo';
import { IAuthState } from 'src/interfaces/IAuthState';
import { IUser } from 'src/interfaces/IUser';

const useAutenticacaoStore = defineStore('userAuth', {
  state: (): IAuthState => ({
    user: null,
    isAuth: false,
  }),

  getters: {
    authenticated: (state) => state.isAuth,
    modules: (state) => (state.user ? state.user.modulos : []),
    routes: (state) => (state.user ? state.user.routes : []),
    username: (state) => (state.user ? state.user.userName : ''),
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

    temAcessoNaAcao(componente: string, nomeAcao: string): boolean {
      const result = this.temAcesso(
        this.user?.modulos || [],
        componente,
        nomeAcao
      );

      return result;
    },

    temAcesso(
      subModulos: IUsuarioModulo[],
      componente: string,
      nomeAcao: string
    ) {
      for (const subModulo of subModulos) {
        if (
          subModulo.paginas.some((pagina) => pagina.componente === componente)
        ) {
          if (
            subModulo.paginas.some((pagina) =>
              this.temAcessoNaPagina(pagina, nomeAcao)
            )
          ) {
            return true;
          }
        }
        if (subModulo.subModulos && subModulo.subModulos.length > 0) {
          if (this.temAcesso(subModulo.subModulos, componente, nomeAcao)) {
            return true;
          }
        }
      }
      return false;
    },

    temAcessoNaPagina(pagina: IModuloPagina, codigoAcao: string): boolean {
      if (pagina.acoes) {
        return pagina.acoes.some((acao) => acao.codigo === codigoAcao);
      }
      return false;
    },
  },
});

export default useAutenticacaoStore;
