<template>
  <t-form
    :form="form"
    :initial-data="dataFromServer"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import AbatimentoService from 'src/services/AbatimentoService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IAbatimentoResponse } from 'src/interfaces/IAbatimento';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'AbatimentoForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as IAbatimentoResponse);

    const route = useRoute();
    const router = useRouter();
    const service = AbatimentoService;

    const dense = false;
    const design = 'outlined';

    const form = ref<IForm>({
      title: 'Abatimentos',
      fields: [
        {
          label: 'Pedido',
          name: 'pedidoId',
          type: 'number',
          cols: 6,
          dense: dense,
          design: design,
          required: false,
        },
        {
          label: 'Tipo de operação',
          name: 'tipoOperacaoId',
          type: 'radio',
          cols: 6,
          dense: dense,
          design: design,
          required: false,
          radioInline: true,
          options: [
            { label: 'Crédito', value: 2 },
            { label: 'Débito', value: 1 },
          ],
        },
        {
          label: 'Nota fiscal',
          name: 'notaFiscalSaida',
          type: 'number',
          cols: 6,
          dense: dense,
          design: design,
          required: false,
        },
        {
          label: 'Série da nota fiscal',
          name: 'serieNotaFiscalSaida',
          type: 'number',
          cols: 6,
          dense: dense,
          design: design,
          required: false,
        },
        {
          label: 'Cliente',
          name: 'clienteId',
          type: 'select',
          cols: 6,
          dense: dense,
          design: design,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterClientes',
          labelSelect: 'clienteDescricao',
        },
        {
          label: 'Divisão',
          name: 'divisaoId',
          type: 'select',
          cols: 6,
          dense: dense,
          design: design,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterDivisoes',
          labelSelect: 'divisaoDescricao',
        },
        {
          label: 'Subdivisão',
          name: 'subdivisaoId',
          type: 'select',
          cols: 6,
          dense: dense,
          design: design,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterSubdivisoes',
          labelSelect: 'subdivisaoDescricao',
        },
        {
          label: 'Representante',
          name: 'representanteId',
          type: 'select',
          cols: 6,
          dense: dense,
          design: design,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterRepresentantes',
          labelSelect: 'representanteDescricao',
        },
        {
          label: 'Centro de custo',
          name: 'centroCustoId',
          type: 'select',
          cols: 6,
          dense: dense,
          design: design,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterCentrosCusto',
          labelSelect: 'centroCustoDescricao',
        },
        {
          label: 'Ano de referência',
          name: 'anoReferencia',
          type: 'number',
          cols: 6,
          dense: dense,
          design: design,
          required: true,
        },
        {
          label: 'Mês de referência',
          name: 'mesReferencia',
          type: 'number',
          cols: 6,
          dense: dense,
          design: design,
          required: true,
        },
        {
          label: 'Estabelecimento',
          name: 'estabelecimentoId',
          type: 'number',
          cols: 6,
          dense: dense,
          design: design,
          required: false,
        },
        {
          label: 'Setor (Regional)',
          name: 'setorId',
          type: 'select',
          cols: 6,
          dense: dense,
          design: design,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterSetores',
          labelSelect: 'setorDescricao',
        },
        {
          label: 'Valor de faturamento',
          name: 'valorFaturamento',
          type: 'number',
          cols: 6,
          dense: dense,
          design: design,
          required: true,
          step: 0.01,
        },
        {
          label: 'Percentual de desconto',
          name: 'percentualDesconto',
          type: 'number',
          cols: 6,
          dense: dense,
          design: design,
          required: true,
          step: 0.01,
        },
        {
          label: 'Valor de desconto',
          name: 'valorDesconto',
          type: 'number',
          cols: 6,
          dense: dense,
          design: design,
          required: true,
          step: 0.01,
        },
        {
          label: 'Tipo de desconto',
          name: 'tipoDescontoId',
          type: 'select',
          cols: 6,
          dense: dense,
          design: design,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterTiposDesconto',
          labelSelect: 'tipoDescontoDescricao',
        },
        {
          label: 'Motivo de abatimento',
          name: 'motivoAbatimentoId',
          type: 'select',
          cols: 6,
          dense: dense,
          design: design,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterMotivosAbatimento',
          labelSelect: 'motivoAbatimentoDescricao',
        },
        {
          label: 'Nota fiscal de devolução',
          name: 'notaFiscalDevolucao',
          type: 'number',
          cols: 6,
          dense: dense,
          design: design,
          required: true,
        },
        {
          label: 'Atendente',
          name: 'atendente',
          type: 'text',
          cols: 6,
          dense: dense,
          design: design,
          required: true,
        },
        {
          label: 'Observação',
          name: 'observacao',
          type: 'textarea',
          cols: 12,
          dense: dense,
          design: design,
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
      router.push({ name: 'AbatimentoIndex' });
    }

    return {
      form,
      dataFromServer,
      onSubmit,
      onCancelar,
    };
  },
});
</script>
