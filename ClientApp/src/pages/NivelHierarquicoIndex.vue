<template>
  <t-table-crud
    :table-props="tableProps"
    :service="service"
    form-component="NivelHierarquicoForm"
  />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TTableCrud from 'src/components/TTableCrud.vue';
import NivelHierarquicoService from 'src/services/NivelHierarquicoService';
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
    name: 'sequenciaHierarquica',
    required: true,
    align: 'center',
    label: 'Sequência hierárquica',
    field: 'sequenciaHierarquica',
    sortable: false,
    type: 'number',
  },
  {
    name: 'validarSetorAssociacaoLX',
    required: true,
    align: 'center',
    label: 'Validar associação LX',
    field: 'validarSetorAssociacaoLX',
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
  name: 'NivelHierarquicoIndex',

  components: {
    TTableCrud,
  },

  setup() {
    const service = NivelHierarquicoService;

    const tableProps: ITableProps = {
      titulo: 'Nível hierárquico',
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
    };
  },
});
</script>
