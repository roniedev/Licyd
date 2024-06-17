<template>
  <t-form
    :form="formProps"
    :initial-data="dataFromServer"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import ObjetivoPercentualExcecaoService from 'src/services/ObjetivoPercentualExcecaoService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IObjetivoPercentualExcecaoResponse } from 'src/interfaces/IObjetivoPercentualExcecao';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'ObjetivoPercentualExcecaoForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as IObjetivoPercentualExcecaoResponse);

    const route = useRoute();
    const router = useRouter();
    const service = ObjetivoPercentualExcecaoService;

    const dense = true;
    const design = 'outlined';

    const formProps = ref<IForm>({
      title: 'Cadastro de objetivo por percentual (exceção)',
      fields: [
        {
          label: 'Descrição',
          name: 'descricao',
          dense: dense,
          design: design,
          type: 'text',
          cols: 6,
          required: true,
        },
        {
          label: 'Data inicial',
          name: 'dataInicial',
          dense: dense,
          design: design,
          type: 'date',
          cols: 6,
          required: true,
        },
        {
          label: 'Data final',
          name: 'dataFinal',
          dense: dense,
          design: design,
          type: 'date',
          cols: 6,
          required: false,
        },
        {
          label: 'Objetivo base (%)',
          name: 'objetivoPercentualBase',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
          step: 0.01,
        },
        {
          label: 'Objetivo 1 (%)',
          name: 'objetivoPercentual1',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
          step: 0.01,
        },
        {
          label: 'Objetivo 2 (%)',
          name: 'objetivoPercentual2',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
          step: 0.01,
        },
        {
          label: 'Objetivo 3 (%)',
          name: 'objetivoPercentual3',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
          step: 0.01,
        },
        {
          label: 'Objetivo 4 (%)',
          name: 'objetivoPercentual4',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
          step: 0.01,
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
      router.push({ name: 'ObjetivoPercentualExcecaoIndex' });
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
