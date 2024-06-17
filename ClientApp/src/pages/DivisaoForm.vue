<template>
  <t-form
    :form="form"
    :initialData="dadosIniciais"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import DivisaoService from 'src/services/DivisaoService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IDivisaoResponse } from 'src/interfaces/IDivisao';

export default defineComponent({
  name: 'DivisaoForm',

  components: {
    TForm,
  },

  setup() {
    const dadosIniciais = ref({} as IDivisaoResponse);

    const route = useRoute();
    const router = useRouter();
    const service = DivisaoService;

    const inputDesign = 'outlined';
    const inputDense = false;

    const form = ref<IForm>({
      title: 'Cadastro de divisão',
      fields: [
        {
          label: 'Descrição',
          name: 'descricao',
          dense: inputDense,
          type: 'text',
          cols: 6,
          required: true,
          design: inputDesign,
          requiredRules: [
            (val) => (val && val.length > 0) || 'O campo é obrigatório.',
            (val) =>
              (val && val.length <= 55) ||
              'O campo deve ter no máximo 55 caracteres.',
          ],
        },
        {
          label: 'Transf. vendedor de setor',
          name: 'transfereVendedorSetor',
          dense: inputDense,
          type: 'toggle',
          cols: 6,
          required: false,
        },
        {
          label: 'Transf. vendedor de setor inativo',
          name: 'transfereVendedorSetorInativo',
          dense: inputDense,
          type: 'toggle',
          cols: 6,
          required: false,
        },
        {
          label: 'Visualizar cliente no relatório de vendas por cliente',
          name: 'verClienteRelatorioAcompanhamentoVendas',
          dense: inputDense,
          type: 'toggle',
          cols: 6,
          required: false,
        },
      ],
    });

    onMounted(async () => {
      const id = route.params.id;

      if (id) {
        const data = await service.obterPorId(Number(id));
        if (data) dadosIniciais.value = data;
      }
    });

    async function onSubmit(formData: any) {
      formData.id = dadosIniciais.value.id;

      const data = dadosIniciais.value.id
        ? await service.atualizar(formData)
        : await service.inserir(formData);

      if (data) router.back();
    }

    function onCancelar() {
      router.push({ name: 'DivisaoIndex' });
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
