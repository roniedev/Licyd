<template>
  <q-tabs
    v-model="tab"
    dense
    class="text-black"
    active-color="primary"
    indicator-color="primary"
    align="justify"
    narrow-indicator
    v-if="exibirCardapio"
  >
    <template v-for="categoria in categorias" :key="categoria.id">
      <q-tab :name="categoria.nome" :label="categoria.nome" />
    </template>
  </q-tabs>

  <q-tab-panels v-model="tab" animated v-if="exibirCardapio">
    <template v-for="categoria in categorias" :key="categoria.id">
      <q-tab-panel :name="categoria.nome">
        <div
          v-for="item in categoria.itens"
          :key="item.id"
          @click="addItemPedido(item)"
        >
          <div class="row" v-if="!item.isItemAdicional">
            <div class="col">
              <q-card
                flat
                v-ripple
                class="cursor-pointer q-hoverable q-mb-xs"
                style="border-bottom: solid 1px #ccc; border-radius: 0px"
              >
                <q-card-section horizontal>
                  <q-card-section class="flex flex-center">
                    <q-img
                      class="rounded-borders"
                      src="https://dummyimage.com/100x100/b5b5b5/fff.jpg"
                      style="height: 100px; width: 100px"
                    />
                  </q-card-section>
                  <div class="flex flex-center">
                    <q-card-section vertical>
                      <div
                        class="q-mb-sm text-title text-weight-bold"
                        style="font-size: 16px"
                      >
                        {{ item.nome }}
                      </div>
                      <div class="q-mb-sm text-caption text-grey">
                        {{ item.descricao }}
                      </div>
                      <div
                        class="text-orange text-bold"
                        style="font-size: 14px"
                      >
                        R$ 14,50
                      </div>
                    </q-card-section>
                  </div>
                </q-card-section>
              </q-card>
            </div>
          </div>
        </div>
      </q-tab-panel>
    </template>
  </q-tab-panels>

  <div v-if="exibirOpcionais">
    <div class="row">
      <div class="col-12">
        <q-btn flat icon="mdi-arrow-left" @click="handleOpcionais" />
      </div>
      <div class="col-12">
        <q-card flat>
          <!-- <q-card-section vertical>
          <div class="text-h6 text-bold q-mb-sm">
            {{ itemSelecionado.nome }}
          </div>
          <div class="q-mb-sm text-subtitle2 text-grey">
            {{ itemSelecionado.descricao }}
          </div>
          <div class="text-orange text-bold" style="font-size: 16px">
            R$ 22,50
          </div>
        </q-card-section>
        <q-separator /> -->
          <q-card-section vertical>
            <q-card
              flat
              style="border-radius: 0; border-bottom: 1px solid #b0bec5"
            >
              <div class="text-h6 text-weight-bold q-mb-md">Opcionais</div>
              <template v-for="adicional in adicionais" :key="adicional.id">
                <q-card-section horizontal class="q-pa-xs">
                  <div class="flex flex-center">
                    <div class="text-h6 text-bold">
                      {{ adicional.nome }}
                    </div>
                  </div>
                  <q-space />
                  <div class="flex flex-center">
                    <div class="flex flex-center">
                      <q-btn
                        outline
                        round
                        color="primary"
                        icon="mdi-plus-thick"
                        @click="addItemPedidoAdicional(adicional)"
                      />
                      <span class="text-h6 text-bold q-pa-md text-primary">
                        {{ adicional.quantidade || 0 }}
                      </span>
                      <q-btn
                        outline
                        round
                        color="primary"
                        icon="mdi-minus-thick"
                        :disable="
                          !adicional.quantidade || adicional.quantidade === 0
                        "
                        @click="removerQuantidadeItemPedidoAdicional(adicional)"
                      />
                    </div>
                  </div>
                </q-card-section>
              </template>
            </q-card>
          </q-card-section>
          <q-card-section class="q-mt-lg">
            <q-input
              outlined
              v-model="itemPedido.observacao"
              type="textarea"
              label="Observações"
            />
          </q-card-section>
        </q-card>
      </div>
    </div>

    <q-footer class="bg-grey-3 text-primary q-pa-md" style="height: 100px">
      <div class="flex justify-around">
        <div class="flex flex-center">
          <q-btn
            outline
            round
            color="primary"
            icon="mdi-plus-thick"
            @click="addQuantidadeItemPedido"
          />
          <span class="text-h6 text-weight-bold q-pa-md">
            {{ itemPedido.quantidade }}
          </span>
          <q-btn
            outline
            round
            color="primary"
            icon="mdi-minus-thick"
            :disable="itemPedido.quantidade === 1"
            @click="removerQuantidadeItemPedido"
          />
        </div>
        <div class="flex flex-center">
          <q-btn
            outline
            text-color="primary"
            label="Adicionar"
            class="full-width text-bold"
            size="lg"
            @click="salvarItemPedido"
          />
        </div>
      </div>
    </q-footer>
  </div>

  <q-footer
    class="bg-grey-3 text-primary q-pa-md"
    style="height: 100px"
    v-if="exibirCardapio"
  >
    <div class="flex justify-around" v-if="itemPedidoQuantidade > 0">
      <div>
        <div class="text-body1 q-mb-sm">
          {{ itemPedidoQuantidade }} produto(s)
        </div>
        <div class="text-bold" style="font-size: 22px">
          R$ {{ pedidoValor }}
        </div>
      </div>
      <div class="flex flex-center">
        <q-btn
          outline
          text-color="primary"
          label="Finalizar"
          class="full-width text-bold"
          size="lg"
          @click="finalizarPedido"
        />
      </div>
    </div>
    <div class="flex flex-center" v-else>
      <span class="text-h6">Adicione pelo menos um item</span>
    </div>
  </q-footer>
</template>

<script lang="ts">
import { ref } from 'vue';
import { computed } from 'vue';
import { onBeforeMount } from 'vue';
import { defineComponent } from 'vue';

import CategoriaItemService from 'src/pages/cadastros/categoriaItem/services/CategoriaItemService';
import { IItemPedido, IPedido } from '../interfaces/IPedido';
import { ICategoria, IItem } from '../interfaces/ICategoria';

export default defineComponent({
  name: 'CardapioComponent',

  setup() {
    let categoriaSelecionada: ICategoria | null = null;

    const tab = ref<string>('');
    const pedido = ref<IPedido>({
      itensPedido: [],
    });

    const itemPedido = ref<IItemPedido>({
      itemId: 0,
      valorUnitario: 0,
      quantidade: 0,
      categoriaId: 0,
      nome: '',
      descricao: '',
      adicionais: [],
      observacao: '',
    });

    const adicionais = ref<IItem[]>([]);

    const categorias = ref<ICategoria[]>([]);
    const exibirCardapio = ref<boolean>(true);
    const exibirOpcionais = ref<boolean>(false);
    const categoriaItemService = new CategoriaItemService('CategoriaItem');

    const pedidoValor = ref<number>(0);

    const addItemPedido = (item: any) => {
      itemPedido.value = {
        itemId: item.id,
        valorUnitario: 25,
        quantidade: 1,
        categoriaId: item.categoriaId,
        nome: item.nome,
        descricao: item.descricao,
        adicionais: [],
        observacao: '',
      };

      if (itemPedido.value) {
        const categoria = categorias.value.find(
          (categoria) => categoria.id == itemPedido.value.categoriaId
        );

        if (categoria) {
          categoriaSelecionada = categoria;
          adicionais.value = categoriaSelecionada.itens.filter(
            (item: any) => item.isItemAdicional
          );
        }
      }

      handleOpcionais();
    };

    const addQuantidadeItemPedido = () => {
      itemPedido.value.quantidade++;
    };

    const removerQuantidadeItemPedido = () => {
      itemPedido.value.quantidade--;
    };

    const addItemPedidoAdicional = (item: IItem) => {
      if (!item.quantidade) {
        item.quantidade = 0;
      }
      item.quantidade++;
    };

    const removerQuantidadeItemPedidoAdicional = (item: IItem) => {
      if (item.quantidade && item.quantidade > 0) {
        item.quantidade--;
      }
    };

    const getAllCategoriasItem = async () => {
      categorias.value = await categoriaItemService.getAll();
      if (categorias.value.length > 0) tab.value = categorias.value[0].nome;
    };

    const handleOpcionais = () => {
      exibirCardapio.value = !exibirCardapio.value;
      exibirOpcionais.value = !exibirOpcionais.value;
      if (exibirCardapio.value) inicializarPedido();
    };

    const salvarItemPedido = () => {
      if (adicionais.value.length > 0) {
        adicionais.value.forEach((opcional: IItem) => {
          if (opcional.quantidade && opcional.quantidade > 0) {
            itemPedido.value?.adicionais?.push({
              itemId: opcional.id,
              quantidade: opcional.quantidade,
              valorUnitario: opcional.valorUnitario || 0,
              categoriaId: opcional.categoriaId,
              nome: opcional.nome,
              descricao: opcional.descricao,
            });
          }
        });
      }

      pedido.value.itensPedido.push(itemPedido.value);
      console.log(pedido.value);
      handleOpcionais();
    };

    const finalizarPedido = () => {
      console.log(pedido.value);
    };

    const inicializarPedido = () => {
      itemPedido.value = {
        itemId: 0,
        valorUnitario: 0,
        quantidade: 0,
        categoriaId: 0,
        nome: '',
        descricao: '',
        adicionais: [],
        observacao: '',
      };

      categoriaSelecionada = null;

      adicionais.value.forEach((adicional: IItem) => {
        adicional.quantidade = 0;
      });
    };

    // const adicionais = computed(() => {
    //   if (itemPedido.value) {
    //     const categoria = categorias.value.find(
    //       (categoria) => categoria.id == itemPedido.value.categoriaId
    //     );

    //     if (categoria) {
    //       categoriaSelecionada = categoria;
    //       return categoriaSelecionada.itens.filter(
    //         (item: any) => item.isItemAdicional
    //       );
    //     }
    //   }

    //   return [];
    // });

    const itemPedidoQuantidade = computed(() => {
      return pedido.value.itensPedido.length;
    });

    onBeforeMount(() => getAllCategoriasItem());

    return {
      tab,
      categorias,
      adicionais,
      exibirCardapio,
      exibirOpcionais,
      itemPedido,
      pedidoValor,
      itemPedidoQuantidade,
      addItemPedido,
      finalizarPedido,
      salvarItemPedido,
      handleOpcionais,
      addQuantidadeItemPedido,
      removerQuantidadeItemPedido,
      addItemPedidoAdicional,
      removerQuantidadeItemPedidoAdicional,
    };
  },
});
</script>
