<template>
  <t-form
    :form="formProps"
    :initial-data="dataFromServer"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import SetorClientesService from 'src/services/SetorClientesService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { ISetorClientesResponse } from 'src/interfaces/ISetorClientes';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'SetorClientesForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as ISetorClientesResponse);

    const route = useRoute();
    const router = useRouter();
    const service = SetorClientesService;

    const dense = false;
    const design = 'outlined';

    const formProps = ref<IForm>({
      title: 'Cadastro de setor x clientes',
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
          label: 'Cliente',
          name: 'clienteId',
          dense: dense,
          design: design,
          type: 'select',
          cols: 6,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterClientes',
          labelSelect: 'clienteDescricao',
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
        {
          label: 'Código associação setor',
          name: 'codigoAssociacaoSetor',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
        },
        {
          label: 'Inativar cadastro anterior (importação ou manual)',
          name: 'isInativarCadastroAnterior',
          dense: dense,
          design: design,
          type: 'toggle',
          cols: 6,
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
      router.push({ name: 'SetorClientesIndex' });
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
