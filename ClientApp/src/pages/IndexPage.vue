<template>
  <div class="row q-mb-sm" v-if="true">
    <div class="col">
      <q-card flat>
        <q-tabs
          v-model="tab"
          dense
          class="text-grey"
          active-color="primary"
          indicator-color="primary"
          align="justify"
          narrow-indicator
        >
          <q-tab name="sanduiches" label="Sanduiches" />
          <q-tab name="bebidas" label="Bebidas" />
          <q-tab name="pizzas" label="Pizzas" />
        </q-tabs>

        <q-separator />

        <q-tab-panels v-model="tab" animated>
          <q-tab-panel name="sanduiches">
            <div class="row">
              <div class="col">
                <q-card
                  flat
                  bordered
                  v-ripple
                  class="cursor-pointer q-hoverable q-mb-xs"
                  :onclick="mostrarOpcionais"
                >
                  <q-card-section horizontal>
                    <q-card-section class="flex flex-center">
                      <q-img
                        class="rounded-borders"
                        src="https://dummyimage.com/200x200/b5b5b5/fff.jpg"
                        style="height: 100px; width: 100px"
                      />
                    </q-card-section>
                    <div class="flex flex-center">
                      <q-card-section vertical>
                        <div
                          class="q-mb-sm text-title text-bold"
                          style="font-size: 16px"
                        >
                          Franguitos
                        </div>
                        <div class="q-mb-sm text-caption text-grey">
                          Pão,filé de frango, maionese, batata palha,tomate,
                          alface.
                        </div>
                        <div
                          class="text-orange text-bold"
                          style="font-size: 14px"
                        >
                          R$ 14,50
                        </div>
                      </q-card-section>
                    </div>
                    <q-space />
                  </q-card-section>
                </q-card>
                <q-card
                  flat
                  bordered
                  v-ripple
                  class="cursor-pointer q-hoverable q-mb-xs"
                  :onclick="mostrarOpcionais"
                >
                  <q-card-section horizontal>
                    <q-card-section class="flex flex-center">
                      <q-img
                        class="rounded-borders"
                        src="https://dummyimage.com/200x200/b5b5b5/fff.jpg"
                        style="height: 100px; width: 100px"
                      />
                    </q-card-section>
                    <div class="flex flex-center">
                      <q-card-section vertical>
                        <div
                          class="q-mb-sm text-title text-bold"
                          style="font-size: 16px"
                        >
                          Super tudo
                        </div>
                        <div class="q-mb-sm text-caption text-grey">
                          Pão brioche selado, incrível burguer Seara (Burguer
                          feito de plantas), queijo cheddar Polenghi, alface
                          americana, cebola roxa, tomate e maionese da casa.
                        </div>
                        <div
                          class="text-orange text-bold"
                          style="font-size: 14px"
                        >
                          R$ 22,50
                        </div>
                      </q-card-section>
                    </div>
                    <q-space />
                  </q-card-section>
                </q-card>
              </div>
            </div>
          </q-tab-panel>

          <q-tab-panel name="bebidas">
            <div class="text-h6">Alarms</div>
            Lorem ipsum dolor sit amet consectetur adipisicing elit.
          </q-tab-panel>

          <q-tab-panel name="pizzas">
            <div class="text-h6">Movies</div>
            Lorem ipsum dolor sit amet consectetur adipisicing elit.
          </q-tab-panel>
        </q-tab-panels>
      </q-card>
    </div>
  </div>

  <div class="row" v-if="opcionais">
    <div class="col-12">
      <q-btn flat icon="mdi-arrow-left" @click="mostrarCardapio" />
    </div>
    <div class="col-12">
      <q-card flat>
        <q-card-section vertical>
          <div class="text-h6 text-bold q-mb-sm">Franguito</div>
          <div class="q-mb-sm text-subtitle2 text-grey">
            Pão,filé de frango, maionese, batata palha,tomate, alface.
          </div>
          <div class="text-orange text-bold" style="font-size: 16px">
            R$ 22,50
          </div>
        </q-card-section>
        <q-separator />
        <q-card-section vertical>
          <q-card
            flat
            style="border-radius: 0; border-bottom: 1px solid #b0bec5"
          >
            <div class="text-h6 text-weight-bold q-mb-md">Opcionais</div>
            <q-card-section horizontal class="q-pa-xs">
              <div class="flex flex-center">
                <div>Bacon</div>
              </div>
              <q-space />
              <div class="flex flex-center">R$ 2,50 <q-checkbox /></div>
            </q-card-section>
          </q-card>
          <q-card
            flat
            style="border-radius: 0; border-bottom: 1px solid #b0bec5"
          >
            <q-card-section horizontal class="q-pa-xs">
              <div class="flex flex-center">
                <div>Ovo</div>
              </div>
              <q-space />
              <div class="flex flex-center">R$ 2,50 <q-checkbox /></div>
            </q-card-section>
          </q-card>
          <q-card
            flat
            style="border-radius: 0; border-bottom: 1px solid #b0bec5"
          >
            <q-card-section horizontal class="q-pa-xs">
              <div class="flex flex-center">
                <div>Calabresa</div>
              </div>
              <q-space />
              <div class="flex flex-center">R$ 2,50 <q-checkbox /></div>
            </q-card-section>
          </q-card>
          <q-card
            flat
            style="border-radius: 0; border-bottom: 1px solid #b0bec5"
          >
            <q-card-section horizontal class="q-pa-xs">
              <div class="flex flex-center">
                <div>Frango</div>
              </div>
              <q-space />
              <div class="flex flex-center">R$ 2,50 <q-checkbox /></div>
            </q-card-section>
          </q-card>
          <q-card
            flat
            style="border-radius: 0; border-bottom: 1px solid #b0bec5"
          >
            <q-card-section horizontal class="q-pa-xs">
              <div class="flex flex-center">
                <div>Salsicha</div>
              </div>
              <q-space />
              <div class="flex flex-center">R$ 2,50 <q-checkbox /></div>
            </q-card-section>
          </q-card>
        </q-card-section>
        <q-card-section class="q-mt-lg">
          <q-input outlined type="textarea" label="Observações" />
        </q-card-section>
        <q-card-section class="q-mt-lg flex justify-center">
          <q-btn
            unelevated
            color="primary"
            label="Salvar e novo"
            class="q-mr-md"
          />
          <q-btn unelevated color="primary" label="Finalizar" />
        </q-card-section>
      </q-card>
    </div>
  </div>
</template>

<script lang="ts">
import { ref } from 'vue';
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'IndexPage',

  setup() {
    const cardapio = ref<boolean>(true);
    const opcionais = ref<boolean>(false);
    const checkout = ref<boolean>(false);

    const mostrarCardapio = () => {
      cardapio.value = true;
      opcionais.value = false;
      checkout.value = false;
    };

    const mostrarOpcionais = () => {
      cardapio.value = false;
      opcionais.value = true;
      checkout.value = false;
    };

    return {
      tab: ref('sanduiches'),
      model: ref(null),
      options: ['Google', 'Facebook', 'Twitter', 'Apple', 'Oracle'],
      cardapio,
      opcionais,
      checkout,
      mostrarCardapio,
      mostrarOpcionais,
    };
  },
});
</script>
