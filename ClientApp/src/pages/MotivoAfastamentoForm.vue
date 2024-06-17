<template>
  <t-form
    :form="form"
    :initialData="initialData"
    @submit="submit"
    @cancelar="cancelar"
  />
</template>

<script lang="ts">
import MotivoAfastamentoService from 'src/services/MotivoAfastamentoService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IMotivoAfastamentoResponse } from 'src/interfaces/IMotivoAfastamento';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'MotivoAfastamentoForm',

  components: {
    TForm,
  },

  setup() {
    const initialData = ref({} as IMotivoAfastamentoResponse);

    const route = useRoute();
    const router = useRouter();
    const service = MotivoAfastamentoService;

    const dense = false;
    const design = 'outlined';

    const form = ref<IForm>({
      title: 'Cadastro de motivo de afastamento',
      fields: [
        {
          label: 'Descrição',
          name: 'descricao',
          type: 'text',
          cols: 6,
          dense: dense,
          design: design,
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

      if (data) cancelar();

      hideLoading();
    }

    function cancelar() {
      router.push({ name: 'MotivoAfastamentoIndex' });
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
