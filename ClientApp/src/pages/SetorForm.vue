<template>
  <t-form
    :form="form"
    :initialData="initialData"
    @submit="submit"
    @cancelar="cancelar"
  />
</template>

<script lang="ts">
import SetorService from 'src/services/SetorService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { ISetorResponse } from 'src/interfaces/ISetor';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'SetorForm',

  components: {
    TForm,
  },

  setup() {
    const initialData = ref({} as ISetorResponse);

    const route = useRoute();
    const router = useRouter();
    const service = SetorService;

    const inputDesign = 'outlined';
    const inputDense = false;

    const form = ref<IForm>({
      title: 'Cadastro de setor',
      fields: [
        {
          label: 'Descrição',
          name: 'descricao',
          type: 'text',
          cols: 6,
          dense: inputDense,
          design: inputDesign,
          required: false,
        },
        {
          label: 'Nível hierárquico',
          name: 'nivelHierarquicoId',
          type: 'select',
          cols: 6,
          dense: inputDense,
          design: inputDesign,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterNiveisHierarquicos',
          labelSelect: 'nivelHierarquicoDescricao',
        },
        {
          label: 'Setor superior',
          name: 'setorSuperiorId',
          type: 'select',
          cols: 6,
          dense: inputDense,
          design: inputDesign,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterSetores',
          labelSelect: 'setorSuperiorDescricao',
        },
        {
          label: 'Divisão',
          name: 'divisaoId',
          type: 'select',
          cols: 6,
          dense: inputDense,
          design: inputDesign,
          required: false,
          urlRequestOption: '/ConsultaPadrao/obterdivisoes',
          labelSelect: 'divisaoDescricao',
        },
        {
          label: 'Subdivisão',
          name: 'subdivisaoId',
          type: 'select',
          cols: 6,
          dense: inputDense,
          design: inputDesign,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterSubdivisoes',
          labelSelect: 'subdivisaoDescricao',
        },
        {
          label: 'Centro de custo',
          name: 'centroCustoId',
          type: 'select',
          cols: 6,
          dense: inputDense,
          design: inputDesign,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterCentrosCusto',
          labelSelect: 'centroCustoDescricao',
        },
        {
          label: 'Vago padrão',
          name: 'vagoPadraoId',
          type: 'select',
          cols: 6,
          dense: inputDense,
          design: inputDesign,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterRepresentantes',
          labelSelect: 'vagoPadraoDescricao',
        },
        {
          label: 'Estabelecimento',
          name: 'estabelecimentoId',
          type: 'number',
          cols: 6,
          dense: inputDense,
          design: inputDesign,
          required: true,
        },
        {
          label: 'Data inicial de vigência',
          name: 'dataInicialVigencia',
          type: 'date',
          cols: 6,
          dense: inputDense,
          design: inputDesign,
          required: true,
        },
        {
          label: 'Data final de vigência',
          name: 'dataFinalVigencia',
          type: 'date',
          cols: 6,
          dense: inputDense,
          design: inputDesign,
          required: false,
        },
        {
          label: 'Afeta premiação?',
          name: 'isAfetaPremiacao',
          type: 'toggle',
          cols: 6,
          dense: inputDense,
          required: false,
        },
        {
          label: 'Afeta faturamento?',
          name: 'isAfetaFaturamento',
          type: 'toggle',
          cols: 6,
          dense: inputDense,
          required: false,
        },
        {
          label: 'Visualizar setor no mapa de vendas?',
          name: 'isVisualizarSetorMapaVendas',
          type: 'toggle',
          cols: 6,
          dense: inputDense,
          required: false,
        },
        {
          label: 'Alterar para vago padrão nos afastamentos?',
          name: 'isAlterarVagoPadraoAfastamento',
          type: 'toggle',
          cols: 6,
          dense: inputDense,
          required: false,
        },
        {
          label: 'Sell-in?',
          name: 'isSellIn',
          type: 'toggle',
          cols: 6,
          dense: inputDense,
          required: false,
        },
      ],
    });

    onMounted(async () => {
      showLoading();
      const id = route.params.id;

      if (id) {
        const data = await service.obterPorId(Number(id));
        if (data) initialData.value = data;
      }
      hideLoading();
    });

    async function submit(formData: any) {
      showLoading();
      formData.id = initialData.value.id;

      const data = initialData.value.id
        ? await service.atualizar(formData)
        : await service.inserir(formData);

      if (data) router.push({ name: 'SetorIndex' });
      hideLoading();
    }

    function cancelar() {
      router.push({ name: 'SetorIndex' });
    }

    return {
      form,
      initialData,
      submit,
      cancelar,
    };
  },
});
</script>
