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
import AfastamentoService from 'src/services/AfastamentoService';
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
    name: 'matricula',
    required: true,
    label: 'Matrícula',
    align: 'center',
    field: 'matricula',
    sortable: false,
    type: 'number',
  },
  {
    name: 'nomeColaborador',
    required: true,
    align: 'center',
    label: 'Colaborador',
    field: 'nomeColaborador',
    sortable: false,
    type: 'text',
  },
  {
    name: 'motivoAfastamentoId',
    required: true,
    align: 'center',
    label: 'Cód. motivo de afastamento',
    field: 'motivoAfastamentoId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'motivoAfastamentoDescricao',
    required: true,
    align: 'center',
    label: 'Motivo de afastamento',
    field: 'motivoAfastamentoDescricao',
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
  name: 'AfastamentoIndex',

  components: {
    TTableCrud,
  },

  setup() {
    const formComponent = ref('AfastamentoForm');
    const service = AfastamentoService;

    const tableProps: ITableProps = {
      titulo: 'Afastamento',
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
