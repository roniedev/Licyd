<template>
  <q-header
    reveal
    class="q-py-sm"
    :class="$q.dark.isActive ? 'q-header--dark' : 'q-header--light'"
  >
    <q-toolbar>
      <q-btn dense flat round icon="menu" @click="onClickMenu" />
      <q-toolbar-title>
        <div class="row items-center">
          <div class="flex items-center">
            <img
              :src="logo"
              class="img-logo"
              alt="Logo Laboratório Teuto Brasileiro S/A"
            />
            <span class="q-ml-md text-h6" style="font-weight: 600">
              {{ aplicacaoCodigo }}
            </span>
          </div>
        </div>
      </q-toolbar-title>
      <q-space />
      <dark-mode-toggle style="margin-right: 5px" />
      <q-btn-dropdown flat round :label="usuarioRede">
        <q-list style="font-size: 12.5px">
          <q-item clickable v-close-popup>
            <q-item-section>
              <q-item-label>Versão 7.0</q-item-label>
            </q-item-section>
            <q-item-section avatar>
              <q-avatar icon="mdi-information-outline" />
            </q-item-section>
          </q-item>

          <q-item clickable v-close-popup @click="logout">
            <q-item-section>
              <q-item-label>Sair</q-item-label>
            </q-item-section>
            <q-item-section avatar>
              <q-avatar icon="mdi-logout" />
            </q-item-section>
          </q-item>
        </q-list>
      </q-btn-dropdown>
      <!-- <q-btn flat round size="md" icon="mdi-logout" style="margin-right: 5px" /> -->
    </q-toolbar>
  </q-header>
</template>

<script lang="ts">
import { useRouter } from 'vue-router';
import { defineComponent, ref } from 'vue';

import DarkModeToggle from './DarkModeToggle.vue';
import useAutenticacaoStore from 'src/stores/autenticacao.store';

export default defineComponent({
  emits: ['onClickMenu'],

  components: {
    DarkModeToggle,
  },

  setup(_props, { emit }) {
    const router = useRouter();
    const useAutenticacao = useAutenticacaoStore();

    const usuarioRede = ref(useAutenticacao.username);
    const aplicacaoCodigo = ref(process.env.APLICACAO_CODIGO);
    const logo = ref('img/logo-teuto.png');

    const onClickMenu = () => {
      emit('onClickMenu');
    };

    const logout = () => {
      useAutenticacao.delete();
      router.replace({ name: 'LoginPage' });
    };

    return {
      aplicacaoCodigo,
      usuarioRede,
      logo,
      logout,
      onClickMenu,
    };
  },
});
</script>

<style scoped>
.q-header {
  height: 65px;
  color: #fff;
}

.q-header--light {
  background-color: #007bff;
}

.q-header--dark {
  background-color: #2c2a31;
}

.img-logo {
  width: 20%;
  height: 20%;
}
</style>
