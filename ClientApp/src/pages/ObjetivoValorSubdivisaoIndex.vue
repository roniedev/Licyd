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
import ObjetivoValorSubdivisaoService from 'src/services/ObjetivoValorSubdivisaoService';
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
    name: 'divisaoDescricao',
    required: true,
    align: 'center',
    label: 'Divisão',
    field: 'divisaoDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'subdivisaoDescricao',
    required: true,
    align: 'center',
    label: 'Subdivisão',
    field: 'subdivisaoDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'anoReferencia',
    required: true,
    align: 'center',
    label: 'Ano de refêrencia',
    field: 'anoReferencia',
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
    name: 'valor',
    required: true,
    align: 'center',
    label: 'Valor',
    field: 'valor',
    sortable: false,
    type: 'number',
  },
  {
    name: 'valor',
    required: true,
    align: 'center',
    label: 'Valor',
    field: 'valor',
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
  name: 'ObjetivoValorSubdivisaoIndex',

  components: {
    TTableCrud,
  },

  setup() {
    const service = ObjetivoValorSubdivisaoService;
    const formComponent = ref('ObjetivoValorSubdivisaoForm');

    const tableProps: ITableProps = {
      titulo: 'Objetivo por valor - subdivisão',
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
