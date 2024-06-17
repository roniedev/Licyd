<template>
  <t-form
    :form="formProps"
    :initial-data="dataFromServer"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import UsuarioDivisaoService from 'src/services/UsuarioDivisaoService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IUsuarioDivisaoResponse } from 'src/interfaces/IUsuarioDivisao';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'UsuarioDivisaoForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as IUsuarioDivisaoResponse);

    const route = useRoute();
    const router = useRouter();
    const service = UsuarioDivisaoService;

    const dense = false;
    const design = 'outlined';

    const formProps = ref<IForm>({
      title: 'Cadastro de usuário x divisão',
      fields: [
        {
          label: 'Usuário',
          name: 'usuarioRede',
          dense: dense,
          design: design,
          type: 'select',
          cols: 12,
          required: true,
          urlRequestOption: '/ConsultaPadrao/ObterUsuarios',
        },
        {
          label: 'Farma',
          name: 'divisaoFarma',
          dense: dense,
          design: design,
          type: 'toggle',
          cols: 4,
          required: true,
        },
        {
          label: 'Hospitalar',
          name: 'divisaoHospitalar',
          dense: dense,
          design: design,
          type: 'toggle',
          cols: 4,
          required: true,
        },
        {
          label: 'Comex',
          name: 'divisaoComex',
          dense: dense,
          design: design,
          type: 'toggle',
          cols: 4,
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
      router.push({ name: 'UsuarioDivisaoIndex' });
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
