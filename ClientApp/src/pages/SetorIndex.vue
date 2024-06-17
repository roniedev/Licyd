<template>
  <t-table-crud
    :table-props="tableProps"
    :service="service"
    :form-component="formComponent"
    :update-data="updateData"
  >
    <template #acoes>
      <q-btn
        flat
        icon="fa-regular fa-file-excel"
        class="q-ml-sm q-pa-sm"
        title="Inativar registros via importação"
        label="inativar (importação)"
        @click="formImportacaoInativarVisivel = !formImportacaoInativarVisivel"
        size="sm"
      />
      <q-btn
        flat
        icon="fa-regular fa-file-excel"
        class="q-ml-sm q-pa-sm"
        title="Atualizar registros via importação"
        label="Atualizar (importação)"
        @click="
          formImportacaoAtualizarVisivel = !formImportacaoAtualizarVisivel
        "
        size="sm"
      />
    </template>
  </t-table-crud>
  <q-dialog persistent v-model="formImportacaoInativarVisivel">
    <t-form
      @submit="inativarViaImportacao"
      @cancelar="formImportacaoInativarVisivel = !formImportacaoInativarVisivel"
      :form="formInativarImportacao"
      style="width: 1000px"
    >
      <template #acoesEsquerda>
        <q-btn
          flat
          size="md"
          color="secondary"
          label="layout"
          icon="fa-regular fa-file-excel"
          @click="obterInativarLayout"
        />
      </template>
    </t-form>
  </q-dialog>
  <q-dialog persistent v-model="formImportacaoAtualizarVisivel">
    <t-form
      @submit="atualizarViaImportacao"
      @cancelar="
        formImportacaoAtualizarVisivel = !formImportacaoAtualizarVisivel
      "
      :form="formAtualizarImportacao"
      style="width: 1000px"
    >
      <template #acoesEsquerda>
        <q-btn
          flat
          size="md"
          color="secondary"
          label="layout"
          icon="fa-regular fa-file-excel"
          @click="obterAtualizarLayout"
        />
      </template>
    </t-form>
  </q-dialog>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import {
  ITableColumn,
  ITableProps,
} from 'src/interfaces/components/ITableCrud';
import { Dialog } from 'quasar';
import { IForm } from 'src/interfaces/components/IForm';

import TTableCrud from 'src/components/TTableCrud.vue';
import TForm from 'src/components/TForm/TForm.vue';
import SetorService from 'src/services/SetorService';

export const colunas: ITableColumn[] = [
  {
    name: 'id',
    required: true,
    label: 'Id (código setor)',
    align: 'center',
    field: 'id',
    sortable: false,
    type: 'number',
  },
  {
    name: 'descricao',
    required: true,
    align: 'center',
    label: 'Descricao setor',
    field: 'descricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'nivelHierarquicoId',
    required: true,
    align: 'center',
    label: 'Código nivel hierárquico',
    field: 'nivelHierarquicoId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'nivelHierarquicoDescricao',
    required: true,
    align: 'center',
    label: 'Nivel hierárquico',
    field: 'nivelHierarquicoDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'divisaoId',
    required: true,
    align: 'center',
    label: 'Código divisão',
    field: 'divisaoId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'divisaoDescricao',
    required: true,
    align: 'center',
    label: 'Descrição divisão',
    field: 'divisaoDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'subdivisaoId',
    required: true,
    align: 'center',
    label: 'Código subdivisão',
    field: 'subdivisaoId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'subdivisaoDescricao',
    required: true,
    align: 'center',
    label: 'Descrição subdivisão',
    field: 'subdivisaoDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'setorSuperiorId',
    required: true,
    align: 'center',
    label: 'Cód. setor superior',
    field: 'setorSuperiorId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'setorSuperiorDescricao',
    required: true,
    align: 'center',
    label: 'Descrição setor superior',
    field: 'setorSuperiorDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'estabelecimentoId',
    required: true,
    align: 'center',
    label: 'Estabelecimento',
    field: 'estabelecimentoId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'centroCustoId',
    required: true,
    align: 'center',
    label: 'Centro de custo',
    field: 'centroCustoId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'centroCustoDescricao',
    required: true,
    align: 'center',
    label: 'Descrição centro de custo',
    field: 'centroCustoDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'dataInicialVigencia',
    required: true,
    align: 'center',
    label: 'Data vig. inicial',
    field: 'dataInicialVigencia',
    sortable: false,
    type: 'date',
  },
  {
    name: 'dataFinalVigencia',
    required: true,
    align: 'center',
    label: 'Data vig. final',
    field: 'dataFinalVigencia',
    sortable: false,
    type: 'date',
  },
  {
    name: 'vagoPadraoId',
    required: true,
    align: 'center',
    label: 'Código vago padrão',
    field: 'vagoPadraoId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'vagoPadraoDescricao',
    required: true,
    align: 'center',
    label: 'Descrição vago padrão',
    field: 'vagoPadraoDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'isAfetaPremiacao',
    required: true,
    align: 'center',
    label: 'Afeta premiação?',
    field: 'isAfetaPremiacao',
    sortable: false,
    type: 'boolean',
  },
  {
    name: 'isAfetaFaturamento',
    required: true,
    align: 'center',
    label: 'Afeta faturamento?',
    field: 'isAfetaFaturamento',
    sortable: false,
    type: 'boolean',
  },
  {
    name: 'isVisualizarSetorMapaVendas',
    required: true,
    align: 'center',
    label: 'Visualizar setor no mapa de vendas?',
    field: 'isVisualizarSetorMapaVendas',
    sortable: false,
    type: 'boolean',
  },
  {
    name: 'isSellIn',
    required: true,
    align: 'center',
    label: 'Sell-in?',
    field: 'isSellIn',
    sortable: false,
    type: 'boolean',
  },
  {
    name: 'isAlterarVagoPadraoAfastamento',
    required: true,
    align: 'center',
    label: 'Alterar para vago padrão nos afastamentos?',
    field: 'isAlterarVagoPadraoAfastamento',
    sortable: false,
    type: 'boolean',
  },
  {
    name: 'copiaSetorId',
    required: true,
    align: 'center',
    label: 'Código cópia setor',
    field: 'copiaSetorId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'copiaSetorDescricao',
    required: true,
    align: 'center',
    label: 'Descrição cópia setor',
    field: 'copiaSetorDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'statusDescricao',
    required: true,
    align: 'center',
    label: 'Status',
    field: 'statusDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'motivoInativacao',
    required: true,
    align: 'center',
    label: 'Motivo inativação',
    field: 'motivoInativacao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'criadoPor',
    required: true,
    align: 'center',
    label: 'Criado por',
    field: 'criadoPor',
    sortable: false,
    type: 'text',
  },
  {
    name: 'criadoEm',
    required: true,
    align: 'center',
    label: 'Criado em',
    field: 'criadoEm',
    sortable: false,
    type: 'date',
  },
  {
    name: 'atualizadoPor',
    required: true,
    align: 'center',
    label: 'Atualizado por',
    field: 'atualizadoPor',
    sortable: false,
    type: 'text',
  },
  {
    name: 'atualizadoEm',
    required: true,
    align: 'center',
    label: 'Atualizado em',
    field: 'atualizadoEm',
    sortable: false,
    type: 'date',
  },
];

export default defineComponent({
  name: 'SetorIndex',

  components: {
    TTableCrud,
    TForm,
  },

  setup() {
    const updateData = ref(0);
    const formComponent = ref('SetorForm');
    const formInativarImportacao = ref({} as IForm);
    const formAtualizarImportacao = ref({} as IForm);
    const formImportacaoInativarVisivel = ref(false);
    const formImportacaoAtualizarVisivel = ref(false);

    const service = SetorService;

    const tableProps: ITableProps = {
      titulo: 'Setor',
      colunas: colunas,
      acoes: [],
      selecao: 'none',
      bordas: 'cell',
      filtros: [],
    };

    tableProps.acoes.push({
      nome: 'Clone completo',
      icon: 'fa-solid fa-clone',
      onClick: cloneCompleto,
    });

    tableProps.colunas.forEach((item) => {
      tableProps.filtros.push({
        label: item.label,
        value: item.name,
        type: item.type,
      });
    });

    tableProps.formInativar = {
      title: 'Inativar setor',
      fields: [
        {
          name: 'dataFinalVigencia',
          label: 'Data final de vigência',
          type: 'date',
          cols: 12,
          dense: true,
          required: true,
          design: 'outlined',
        },
        {
          name: 'motivoInativacao',
          label: 'Motivo da inativação',
          type: 'textarea',
          cols: 12,
          dense: true,
          required: true,
          design: 'outlined',
        },
      ],
    };

    formInativarImportacao.value = {
      title: 'Inativar via importação',
      fields: [
        {
          cols: 12,
          dense: true,
          label: 'Arquivo',
          name: 'file',
          required: true,
          type: 'file',
          design: 'outlined',
        },
      ],
    };

    formAtualizarImportacao.value = {
      title: 'Atualizar via importação',
      fields: [
        {
          cols: 12,
          dense: true,
          label: 'Arquivo',
          name: 'file',
          required: true,
          type: 'file',
          design: 'outlined',
        },
      ],
    };

    async function cloneCompleto(linha: any) {
      Dialog.create({
        title: 'Confirmação de cópia completa',
        message:
          'Tem certeza que deseja fazer uma cópia completa do registro selecionado?',
        cancel: true,
        persistent: true,
      }).onOk(async () => {
        const sucesso = await service.cloneCompleto(linha.id);
        if (sucesso) updateData.value++;
      });
    }

    async function inativarViaImportacao(formData: any) {
      await service.inativarViaImportacao(formData);

      updateData.value++;

      formImportacaoInativarVisivel.value =
        !formImportacaoInativarVisivel.value;
    }

    async function atualizarViaImportacao(formData: any) {
      await service.atualizarViaImportacao(formData);

      updateData.value++;

      formImportacaoAtualizarVisivel.value =
        !formImportacaoAtualizarVisivel.value;
    }

    async function obterInativarLayout() {
      await service.obterInativarLayout();
    }

    async function obterAtualizarLayout() {
      await service.obterAtualizarLayout();
    }

    return {
      service,
      updateData,
      tableProps,
      formComponent,
      formInativarImportacao,
      formImportacaoInativarVisivel,
      formAtualizarImportacao,
      formImportacaoAtualizarVisivel,
      inativarViaImportacao,
      obterInativarLayout,
      atualizarViaImportacao,
      obterAtualizarLayout,
    };
  },
});
</script>
