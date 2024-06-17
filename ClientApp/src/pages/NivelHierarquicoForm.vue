<template>
  <t-form
    :form="formProps"
    :initial-data="initialData"
    @submit="onSubmit"
    @cancelar="onCancel"
  />
</template>

<script lang="ts">
import NivelHierarquicoService from 'src/services/NivelHierarquicoService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { INivelHierarquicoResponse } from 'src/interfaces/INivelHierarquico';

export default defineComponent({
  name: 'NivelHierarquicoForm',

  components: {
    TForm,
  },

  setup() {
    const initialData = ref({} as INivelHierarquicoResponse);

    const route = useRoute();
    const router = useRouter();
    const service = NivelHierarquicoService;

    const designInput = 'outlined';
    const denseInput = true;

    const formProps = ref<IForm>({
      title: 'Cadastro de nível hierárquico',
      fields: [
        {
          label: 'Descrição',
          name: 'descricao',
          dense: denseInput,
          design: designInput,
          type: 'text',
          required: true,
          cols: 6,
        },
        {
          label: 'Sequência hierárquica',
          name: 'sequenciaHierarquica',
          dense: denseInput,
          design: designInput,
          required: true,
          type: 'text',
          cols: 6,
        },
      ],
    });

    onMounted(async () => {
      const id = route.params.id;

      if (id) {
        const data = await service.obterPorId(Number(id));
        if (data) initialData.value = data;
      }
    });

    async function onSubmit(formData: any) {
      formData.id = initialData.value.id;

      const data = initialData.value.id
        ? await service.atualizar(formData)
        : await service.inserir(formData);

      if (data) router.back();
    }

    function onCancel() {
      router.push({ name: 'NivelHierarquicoIndex' });
    }

    return {
      formProps,
      initialData,
      onSubmit,
      onCancel,
    };
  },
});
</script>
