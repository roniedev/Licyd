<template>
  <t-form :form="form" @submit="onSubmit" @cancelar="onCancel" />
</template>

<script lang="ts">
import AfastamentoService from 'src/services/AfastamentoService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IAfastamentoResponse } from 'src/interfaces/IAfastamento';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'AfastamentoForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as IAfastamentoResponse);

    const route = useRoute();
    const router = useRouter();
    const service = AfastamentoService;

    const denseInput = false;
    const designInput = 'outlined';

    const form = ref<IForm>({
      title: 'Cadastro de afastamentos',
      fields: [
        {
          label: 'Matrícula',
          name: 'matricula',
          type: 'select',
          cols: 6,
          dense: denseInput,
          design: designInput,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterMatriculas',
          labelSelect: 'matriculaDescricao',
        },
        {
          label: 'Motivo do afastamento',
          name: 'motivoAfastamentoId',
          type: 'select',
          cols: 6,
          dense: denseInput,
          design: designInput,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterMotivosAfastamento',
          labelSelect: 'motivoAfastamentoDescricao',
        },
        {
          label: 'Data inicial',
          name: 'dataInicial',
          type: 'date',
          cols: 6,
          dense: denseInput,
          design: designInput,
          required: false,
        },
        {
          label: 'Data final',
          name: 'dataFinal',
          type: 'date',
          cols: 6,
          dense: denseInput,
          design: designInput,
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

      if (data) router.push({ name: 'AfastamentoIndex' });
      hideLoading();
    }

    function onCancel() {
      router.push({ name: 'AfastamentoIndex' });
    }

    return {
      form,
      dataFromServer,
      onSubmit,
      onCancel,
    };
  },
});
</script>
