<template>
  <t-form
    :form="formProps"
    :initial-data="dataFromServer"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import ObjetivoPeriodoMesDivisaoService from 'src/services/ObjetivoPeriodoMesDivisaoService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IObjetivoPeriodoMesDivisaoResponse } from 'src/interfaces/IObjetivoPeriodoMesDivisao';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'ObjetivoPeriodoMesDivisaoForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as IObjetivoPeriodoMesDivisaoResponse);

    const route = useRoute();
    const router = useRouter();
    const service = ObjetivoPeriodoMesDivisaoService;

    const dense = false;
    const design = 'outlined';

    const formProps = ref<IForm>({
      title: 'Cadastro de objetivo por período/mês divisão',
      fields: [
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
          label: 'Estabelecimento',
          name: 'estabelecimentoId',
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
          step: 1,
        },
        {
          label: 'Mês de referência',
          name: 'mesReferencia',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
          step: 1,
        },
        {
          label: 'Data inicial de vigência',
          name: 'dataInicialVigencia',
          dense: dense,
          design: design,
          type: 'date',
          cols: 6,
          required: true,
        },
        {
          label: 'Data final de vigência',
          name: 'dataFinalVigencia',
          dense: dense,
          design: design,
          type: 'date',
          cols: 6,
          required: false,
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
      router.push({ name: 'ObjetivoPeriodoMesDivisaoIndex' });
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
