<template>
  <jump-form :form="form" @submit="onSubmit" />
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { IJumpForm } from 'src/components/forms/interfaces/IJumpForm';
import { FormElementType } from 'src/components/forms/enums/FormElementType';
import { IJumpSelectServer } from 'src/components/forms/interfaces/IJumpSelectServer';

import JumpForm from 'src/components/forms/JumpForm.vue';
import RelatorioService from '../services/RelatorioService';

export default defineComponent({
  components: {
    JumpForm,
  },
  setup() {
    const service = new RelatorioService();

    const form = ref<IJumpForm>({
      title: 'Relatório de múltiplo acesso',
      labelSubmitButton: 'Gerar relatório',
      iconSubmitButton: 'mdi-file-chart-outline',
      showCancelButton: false,
      fields: [
        {
          name: 'setoresPaiId',
          label: 'Setores pai',
          type: FormElementType.selectServer,
          cols: 6,
          style: 'outlined',
          urlGetOptions: 'Relatorio/ObterSetores',
          multiselect: true,
          behavior: 'dialog',
          useChips: true,
        } as IJumpSelectServer,
        {
          name: 'setoresFilhoId',
          label: 'Setores filho',
          type: FormElementType.selectServer,
          cols: 6,
          style: 'outlined',
          urlGetOptions: 'Relatorio/ObterSetores',
          multiselect: true,
          behavior: 'dialog',
          useChips: true,
        } as IJumpSelectServer,
      ],
    });

    const onSubmit = async (formValues: any) => {
      await service.MultiploAcesso(formValues);
    };

    return {
      form,
      onSubmit,
    };
  },
});
</script>
