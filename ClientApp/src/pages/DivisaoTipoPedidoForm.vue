<template>
  <t-form
    :form="form"
    :initial-data="dadosIniciais"
    @submit="onSubmit"
    @cancelar="onCancelar"
  />
</template>

<script lang="ts">
import DivisaoTipoPedidoService from 'src/services/DivisaoTipoPedidoService';
import TForm from 'src/components/TForm/TForm.vue';

import { defineComponent, onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { IForm } from 'src/interfaces/components/IForm';
import { IDivisaoTipoPedidoResponse } from 'src/interfaces/IDivisaoTipoPedido';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export default defineComponent({
  name: 'DivisaoTipoPedidoForm',

  components: {
    TForm,
  },

  setup() {
    const dadosIniciais = ref({} as IDivisaoTipoPedidoResponse);

    const route = useRoute();
    const router = useRouter();
    const service = DivisaoTipoPedidoService;

    const dense = false;
    const design = 'outlined';

    const form = ref<IForm>({
      title: 'Cadastro de divisão x tipo de pedido',
      fields: [
        {
          label: 'Divisão',
          name: 'divisaoId',
          dense: dense,
          design: design,
          type: 'select',
          cols: 6,
          required: true,
          urlRequestOption: '/ConsultaPadrao/ObterDivisoes',
          labelSelect: 'divisaoDescricao',
        },
        {
          label: 'Tipo de pedido',
          name: 'tipoPedidoId',
          dense: dense,
          design: design,
          type: 'select',
          cols: 6,
          required: true,
          urlRequestOption: '/ConsultaPadrao/ObterTiposPedido',
          labelSelect: 'tipoPedidoDescricao',
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

      if (data) router.push({ name: 'DivisaoTipoPedidoIndex' });

      hideLoading();
    }

    function onCancelar() {
      router.push({ name: 'DivisaoTipoPedidoIndex' });
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
