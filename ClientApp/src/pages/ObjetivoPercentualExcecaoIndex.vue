<template>
  <t-table-crud
    :table-props="tableProps"
    :service="service"
    :form-component="formComponent"
  />
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import TTableCrud from 'src/components/TTableCrud.vue';
import ObjetivoPercentualExcecaoService from 'src/services/ObjetivoPercentualExcecaoService';
import {
  ITableColumn,
  ITableProps,
} from 'src/interfaces/components/ITableCrud';

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
    name: 'descricao',
    required: true,
    align: 'center',
    label: 'Descrição',
    field: 'descricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'dataInicial',
    required: true,
    align: 'center',
    label: 'Data inicial',
    field: 'dataInicial',
    sortable: false,
    type: 'date',
  },
  {
    name: 'dataFinal',
    required: true,
    align: 'center',
    label: 'Data final',
    field: 'dataFinal',
    sortable: false,
    type: 'date',
  },
  {
    name: 'objetivoPercentual1',
    required: true,
    align: 'center',
    label: 'Objetivo (%) 1',
    field: 'objetivoPercentual1',
    sortable: false,
    type: 'number',
  },
  {
    name: 'objetivoPercentual2',
    required: true,
    align: 'center',
    label: 'Objetivo (%) 2',
    field: 'objetivoPercentual2',
    sortable: false,
    type: 'number',
  },
  {
    name: 'objetivoPercentual3',
    required: true,
    align: 'center',
    label: 'Objetivo (%) 3',
    field: 'objetivoPercentual3',
    sortable: false,
    type: 'number',
  },
  {
    name: 'objetivoPercentual4',
    required: true,
    align: 'center',
    label: 'Objetivo (%) 4',
    field: 'objetivoPercentual4',
    sortable: false,
    type: 'number',
  },
  {
    name: 'objetivoPercentualBase',
    required: true,
    align: 'center',
    label: 'Objetivo (%) base',
    field: 'objetivoPercentualBase',
    sortable: false,
    type: 'number',
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
  name: 'ObjetivoPercentualExcecaoIndex',

  components: {
    TTableCrud,
  },

  setup() {
    const service = ObjetivoPercentualExcecaoService;
    const formComponent = ref('ObjetivoPercentualExcecaoForm');

    const tableProps: ITableProps = {
      titulo: 'Objetivo por percentual - exceção',
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

    return {
      service,
      tableProps,
      formComponent,
    };
  },
});
</script>
