<template>
  <q-drawer
    v-model="drawerVisible"
    side="left"
    show-if-above
    behavior="desktop"
    :width="350"
    :breakpoint="200"
    :class="$q.dark.isActive ? 'q-drawer--dark' : 'q-drawer--light'"
  >
    <q-scroll-area class="fit">
      <q-list>
        <div id="menu-lateral">
          <div class="q-mt-md q-mr-lg q-ml-lg">
            <q-input
              v-model="filtroMenu"
              @update:model-value="(val) => $.emit('buscarMenu', val)"
              dense
              rounded
              outlined
              :class="$q.dark.isActive ? '' : 'q-field--outlined--light'"
            >
              <template v-slot:append>
                <q-icon name="mdi-magnify" />
              </template>
            </q-input>
          </div>
          <br />
          <!-- Nível 0 -->
          <template v-for="(menu, index) in itens" :key="index">
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
import { useRouter } from 'vue-router';
import { defineComponent, ref, watch } from 'vue';
import { notificate } from 'src/helpers/NotificationHelper';
import { IMenuUsuario } from 'src/interfaces/IMenuUsuario.interfaces';

export default defineComponent({
  props: {
    itens: {
      type: Array<IMenuUsuario>,
      default: [],
    },
    visivel: {
      type: Boolean,
      default: true,
    },
  },

  setup(props) {
    const router = useRouter();
    const filtroMenu = ref(null);
    const drawerVisible = ref(true);

    watch(
      () => props.visivel,
      (value) => {
        drawerVisible.value = value;
      },
      {
        immediate: true,
      }
    );

    const pushRoute = (componente: string) => {
      router.push({ name: componente }).catch(() => {
        const mensagem =
          'A rota não está configurada corretamente no DTI0021. Favor entrar em contato com o DTI.';
        notificate(mensagem, 'warning', 'top', 5000);
      });
    };

    return {
      pushRoute,
      filtroMenu,
      drawerVisible,
    };
  },
});
</script>

<style>
#menu-lateral {
  font-size: 12.2px;
  padding: 0 15px 15px 15px;
}

#menu-lateral {
  .q-icon {
    font-size: 15px;
  }
}

#menu-lateral {
  .q-item {
    border-radius: 5px;
    padding: 7.5px;
  }
}

#menu-lateral {
  .q-item:hover {
    border-radius: 5px;
    padding: 7.5px;
    color: #007bff;
  }
}

#menu-lateral {
  .q-expansion-item__content {
    margin-left: 5px;
    margin-top: 5px;
    margin-bottom: 5px;
  }
}

.q-drawer {
  border: none;
  font-weight: 400;
  color: #212121;
}

.q-drawer--light {
  background-color: #f0f0f0;
}

.q-drawer--dark {
  background-color: #242226;
}

img {
  width: 35%;
  height: 35%;
}

.logo-menu {
  margin-bottom: 15px;
}
.q-field--outlined {
  border-radius: 100px;
}

.q-field--outlined--light {
  background-color: #fff;
  border-radius: 100px;
}
</style>
