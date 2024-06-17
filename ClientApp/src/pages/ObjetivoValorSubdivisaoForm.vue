<template>
  <t-form
    :form="formProps"
    :initial-data="dataFromServer"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import ObjetivoValorSubdivisaoService from 'src/services/ObjetivoValorSubdivisaoService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IObjetivoValorSubdivisaoResponse } from 'src/interfaces/IObjetivoValorSubdivisao';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'ObjetivoValorSubdivisaoForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as IObjetivoValorSubdivisaoResponse);

    const route = useRoute();
    const router = useRouter();
    const service = ObjetivoValorSubdivisaoService;

    const dense = true;
    const design = 'outlined';

    const formProps = ref<IForm>({
      title: 'Cadastro de objetivo por valor (subdivisão)',
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
          label: 'Valor do objetivo',
          name: 'valor',
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
      router.push({ name: 'ObjetivoValorSubdivisaoIndex' });
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
