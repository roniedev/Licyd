<template>
  <t-form
    :form="form"
    :initialData="dadosIniciais"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import SubdivisaoService from 'src/services/SubdivisaoService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { ISubdivisaoResponse } from 'src/interfaces/ISubdivisao';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'SubdivisaoForm',

  components: {
    TForm,
  },

  setup() {
    const dadosIniciais = ref({} as ISubdivisaoResponse);

    const route = useRoute();
    const router = useRouter();
    const service = SubdivisaoService;

    const inputDesign = 'outlined';
    const inputDense = false;

    const form = ref<IForm>({
      title: 'Cadastro de subdivisão',
      fields: [
        {
          label: 'Descrição',
          name: 'descricao',
          type: 'text',
          cols: 6,
          required: true,
          dense: inputDense,
          design: inputDesign,
          validationRules: [
            (val) => (val && val.length > 0) || 'O campo é obrigatório.',
            (val) =>
              (val && val.length <= 55) ||
              'O campo deve ter no máximo 55 caracteres.',
          ],
        },
        {
          label: 'Divisão',
          name: 'divisaoId',
          dense: inputDense,
          design: inputDesign,
          type: 'select',
          cols: 6,
          required: false,
          urlRequestOption: '/consultapadrao/obterdivisoes',
          labelSelect: 'divisaoDescricao',
        },
        {
          label: 'Tipo de venda',
          name: 'tipoVenda',
          dense: inputDense,
          type: 'radio',
          cols: 6,
          radioInline: true,
          required: false,
          options: [
            { label: 'Sell-in', value: 1 },
            { label: 'Sell-out', value: 2 },
            { label: 'Ambos', value: 3 },
          ],
        },
      ],
    });

    onMounted(async () => {
      showLoading();
      const id = route.params.id;

      if (id) {
        const data = await service.obterPorId(Number(id));
        if (data) dadosIniciais.value = data;
      }
      hideLoading();
    });

    async function onSubmit(formData: any) {
      showLoading();
      formData.id = dadosIniciais.value.id;

      const data = dadosIniciais.value.id
        ? await service.atualizar(formData)
        : await service.inserir(formData);

      if (data) router.push({ name: 'SubdivisaoIndex' });
      hideLoading();
    }

    function onCancelar() {
      router.push({ name: 'SubdivisaoIndex' });
    }

    return {
      form,
      dadosIniciais,
      onSubmit,
      onCancelar,
    };
  },
});
</script>
