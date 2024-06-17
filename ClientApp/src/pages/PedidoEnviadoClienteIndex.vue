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
import PedidoEnviadoClienteService from 'src/services/PedidoEnviadoClienteService';
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
    name: 'clienteId',
    required: true,
    align: 'center',
    label: 'Código cliente',
    field: 'clienteId',
    sortable: false,
    type: 'text',
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
    name: 'divisaoId',
    required: true,
    align: 'center',
    label: 'Código divisão',
    field: 'divisaoId',
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
    name: 'subdivisaoId',
    required: true,
    align: 'center',
    label: 'Código subdivisão',
    field: 'subdivisaoId',
    sortable: false,
    type: 'number',
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
    type: 'number',
  },
  {
    name: 'representanteDescricao',
    required: true,
    align: 'center',
    label: 'Descricao vendedor',
    field: 'representanteDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'pedidoId',
    required: true,
    align: 'center',
    label: 'Pedido',
    field: 'pedidoId',
    sortable: false,
    type: 'date',
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
    name: 'estabelecimentoId',
    required: true,
    align: 'center',
    label: 'Estabelecimento',
    field: 'estabelecimentoId',
    sortable: false,
    type: 'number',
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
    name: 'valorFaturado',
    required: true,
    align: 'center',
    label: 'Valor do pedido',
    field: 'valorFaturado',
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
    name: 'percentualDescontoAutorizado',
    required: true,
    align: 'center',
    label: 'Percentual desconto autorizado',
    field: 'percentualDescontoAutorizado',
    sortable: false,
    type: 'text',
  },
  {
    name: 'valorDescontoConcedido',
    required: true,
    align: 'center',
    label: 'Valor desconto concedido',
    field: 'valorDescontoConcedido',
    sortable: false,
    type: 'text',
  },
  {
    name: 'quantidadeTotalEnviada',
    required: true,
    align: 'center',
    label: 'Quantidade total enviado',
    field: 'quantidadeTotalEnviada',
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
  name: 'PedidoEnviadoClienteIndex',

  components: {
    TTableCrud,
  },

  setup() {
    const service = PedidoEnviadoClienteService;
    const formComponent = ref('PedidoEnviadoClienteForm');

    const tableProps: ITableProps = {
      titulo: 'Manutenção de pedidos enviados por clientes',
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
