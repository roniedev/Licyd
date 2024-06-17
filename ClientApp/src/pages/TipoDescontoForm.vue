<template>
  <t-form
    :title="formTitle"
    :form-props="formProps"
    :form-data="dataFromServer"
    @on-submit="onSubmit"
  />
</template>

<script lang="ts">
import TipoDescontoService from 'src/services/TipoDescontoService';
import TForm from 'src/components/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IFormProps } from 'src/interfaces/components/IForm';
import { ITipoDescontoResponse } from 'src/interfaces/ITipoDesconto';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'TipoDescontoForm',

  components: {
    TForm,
  },

  setup() {
    const formTitle = ref('Tipos de desconto');
    const dataFromServer = ref({} as ITipoDescontoResponse);

    const route = useRoute();
    const router = useRouter();
    const service = TipoDescontoService;

    const formProps = ref<IFormProps[]>([
      {
        label: 'Descrição',
        name: 'descricao',
        dense: false,
        type: 'text',
        cols: 6,
        newLine: false,
        required: false,
      },
      {
        label: 'Divisão',
        name: 'divisaoId',
        dense: false,
        type: 'select',
        cols: 6,
        newLine: false,
        required: false,
        urlRequestOption: '/ConsultaPadrao/ObterDivisoes',
        labelSelect: 'divisaoDescricao',
      },
    ]);

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

      if (data) router.push({ name: 'TipoDescontoIndex' });
      hideLoading();
    }

    return {
      formProps,
      formTitle,
      dataFromServer,
      onSubmit,
    };
  },
});
</script>
