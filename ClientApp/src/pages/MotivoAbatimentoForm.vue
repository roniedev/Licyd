<template>
  <t-form
    :form="formProps"
    :initial-data="dataFromServer"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import MotivoAbatimentoService from 'src/services/MotivoAbatimentoService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IMotivoAbatimentoResponse } from 'src/interfaces/IMotivoAbatimento';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'MotivoAbatimentoForm',

  components: {
    TForm,
  },

  setup() {
    const formTitle = ref('Motivos de abatimento');
    const dataFromServer = ref({} as IMotivoAbatimentoResponse);

    const route = useRoute();
    const router = useRouter();
    const service = MotivoAbatimentoService;

    const dense = true;
    const design = 'outlined';

    const formProps = ref<IForm>({
      title: 'Cadastro de motivo de abatimento',
      fields: [
        {
          label: 'Descrição',
          name: 'descricao',
          dense: dense,
          design: design,
          type: 'text',
          cols: 6,
          required: false,
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
          label: 'Nota fiscal de saída e devolução obrigatórias?',
          name: 'isNotaFiscalSaida',
          dense: dense,
          design: design,
          type: 'toggle',
          cols: 6,
          required: false,
        },
        // {
        //   label: 'Nota fiscal de devolução obrigatória?',
        //   name: 'isNotaFiscalDevolucao',
        //   dense: dense,
        //   design: design,
        //   type: 'toggle',
        //   cols: 6,
        //   required: false,
        // },
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
      router.push({ name: 'MotivoAbatimentoIndex' });
    }

    return {
      formProps,
      formTitle,
      dataFromServer,
      onSubmit,
      onCancelar,
    };
  },
});
</script>
