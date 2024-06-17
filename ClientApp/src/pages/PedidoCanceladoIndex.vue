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
import PedidoCanceladoService from 'src/services/PedidoCanceladoService';
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
    name: 'representanteDescricao',
    required: true,
    align: 'center',
    label: 'Representante',
    field: 'representanteDescricao',
    sortable: false,
    type: 'text',
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
    name: 'pedidoId',
    required: true,
    align: 'center',
    label: 'Pedido',
    field: 'pedidoId',
    sortable: false,
    type: 'number',
  },
  {
    name: 'dataPedido',
    required: true,
    align: 'center',
    label: 'Data do pedido',
    field: 'dataPedido',
    sortable: false,
    type: 'date',
  },
  {
    name: 'dataCancelamento',
    required: true,
    align: 'center',
    label: 'Data de cancelamento',
    field: 'dataCancelamento',
    sortable: false,
    type: 'date',
  },
  {
    name: 'motivoCancelamento',
    required: true,
    align: 'center',
    label: 'Motivo de cancelamento',
    field: 'motivoCancelamento',
    sortable: false,
    type: 'text',
  },
  {
    name: 'valorPedido',
    required: true,
    align: 'center',
    label: 'Valor do pedido',
    field: 'valorPedido',
    sortable: false,
    type: 'number',
  },
  {
    name: 'situacao',
    required: true,
    align: 'center',
    label: 'Situação',
    field: 'situacao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'centroCustoDescricao',
    required: true,
    align: 'center',
    label: 'Centro de custo',
    field: 'centroCustoDescricao',
    sortable: false,
    type: 'text',
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
    name: 'setorDescricao',
    required: true,
    align: 'center',
    label: 'Setor',
    field: 'setorDescricao',
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
    name: 'clienteDescricao',
    required: true,
    align: 'center',
    label: 'Cliente',
    field: 'clienteDescricao',
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
    name: 'setorDescricao',
    required: true,
    align: 'center',
    label: 'Setor',
    field: 'setorDescricao',
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
  name: 'PedidoCanceladoIndex',

  components: {
    TTableCrud,
  },

  setup() {
    const service = PedidoCanceladoService;
    const formComponent = ref('PedidoCanceladoForm');

    const tableProps: ITableProps = {
      titulo: 'Manutenção de pedidos cancelados',
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
