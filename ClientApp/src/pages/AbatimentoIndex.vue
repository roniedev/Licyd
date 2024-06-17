<template>
  <t-table-crud
    :table-props="tableProps"
    :service="service"
    form-component="AbatimentoForm"
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
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import TTableCrud from 'src/components/TTableCrud.vue';
import AbatimentoService from 'src/services/AbatimentoService';
import {
  ITableColumn,
  ITableProps,
} from 'src/interfaces/components/ITableCrud';
import { IForm } from 'src/interfaces/components/IForm';
import TForm from 'src/components/TForm/TForm.vue';
import { showLoading, hideLoading } from 'src/helpers/Loading';

export const colunas: ITableColumn[] = [
  {
    name: 'id',
    required: true,
    label: 'Id',
    align: 'center',
    field: 'id',
    sortable: false,
    type: 'number',
  },
  {
    name: 'pedidoId',
    required: true,
    align: 'center',
    label: 'Pedido',
    field: 'pedidoId',
    sortable: false,
    type: 'number',
  },
  {
    name: 'mesReferencia',
    required: true,
    align: 'center',
    label: 'Mês de referência',
    field: 'mesReferencia',
    sortable: false,
    type: 'number',
  },
  {
    name: 'anoReferencia',
    required: true,
    align: 'center',
    label: 'Ano de referência',
    field: 'anoReferencia',
    sortable: false,
    type: 'number',
  },
  {
    name: 'clienteId',
    required: true,
    align: 'center',
    label: 'Código cliente',
    field: 'clienteId',
    sortable: false,
    type: 'number',
  },
  {
    name: 'clienteDescricao',
    required: true,
    align: 'center',
    label: 'Descrição cliente',
    field: 'clienteDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'setorId',
    required: true,
    align: 'center',
    label: 'Código setor',
    field: 'setorId',
    sortable: false,
    type: 'number',
  },
  {
    name: 'setorDescricao',
    required: true,
    align: 'center',
    label: 'Descrição setor',
    field: 'setorDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'representanteId',
    required: true,
    align: 'center',
    label: 'Código vendedor',
    field: 'representanteId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'representanteDescricao',
    required: true,
    align: 'center',
    label: 'Descrição vendedor',
    field: 'representanteDescricao',
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
    name: 'codigo',
    required: true,
    align: 'center',
    label: 'Código (id)',
    field: 'codigo',
    sortable: false,
    type: 'text',
  },
  {
    name: 'motivoAbatimentoId',
    required: true,
    align: 'center',
    label: 'Código motivo abatimento',
    field: 'motivoAbatimentoId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'motivoAbatimentoDescricao',
    required: true,
    align: 'center',
    label: 'Motivo de abatimento',
    field: 'motivoAbatimentoDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'tipoDescontoId',
    required: true,
    align: 'center',
    label: 'Código tipo de desconto',
    field: 'tipoDescontoId',
    sortable: false,
    type: 'number',
  },
  {
    name: 'tipoDescontoDescricao',
    required: true,
    align: 'center',
    label: 'Descrição tipo de desconto',
    field: 'tipoDescontoDescricao',
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
    label: 'Código centro de custo',
    field: 'centroCustoId',
    sortable: false,
    type: 'number',
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
    name: 'tipoOperacaoId',
    required: true,
    align: 'center',
    label: 'Código tipo de operação',
    field: 'tipoOperacaoId',
    sortable: false,
    type: 'number',
  },
  {
    name: 'tipoOperacaoDescricao',
    required: true,
    align: 'center',
    label: 'Descrição tipo de operação',
    field: 'tipoOperacaoDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'valorFaturamento',
    required: true,
    align: 'center',
    label: 'Valor do faturamento',
    field: 'valorFaturamento',
    sortable: false,
    type: 'number',
  },
  {
    name: 'percentualDesconto',
    required: true,
    align: 'center',
    label: 'Percentual de desconto',
    field: 'percentualDesconto',
    sortable: false,
    type: 'text',
  },
  {
    name: 'valorDesconto',
    required: true,
    align: 'center',
    label: 'Valor do desconto',
    field: 'valorDesconto',
    sortable: false,
    type: 'text',
  },
  {
    name: 'notaFiscalSaida',
    required: true,
    align: 'center',
    label: 'Nota fiscal de saída',
    field: 'notaFiscalSaida',
    sortable: false,
    type: 'number',
  },
  {
    name: 'serieNotaFiscalSaida',
    required: true,
    align: 'center',
    label: 'Série da nota fiscal',
    field: 'serieNotaFiscalSaida',
    sortable: false,
    type: 'number',
  },
  {
    name: 'notaFiscalDevolucao',
    required: true,
    align: 'center',
    label: 'Nota fiscal de devolução',
    field: 'notaFiscalDevolucao',
    sortable: false,
    type: 'number',
  },
  {
    name: 'atendente',
    required: true,
    align: 'center',
    label: 'Atendente',
    field: 'atendente',
    sortable: false,
    type: 'text',
  },
  {
    name: 'observacao',
    required: true,
    align: 'center',
    label: 'Observação',
    field: 'observacao',
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
  name: 'AbatimentoIndex',

  components: {
    TTableCrud,
    TForm,
  },

  setup() {
    const service = AbatimentoService;

    const updateData = ref(0);
    const formInativarImportacao = ref({} as IForm);
    const formImportacaoInativarVisivel = ref(false);

    const tableProps: ITableProps = {
      titulo: 'Abatimentos',
      colunas: colunas,
      acoes: [],
      selecao: 'none',
      bordas: 'cell',
      filtros: [],
    };

    tableProps.colunas.forEach((item) => {
      tableProps.filtros.push({
        label: item.label,
        value: item.name,
        type: item.type,
      });
    });

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

    async function inativarViaImportacao(formData: any) {
      showLoading();

      await service.inativarViaImportacao(formData);

      updateData.value++;

      formImportacaoInativarVisivel.value =
        !formImportacaoInativarVisivel.value;

      hideLoading();
    }

    async function obterInativarLayout() {
      await service.obterInativarLayout();
    }

    return {
      service,
      tableProps,
      updateData,
      formInativarImportacao,
      formImportacaoInativarVisivel,
      inativarViaImportacao,
      obterInativarLayout,
    };
  },
});
</script>
