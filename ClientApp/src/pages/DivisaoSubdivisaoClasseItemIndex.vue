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
import DivisaoSubdivisaoClasseItemService from 'src/services/DivisaoSubdivisaoClasseItemService';
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
    name: 'classeItemDescricao',
    required: true,
    align: 'center',
    label: 'Classe item',
    field: 'classeItemDescricao',
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
    name: 'dataVigenciaInicial',
    required: true,
    align: 'center',
    label: 'Data de vigência inicial',
    field: 'dataVigenciaInicial',
    sortable: false,
    type: 'date',
  },
  {
    name: 'dataVigenciaFinal',
    required: true,
    align: 'center',
    label: 'Data de vigência final',
    field: 'dataVigenciaFinal',
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
  name: 'DivisaoSubdivisaoClasseItemIndex',

  components: {
    TTableCrud,
  },

  setup() {
    const service = DivisaoSubdivisaoClasseItemService;
    const formComponent = ref('DivisaoSubdivisaoClasseItemForm');

    const tableProps: ITableProps = {
      titulo: 'Divisão x Subdivisão x Classe de item',
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
