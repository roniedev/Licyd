<template>
  <t-table-crud
    :table-props="tableProps"
    :service="service"
    form-component="DivisaoForm"
  />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TTableCrud from 'src/components/TTableCrud.vue';
import DivisaoService from 'src/services/DivisaoService';
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
    name: 'transfereVendedorSetor',
    required: true,
    align: 'center',
    label: 'Transf. vendedor de setor',
    field: 'transfereVendedorSetor',
    sortable: false,
    type: 'boolean',
  },
  {
    name: 'transfereVendedorSetorInativo',
    required: true,
    align: 'center',
    label: 'Transf. vendedor de setor inativo',
    field: 'transfereVendedorSetorInativo',
    sortable: false,
    type: 'boolean',
  },
  {
    name: 'verClienteRelatorioAcompanhamentoVendas',
    required: true,
    align: 'center',
    label: 'Ver cliente relatório acomp. de vendas',
    field: 'verClienteRelatorioAcompanhamentoVendas',
    sortable: false,
    type: 'boolean',
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
  name: 'DivisaoIndex',

  components: {
    TTableCrud,
  },

  setup() {
    const service = DivisaoService;

    const tableProps: ITableProps = {
      titulo: 'Divisão',
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
