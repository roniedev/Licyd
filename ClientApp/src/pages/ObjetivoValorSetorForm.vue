<template>
  <t-form
    :form="formProps"
    :initial-data="dataFromServer"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import ObjetivoValorSetorService from 'src/services/ObjetivoValorSetorService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IObjetivoValorSetorResponse } from 'src/interfaces/IObjetivoValorSetor';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'ObjetivoValorSetorForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as IObjetivoValorSetorResponse);

    const route = useRoute();
    const router = useRouter();
    const service = ObjetivoValorSetorService;

    const dense = true;
    const design = 'outlined';

    const formProps = ref<IForm>({
      title: 'Cadastro de objetivo por valor (setor)',
      fields: [
        {
          label: 'Setor',
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
          label: 'Valor objetivo',
          name: 'valorObjetivo',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
          step: 0.01,
        },
        {
          label: 'Valor compromisso',
          name: 'valorCompromisso',
          dense: dense,
          design: design,
          type: 'number',
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
      router.push({ name: 'ObjetivoValorSetorIndex' });
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
