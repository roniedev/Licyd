<template>
  <t-form
    :form="formProps"
    :initial-data="dataFromServer"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import PedidoEnviadoClienteService from 'src/services/PedidoEnviadoClienteService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IPedidoEnviadoClienteResponse } from 'src/interfaces/IPedidoEnviadoCliente';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'PedidoEnviadoClienteForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as IPedidoEnviadoClienteResponse);

    const route = useRoute();
    const router = useRouter();
    const service = PedidoEnviadoClienteService;

    const dense = true;
    const design = 'outlined';

    const formProps = ref<IForm>({
      title: 'Pedidos enviados por clientes',
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
          label: 'Data do pedido',
          name: 'dataPedido',
          dense: dense,
          design: design,
          type: 'date',
          cols: 6,
          required: true,
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
          label: 'Mês de referência',
          name: 'mesReferencia',
          dense: dense,
          design: design,
          type: 'number',
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
          label: 'Estabelecimento',
          name: 'estabelecimentoId',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: false,
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
        // {
        //   label: 'Empenho',
        //   name: 'empenhoId',
        //   dense: dense,
        //   design: design,
        //   type: 'text',
        //   cols: 6,
        //   required: true,
        // },
        // {
        //   label: 'Pedido enviado pelo consultor',
        //   name: 'pedidoId',
        //   dense: dense,
        //   design: design,
        //   type: 'number',
        //   cols: 6,
        //   required: false,
        // },
        {
          label: 'Valor enviado',
          name: 'valorPedido',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
          step: 0.01,
        },
        {
          label: 'Valor faturado',
          name: 'valorFaturado',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
          step: 0.01,
        },
        {
          label: 'Percentual desconto autorizado',
          name: 'percentualDescontoAutorizado',
          dense: dense,
          design: design,
          type: 'number',
          step: 0.01,
          cols: 6,
          required: true,
        },
        {
          label: 'Valor desconto concedido',
          name: 'valorDescontoConcedido',
          dense: dense,
          design: design,
          type: 'number',
          step: 0.01,
          cols: 6,
          required: true,
        },
        {
          label: 'Quantidade total enviada',
          name: 'quantidadeTotalEnviada',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
        },
        {
          label: 'Observação',
          name: 'observacao',
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
      router.push({ name: 'PedidoEnviadoClienteIndex' });
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
