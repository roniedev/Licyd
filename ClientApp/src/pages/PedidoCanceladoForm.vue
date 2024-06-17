<template>
  <t-form
    :form="formProps"
    :initial-data="dataFromServer"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import PedidoCanceladoService from 'src/services/PedidoCanceladoService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IPedidoCanceladoResponse } from 'src/interfaces/IPedidoCancelado';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'PedidoCanceladoForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as IPedidoCanceladoResponse);

    const route = useRoute();
    const router = useRouter();
    const service = PedidoCanceladoService;

    const dense = true;
    const design = 'outlined';

    const formProps = ref<IForm>({
      title: 'Manutenção de pedidos cancelados',
      fields: [
        {
          label: 'Pedido',
          name: 'pedidoId',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: false,
        },
        {
          label: 'Cliente',
          name: 'clienteId',
          dense: dense,
          design: design,
          type: 'select',
          cols: 6,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterClientes',
          labelSelect: 'clienteDescricao',
        },
        {
          label: 'Divisão',
          name: 'divisaoId',
          dense: dense,
          design: design,
          type: 'select',
          cols: 6,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterDivisoes',
          labelSelect: 'divisaoDescricao',
        },
        {
          label: 'Subdivisão',
          name: 'subdivisaoId',
          dense: dense,
          design: design,
          type: 'select',
          cols: 6,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterSubdivisoes',
          labelSelect: 'subdivisaoDescricao',
        },
        {
          label: 'Representante',
          name: 'representanteId',
          dense: dense,
          design: design,
          type: 'select',
          cols: 6,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterRepresentantes',
          labelSelect: 'representanteDescricao',
        },
        {
          label: 'Setor (Regional)',
          name: 'setorId',
          dense: dense,
          design: design,
          type: 'select',
          cols: 6,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterSetores',
          labelSelect: 'setorDescricao',
        },
        {
          label: 'Data do pedido',
          name: 'dataPedido',
          dense: dense,
          design: design,
          type: 'date',
          cols: 6,
          required: true,
        },
        {
          label: 'Data de cancelamento',
          name: 'dataCancelamento',
          dense: dense,
          design: design,
          type: 'date',
          cols: 6,
          required: true,
        },
        {
          label: 'Ano de referência',
          name: 'anoReferencia',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
        },
        {
          label: 'Mês de referência',
          name: 'mesReferencia',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
        },
        {
          label: 'Estabelecimento',
          name: 'estabelecimentoId',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: false,
        },
        {
          label: 'Valor do pedido',
          name: 'valorPedido',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
          step: 0.01,
        },
        {
          label: 'Situação',
          name: 'situacao',
          dense: dense,
          design: design,
          type: 'text',
          cols: 6,
          required: true,
        },
        {
          label: 'Atendente',
          name: 'atendente',
          dense: dense,
          design: design,
          type: 'text',
          cols: 6,
          required: true,
        },
        {
          label: 'Motivo',
          name: 'motivo',
          dense: dense,
          design: design,
          type: 'textarea',
          cols: 6,
          required: true,
        },
      ],
    });

    onMounted(async () => {
      showLoading();

      const id = route.params.id;

      if (id) {
        const data = await service.obterPorId(Number(id));
        if (data) dataFromServer.value = data;
      }

      hideLoading();
    });

    async function onSubmit(formData: any) {
      showLoading();

      formData.id = dataFromServer.value.id;

      const data = dataFromServer.value.id
        ? await service.atualizar(formData)
        : await service.inserir(formData);

      if (data) onCancelar();

      hideLoading();
    }

    function onCancelar() {
      router.push({ name: 'PedidoCanceladoIndex' });
    }

    return {
      formProps,
      dataFromServer,
      onSubmit,
      onCancelar,
    };
  },
});
</script>
