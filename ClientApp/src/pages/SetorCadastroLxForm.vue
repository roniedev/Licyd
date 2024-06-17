<template>
  <t-form
    :form="formProps"
    :initial-data="dataFromServer"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import SetorCadastroLxService from 'src/services/SetorCadastroLxService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { ISetorCadastroLxResponse } from 'src/interfaces/ISetorCadastroLx';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'SetorCadastroLxForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as ISetorCadastroLxResponse);

    const route = useRoute();
    const router = useRouter();
    const service = SetorCadastroLxService;

    const dense = true;
    const design = 'outlined';

    const formProps = ref<IForm>({
      title: 'Setor x cadastro LX',
      fields: [
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
        {
          label: 'Código LX',
          name: 'codigoLX',
          dense: dense,
          design: design,
          type: 'select',
          cols: 6,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterRepresentantes',
          labelSelect: 'codigoLxDescricao',
        },
        {
          label: 'Mátricula',
          name: 'matricula',
          dense: dense,
          design: design,
          type: 'select',
          cols: 6,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterMatriculas',
          labelSelect: 'nomeColaborador',
        },
        {
          label: 'Data inicial de vigência',
          name: 'dataInicialVigencia',
          dense: dense,
          design: design,
          type: 'date',
          cols: 6,
          required: true,
        },
        {
          label: 'Data final de vigência',
          name: 'dataFinalVigencia',
          dense: dense,
          design: design,
          type: 'date',
          cols: 6,
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

      if (data) router.push({ name: 'SetorCadastroLxIndex' });
      hideLoading();
    }

    function onCancelar() {
      router.push({ name: 'SetorCadastroLxIndex' });
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
