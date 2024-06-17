<template>
  <t-form
    :form="formProps"
    :initial-data="dataFromServer"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import AssociacaoExcecaoSetorService from 'src/services/AssociacaoExcecaoSetorService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IAssociacaoExcecaoSetorResponse } from 'src/interfaces/IAssociacaoExcecaoSetor';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'AssociacaoExcecaoSetorForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as IAssociacaoExcecaoSetorResponse);

    const route = useRoute();
    const router = useRouter();
    const service = AssociacaoExcecaoSetorService;

    const dense = true;
    const design = 'outlined';

    const formProps = ref<IForm>({
      title: 'Cadastro de associação de setor x exceção',
      fields: [
        {
          label: 'Exceção',
          name: 'excecaoId',
          dense: dense,
          design: design,
          type: 'select',
          cols: 6,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterExcecoes',
          labelSelect: 'excecaoDescricao',
        },
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
      router.push({ name: 'AssociacaoExcecaoSetorIndex' });
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
