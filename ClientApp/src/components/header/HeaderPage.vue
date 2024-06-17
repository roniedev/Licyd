<template>
  <q-header class="text-dark q-py-sm" height-hint="90">
    <q-toolbar>
      <q-btn dense flat round icon="mdi-backburger" @click="handleMenuClick" />
      <q-toolbar-title shrink class="text-weight-bold">
        <div class="row items-center">
          <div class="flex items-center">
            <span class="q-ml-md">{{ aplicacaoCodigo }}</span>
          </div>
        </div>
      </q-toolbar-title>
      <q-space />
      <span class="usuarioLogado">{{ username }}</span>
      <q-btn flat round size="md" icon="mdi-logout" @click="logout" />
    </q-toolbar>
  </q-header>
</template>

<script lang="ts">
import useAutenticacaoStore from 'src/stores/autenticacao.store';
import { defineComponent, ref } from 'vue';

export default defineComponent({
  name: 'HeaderComponent',

  data: () => ({
    logoApp: 'img/logo-header.png',
  }),

  emits: ['onClickMenu'],

  setup(_props, { emit }) {
    const useAutenticacao = useAutenticacaoStore();

    const aplicacaoCodigo = process.env.APLICACAO_CODIGO;
    const username = ref(useAutenticacao.username);

    function logout() {
      useAutenticacao.delete();
      window.location.reload();
    }

    function handleMenuClick() {
      emit('onClickMenu');
    }

    return {
      username,
      aplicacaoCodigo,
      handleMenuClick,
      logout,
    };
  },
});
</script>

<style lang="sass" scoped>
.q-header
  background-color: #FFF

  img
    width: 15%
    height: 15%

  .usuarioLogado
    margin-right: 10px
    font-size: 16.5px
</style>
