<template>
  <t-table-crud
    :table-props="tableProps"
    :service="service"
    form-component="ClienteRepresentanteForm"
  >
    <template #acoes>
      <q-btn
        flat
        icon="fa-solid fa-copy"
        title="Inserir um registro"
        class="q-ml-sm q-pa-sm"
        size="sm"
      />
    </template>
  </t-table-crud>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import TTableCrud from 'src/components/TTableCrud.vue';
import ClienteRepresentanteService from 'src/services/ClienteRepresentanteService';
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
    name: 'clienteDescricao',
    required: true,
    align: 'center',
    label: 'Cliente',
    field: 'clienteDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'ClienteRepresentanteDescricao',
    required: true,
    align: 'center',
    label: 'Divisão',
    field: 'ClienteRepresentanteDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'subClienteRepresentanteDescricao',
    required: true,
    align: 'center',
    label: 'Subdivisão',
    field: 'subClienteRepresentanteDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'representanteDescricao',
    required: true,
    align: 'center',
    label: 'Representante',
    field: 'representanteDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'dataInicialVigencia',
    required: true,
    align: 'center',
    label: 'Data inicial de vigência',
    field: 'dataInicialVigencia',
    sortable: false,
    type: 'date',
  },
  {
    name: 'dataFinalVigencia',
    required: true,
    align: 'center',
    label: 'Data final de vigência',
    field: 'dataFinalVigencia',
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
  name: 'ClienteRepresentanteIndex',

  components: {
    TTableCrud,
  },

  setup() {
    const service = ClienteRepresentanteService;

    const tableProps: ITableProps = {
      titulo: 'EDI - Cliente x Representante',
      colunas: colunas,
      acoes: [],
      selecao: 'none',
      bordas: 'cell',
      filtros: [],
    };

    tableProps.formImportar = {
      title: 'Importar dados',
      fields: [
        {
          label: 'Arquivo',
          name: 'file',
          type: 'file',
          cols: 12,
          dense: true,
          design: 'outlined',
          required: true,
        },
        {
          label: 'Inativar cadastro anterior?',
          name: 'inativarCadastroAnterior',
          type: 'toggle',
          cols: 12,
          dense: true,
          design: 'outlined',
          required: true,
        },
      ],
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
