<template>
  <t-form
    :form="formProps"
    :initial-data="dataFromServer"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import ObjetivoValorGeralService from 'src/services/ObjetivoValorGeralService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IObjetivoValorGeralResponse } from 'src/interfaces/IObjetivoValorGeral';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'ObjetivoValorGeralForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as IObjetivoValorGeralResponse);

    const route = useRoute();
    const router = useRouter();
    const service = ObjetivoValorGeralService;

    const dense = true;
    const design = 'outlined';

    const formProps = ref<IForm>({
      title: 'Cadastro de objetivo por valor (geral)',
      fields: [
        {
          label: 'Valor',
          name: 'valor',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
          step: 0.01,
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
      router.push({ name: 'ObjetivoValorGeralIndex' });
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
