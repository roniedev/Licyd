<template>
  <q-drawer v-model="drawer" show-if-above style="width: 1000px">
    <q-scroll-area class="fit">
      <q-list>
        <div id="menu-lateral">
          <div class="logo-menu flex justify-center items-center">
            <img src="img/logo-branca.png" />
          </div>
          <div class="flex justify-center">
            <q-input
              v-model="filtroMenu"
              @update:model-value="(val) => $.emit('buscarMenu', val)"
              label="buscar página"
              dense
              outlined
            >
              <template v-slot:append>
                <q-icon name="mdi-magnify" />
              </template>
            </q-input>
          </div>
          <br />
          <!-- Nível 0 -->
          <template v-for="(menu, index) in modulos" :key="index">
            <!-- Páginas nível 1 -->
            <template v-for="(pagina1, index) in menu.paginas" :key="index">
              <q-item
                dense
                clickable
                v-ripple
                class="q-item-menu item-nivel-1 q-ma-xs"
                @click="pushRoute(pagina1.componente)"
              >
                <q-item-section avatar>
                  <q-icon :name="pagina1.icone || 'navigate_next'" />
                </q-item-section>
                <q-item-section>
                  {{ pagina1.nome }}
                </q-item-section>
              </q-item>
            </template>
            <!-- Menus nível 1 -->
            <template v-for="(menu1, index) in menu.subModulos" :key="index">
              <q-expansion-item
                dense
                :icon="menu1.icone ? menu1.icone : 'navigate_next'"
                :label="menu1.nome"
                class="q-expansion-item-menu item-nivel-1 q-ma-xs"
                :content-inset-level="0.1"
              >
                <template
                  v-for="(menu2, index) in menu1.subModulos"
                  :key="index"
                >
                  <q-expansion-item
                    dense
                    :label="menu2.nome"
                    :icon="menu2.icone ? menu2.icone : 'navigate_next'"
                    class="q-expansion-item-menu item-nivel-1 q-ma-xs"
                    :content-inset-level="0.1"
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
                        class="q-item-menu q-ma-xs"
                        @click="pushRoute(pagina3.componente)"
                      >
                        <q-item-section avatar>
                          <q-icon :name="pagina3.icone || 'navigate_next'" />
                        </q-item-section>
                        <q-item-section>
                          {{ pagina3.nome }}
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
                    class="q-item-menu q-ma-xs"
                    @click="pushRoute(pagina2.componente)"
                  >
                    <q-item-section avatar>
                      <q-icon :name="pagina2.icone || 'navigate_next'" />
                    </q-item-section>
                    <q-item-section>
                      {{ pagina2.nome }}
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
import { defineComponent, ref, watch } from 'vue';
import { useRouter } from 'vue-router';
import { notificate } from 'src/boot/helpers/NotificationHelper';
import { IUsuarioModulo } from './interfaces/IUsuarioModulo';

export default defineComponent({
  name: 'MenuComponent',
  props: {
    modulos: {
      type: Array<IUsuarioModulo>,
      default: [],
    },
    visible: {
      type: Boolean,
      default: true,
    },
  },
  emits: ['buscarMenu'],

  setup(props) {
    const drawer = ref(true);
    const router = useRouter();
    const filtroMenu = ref(null);

    watch(
      () => props.visible,
      (value) => {
        drawer.value = value;
      },
      {
        immediate: true,
      }
    );

    function pushRoute(componente: string) {
      router.push({ name: componente }).catch(() => {
        const mensagem =
          'A rota não está configurada corretamente, favor entrar em contato com o suporte.';
        notificate(mensagem, 'warning', 'bottom-left', 6000);
      });
    }

    return {
      drawer,
      filtroMenu,
      pushRoute,
    };
  },
});
</script>

<style lang="sass">
.q-drawer
  background-color: #004170
  color: #FAFAFA

  img
    width: 45%
    height: 45%

  .logo-menu
    margin-top: 10px
    margin-bottom: 30px

  .q-field--outlined
    background-color: $white
    border-radius: 8px
    border: none
</style>
