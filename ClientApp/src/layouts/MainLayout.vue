<template>
  <q-layout view="lHh Lpr lFf" class="q-pa-sm">
    <header-page @onClickMenu="toggleMenu" />
    <menu-page
      :modulos="modulos"
      :visible="isMenuVisivel"
      v-if="menuVisivelNoLayout"
    />
    <q-page-container>
      <div id="main-content">
        <router-view />
      </div>
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue';

import useAutenticacaoStore from 'src/stores/auth-store';
import MenuPage from 'src/components/menu/MenuPage.vue';
import HeaderPage from 'src/components/header/HeaderPage.vue';
import { IUsuarioModulo } from 'src/components/menu/interfaces/IUsuarioModulo';

export default defineComponent({
  name: 'MainLayout',

  components: {
    MenuPage,
    HeaderPage,
  },

  setup() {
    const loginStore = useAutenticacaoStore();
    const isMenuVisivel = ref(true);
    const modulos = ref<IUsuarioModulo[]>([]);
    const useAutenticacao = useAutenticacaoStore();
    const menuVisivelNoLayout = !loginStore.user?.isHospede;

    function toggleMenu() {
      isMenuVisivel.value = !isMenuVisivel.value;
    }

    onMounted(() => {
      if (useAutenticacao.authenticated) {
        modulos.value = useAutenticacao.modules;
      }
    });

    return {
      usuarioLogado: loginStore.getAuthenticatedUser()?.nome,
      nomeApp: process.env.NOME_APLICACAO,
      logoTeuto: '/img/logo-teuto.png',
      isMenuVisivel,
      modulos,
      menuVisivelNoLayout,
      toggleMenu,
    };
  },
});
</script>
