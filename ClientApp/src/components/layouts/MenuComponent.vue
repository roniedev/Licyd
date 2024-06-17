<template>
  <q-drawer
    show-if-above
    v-model="drawer"
    side="left"
    bordered
    :width="330"
    :breakpoint="200"
  >
    <q-scroll-area class="fit">
      <q-list>
        <div id="menu-lateral">
          <!-- Nível 0 -->
          <template v-for="(menu, index) in menuUsuario" :key="index">
            <!-- Páginas nível 1 -->
            <template v-for="(pagina1, index) in menu.paginas" :key="index">
              <q-item
                dense
                clickable
                v-ripple
                class="q-item-menu item-nivel-1"
                @click="pushRoute(pagina1.componente)"
              >
                <q-item-section avatar>
                  <q-icon :name="pagina1.icone || 'navigate_next'" />
                </q-item-section>
                <q-item-section>
                  {{ pagina1.nomePagina }}
                </q-item-section>
              </q-item>
            </template>
            <!-- Menus nível 1 -->
            <template v-for="(menu1, index) in menu.menusFilhos" :key="index">
              <q-expansion-item
                dense
                :icon="menu1.icone ? menu1.icone : 'navigate_next'"
                :label="menu1.nomeMenu"
                class="q-expansion-item-menu item-nivel-1"
                :content-inset-level="0.3"
              >
                <template
                  v-for="(menu2, index) in menu1.menusFilhos"
                  :key="index"
                >
                  <q-expansion-item
                    dense
                    :label="menu2.nomeMenu"
                    :icon="menu1.icone ? menu1.icone : 'navigate_next'"
                    class="q-expansion-item-menu item-nivel-1"
                    :content-inset-level="0.3"
                  >
                    <template
                      v-for="(pagina3, index) in menu2.paginas"
                      :key="index"
                    >
                      <!-- Paginas nível 3-->
                      <q-item
                        dense
                        clickable
                        v-ripple
                        class="q-item-menu"
                        @click="pushRoute(pagina3.componente)"
                      >
                        <q-item-section avatar>
                          <q-icon :name="pagina3.icone || 'navigate_next'" />
                        </q-item-section>
                        <q-item-section>
                          {{ pagina3.nomePagina }}
                        </q-item-section>
                      </q-item>
                    </template>
                  </q-expansion-item>
                </template>
                <template
                  v-for="(pagina2, index) in menu1.paginas"
                  :key="index"
                >
                  <!-- Páginas nível 2-->
                  <q-item
                    dense
                    clickable
                    v-ripple
                    class="q-item-menu"
                    @click="pushRoute(pagina2.componente)"
                  >
                    <q-item-section avatar>
                      <q-icon :name="pagina2.icone || 'navigate_next'" />
                    </q-item-section>
                    <q-item-section>
                      {{ pagina2.nomePagina }}
                    </q-item-section>
                  </q-item>
                </template>
              </q-expansion-item>
            </template>
          </template>
        </div>
      </q-list>
    </q-scroll-area>
  </q-drawer>
</template>

<script lang="ts">
// import apiAutenticacao from 'src/api/useApiAutenticacao';
import { notificar } from 'src/helpers/Notificacao';
import { IMenuUsuario } from 'src/interfaces/IMenuUsuario.interfaces';
// import { useAutenticacaoStore } from 'src/stores/autenticacao.store';
import { defineComponent, ref, watch } from 'vue';
import { useRouter } from 'vue-router';

export default defineComponent({
  name: 'MenuComponent',
  props: {
    menuUsuario: {
      type: Array<IMenuUsuario>,
      default: [],
    },
    visible: {
      type: Boolean,
      default: true,
    },
  },

  setup(props) {
    const drawer = ref(true);
    // const menuUsuario = ref([] as IMenuUsuario[]);

    // const useAutenticacao = useAutenticacaoStore();
    const router = useRouter();

    watch(
      () => props.visible,
      (value) => {
        drawer.value = value;
      },
      {
        immediate: true,
      }
    );

    // onMounted(async () => {
    //   if (useAutenticacao.autenticado) {
    //     const response = await apiAutenticacao.post('menu/obtermenususuario', {
    //       usuarioDeRede: useAutenticacao.usuario?.usuarioMenu,
    //       idAplicacao: 2,
    //     });

    //     menuUsuario.value = response.data as IMenuUsuario[];
    //   }
    // });

    function pushRoute(componente: string) {
      router.push({ name: componente }).catch(() => {
        const mensagem =
          'A rota não está configurada corretamente no DTI0021. Favor entrar em contato com o DTI.';
        notificar(mensagem, 'warning', false, 0);
      });
    }

    return {
      drawer,
      // menuUsuario,
      pushRoute,
    };
  },
});
</script>
