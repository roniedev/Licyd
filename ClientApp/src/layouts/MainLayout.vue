<template>
  <q-layout view="hHh lpR fFf" class="q-pa-sm">
    <!-- <header-page @onClickMenu="toggleMenu" /> -->
    <header-layout @on-click-menu="toggleMenu" />
    <sidebar-layout
      :itens="menuAplicacao"
      :visivel="isMenuVisivel"
      @buscarMenu="filtrarMenu"
    />
    <!-- <menu-page :menu-usuario="menuAplicacao" :visible="isMenuVisivel" /> -->
    <q-page-container>
      <div id="main-content">
        <router-view />
      </div>
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">
import { IUserMenu } from 'src/components/menu/interfaces/IUserMenu';
import { defineComponent, onMounted, ref } from 'vue';

// import MenuPage from 'src/components/menu/MenuPage.vue';
// import HeaderPage from 'src/components/header/HeaderPage.vue';
import HeaderLayout from './HeaderLayout.vue';
import SidebarLayout from './SidebarLayout.vue';
import useAutenticacaoStore from 'src/stores/autenticacao.store';

export default defineComponent({
  name: 'MainLayout',

  components: {
    // MenuPage,
    // HeaderPage,
    HeaderLayout,
    SidebarLayout,
  },

  setup() {
    const loginStore = useAutenticacaoStore();
    const isMenuVisivel = ref(true);
    const menuAplicacao = ref<Array<IUserMenu>>([]);
    const useAutenticacao = useAutenticacaoStore();

    function toggleMenu() {
      isMenuVisivel.value = !isMenuVisivel.value;
    }

    function filtrarMenu(filtro: string) {
      menuAplicacao.value = useAutenticacao.modules;

      const menus = useAutenticacao.modules;

      menus.forEach((menu) => {
        if (menu.menusFilhos.length > 0) {
          menu = filtrarMenusFilhos(menu, filtro.toLowerCase().trim());
        }
      });

      menuAplicacao.value = menus;
    }

    function filtrarMenusFilhos(menuPai: IUserMenu, filtro: string) {
      if (menuPai.menusFilhos.length > 0) {
        menuPai.menusFilhos.forEach((menu) => {
          const menuFiltrado = filtrarMenusFilhos(menu, filtro);
          menu = menuFiltrado;
        });

        menuPai.menusFilhos = menuPai.menusFilhos.filter(
          (x) =>
            x.paginas.some((pagina) =>
              pagina.nomePagina.toLowerCase().trim().includes(filtro)
            ) || x.menusFilhos.length > 0
        );
      }

      menuPai.paginas = menuPai.paginas.filter((pagina) =>
        pagina.nomePagina.toLowerCase().trim().includes(filtro)
      );

      return menuPai;
    }

    onMounted(() => {
      if (useAutenticacao.authenticated) {
        menuAplicacao.value = useAutenticacao.modules;
      }
    });

    return {
      usuarioLogado: loginStore.user?.nome,
      nomeApp: process.env.APLICACAO_NOME,
      logoTeuto: '/img/logo-teuto.png',
      isMenuVisivel,
      menuAplicacao,
      toggleMenu,
      filtrarMenu,
    };
  },
});
</script>
