<template>
  <q-header
    elevated
    class="text-white q-py-sm"
    height-hint="58"
    style="background-color: #1aa4e0"
  >
    <q-toolbar>
      <q-btn dense flat round icon="menu" @click="$emit('actionButtonMenu')" />
      <q-toolbar-title shrink class="text-weight-bold">
        <div class="row items-center">
          <div class="flex items-center">
            <img
              :src="logoTeuto"
              alt="Logo Teuto"
              style="width: 20%; height: 20%"
            />
            <span class="q-ml-md text-h6" style="font-weight: 500"
              >CML0036</span
            >
          </div>
        </div>
      </q-toolbar-title>
      <q-space />
      <span style="margin-right: 10px; font-size: 13.5px">{{
        usuarioRede
      }}</span>
      <q-btn flat round size="md" icon="mdi-logout" @click="logout" />
    </q-toolbar>
  </q-header>
</template>

<script lang="ts">
import useAutenticacaoStore from 'src/stores/autenticacao.store';
import { defineComponent, ref } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'HeaderComponent',

  data: () => ({
    logoTeuto: 'img/logo-header.png',
  }),

  setup() {
    const router = useRouter();
    const useAutenticacao = useAutenticacaoStore();
    const usuarioRede = ref(useAutenticacao.username);

    function logout() {
      useAutenticacao.delete();
      router.replace({ name: 'LoginPage' });
    }

    return {
      usuarioRede,
      logout,
    };
  },
});
</script>
