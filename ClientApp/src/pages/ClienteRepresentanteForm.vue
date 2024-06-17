<template>
  <t-form :form="form" :initial-data="dataFromServer" @submit="onSubmit" />
</template>

<script lang="ts">
import ClienteRepresentanteService from 'src/services/ClienteRepresentanteService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IClienteRepresentanteResponse } from 'src/interfaces/IClienteRepresentante';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'ClienteRepresentanteForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as IClienteRepresentanteResponse);

    const route = useRoute();
    const router = useRouter();
    const service = ClienteRepresentanteService;

    const form = ref<IForm>({
      title: 'EDI - Cliente x Representante',
      fields: [
        {
          label: 'Cliente',
          name: 'clienteId',
          type: 'select',
          cols: 6,
          dense: true,
          design: 'standart',
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterClientes',
          labelSelect: 'clienteDescricao',
        },
        {
          label: 'Divisão',
          name: 'divisaoId',
          type: 'select',
          cols: 6,
          dense: true,
          design: 'standart',
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterDivisoes',
          labelSelect: 'divisaoDescricao',
        },
        {
          label: 'Subdivisão',
          name: 'subdivisaoId',
          type: 'select',
          cols: 6,
          dense: true,
          design: 'standart',
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterSubdivisoes',
          labelSelect: 'subdivisaoDescricao',
        },
        {
          label: 'Representante',
          name: 'representanteId',
          type: 'select',
          cols: 6,
          dense: true,
          design: 'standart',
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterRepresentantes',
          labelSelect: 'representanteDescricao',
        },
        {
          label: 'Data de vigência inicial',
          name: 'dataInicialVigencia',
          type: 'date',
          cols: 6,
          dense: true,
          design: 'standart',
          required: true,
        },
        {
          label: 'Data de vigência final',
          name: 'dataFinalVigencia',
          type: 'date',
          cols: 6,
          dense: true,
          design: 'standart',
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

    async function onSubmit(formValues: any) {
      showLoading();

      formValues.id = dataFromServer.value.id;

      const data = dataFromServer.value.id
        ? await service.atualizar(formValues)
        : await service.inserir(formValues);

      if (data) router.push({ name: 'ClienteRepresentanteIndex' });

      hideLoading();
    }

    return {
      form,
      dataFromServer,
      onSubmit,
    };
  },
});
</script>
