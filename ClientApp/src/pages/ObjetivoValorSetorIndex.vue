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
import ObjetivoValorSetorService from 'src/services/ObjetivoValorSetorService';
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
    name: 'setorId',
    required: true,
    align: 'center',
    label: 'Código setor',
    field: 'setorId',
    sortable: false,
    type: 'text',
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
    label: 'Ano de refêrencia',
    field: 'anoReferencia',
    sortable: false,
    type: 'number',
  },
  {
    name: 'valorObjetivo',
    required: true,
    align: 'center',
    label: 'Valor objetivo',
    field: 'valorObjetivo',
    sortable: false,
    type: 'number',
  },
  {
    name: 'valorCompromisso',
    required: true,
    align: 'center',
    label: 'Valor compromisso',
    field: 'valorCompromisso',
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
  name: 'ObjetivoValorSetorIndex',

  components: {
    TTableCrud,
  },

  setup() {
    const service = ObjetivoValorSetorService;
    const formComponent = ref('ObjetivoValorSetorForm');

    const tableProps: ITableProps = {
      titulo: 'Objetivo por valor - setor',
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
