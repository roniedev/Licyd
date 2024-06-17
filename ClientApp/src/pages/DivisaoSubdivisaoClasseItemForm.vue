<template>
  <t-form
    :form="formProps"
    :initial-data="dataFromServer"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import DivisaoSubdivisaoClasseItemService from 'src/services/DivisaoSubdivisaoClasseItemService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IDivisaoSubdivisaoClasseItemResponse } from 'src/interfaces/IDivisaoSubdivisaoClasseItem';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'DivisaoSubdivisaoClasseItemForm',

  components: {
    TForm,
  },

  setup() {
    const dataFromServer = ref({} as IDivisaoSubdivisaoClasseItemResponse);

    const route = useRoute();
    const router = useRouter();
    const service = DivisaoSubdivisaoClasseItemService;

    const dense = false;
    const design = 'outlined';

    const formProps = ref<IForm>({
      title: 'Cadastro de divisão x subdivisão x classe de item',
      fields: [
        {
          label: 'Divisão',
          name: 'divisaoId',
          dense: dense,
          design: design,
          type: 'select',
          cols: 6,
          required: false,
          urlRequestOption: '/ConsultaPadrao/obterdivisoes',
          labelSelect: 'divisaoDescricao',
        },
        {
          label: 'Subdivisão',
          name: 'subdivisaoId',
          dense: dense,
          design: design,
          type: 'select',
          cols: 6,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterSubdivisoes',
          labelSelect: 'subdivisaoDescricao',
        },
        {
          label: 'Classe de item',
          name: 'classeItemId',
          dense: dense,
          design: design,
          type: 'select',
          cols: 6,
          required: false,
          urlRequestOption: '/ConsultaPadrao/ObterClassesItem',
          labelSelect: 'classeItemDescricao',
        },
        {
          label: 'Estabelecimento',
          name: 'estabelecimentoId',
          dense: dense,
          design: design,
          type: 'number',
          cols: 6,
          required: true,
        },
        {
          label: 'Data de vigência inicial',
          name: 'dataVigenciaInicial',
          dense: dense,
          design: design,
          type: 'date',
          cols: 6,
          required: true,
        },
        {
          label: 'Data de vigência final',
          name: 'dataVigenciaFinal',
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

      if (data) router.push({ name: 'DivisaoSubdivisaoClasseItemIndex' });

      hideLoading();
    }

    function onCancelar() {
      router.push({ name: 'DivisaoSubdivisaoClasseItemIndex' });
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
