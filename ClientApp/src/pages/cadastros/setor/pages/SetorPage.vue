<template>
  <jump-crud :crud="crud"></jump-crud>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { ColumnType } from 'src/components/tables/enums/ColumnType';
import { IJumpCrud } from 'src/components/crud/interfaces/IJumpCrud';
import { IJumpInput } from 'src/components/forms/interfaces/IJumpInput';
import { IJumpToggle } from 'src/components/forms/interfaces/IJumpToggle';
import { FormElementType } from 'src/components/forms/enums/FormElementType';
import { IJumpSelectServer } from 'src/components/forms/interfaces/IJumpSelectServer';

import SetorService from '../services/SetorService';
import JumpCrud from 'src/components/crud/JumpCrud.vue';

export default defineComponent({
  name: 'SetorPage',

  components: {
    JumpCrud,
  },

  setup() {
    const service = new SetorService('Setor');

    const crud = ref<IJumpCrud>({
      jumpCrudTable: {
        title: 'Setor',
        service: service,
        componentName: 'CarteiraSetorPage',
        showDeleteImport: false,
        showActivateImport: false,
        rows: [],
        columns: [
          {
            field: 'id',
            name: 'id',
            label: 'Id (código setor)',
            type: ColumnType.number,
          },
          {
            field: 'descricao',
            name: 'descricao',
            label: 'Descrição',
            type: ColumnType.text,
          },
          {
            field: 'nivelHierarquicoId',
            name: 'nivelHierarquicoId',
            label: 'Código nivel hierárquico',
            type: ColumnType.number,
          },
          {
            field: 'nivelHierarquicoDescricao',
            name: 'nivelHierarquicoDescricao',
            label: 'Nivel hierárquico',
            type: ColumnType.text,
          },
          {
            field: 'divisaoId',
            name: 'divisaoId',
            label: 'Código divisão',
            type: ColumnType.number,
          },
          {
            field: 'divisaoDescricao',
            name: 'divisaoDescricao',
            label: 'Descrição divisão',
            type: ColumnType.text,
          },
          {
            field: 'subdivisaoId',
            name: 'subdivisaoId',
            label: 'Código subdivisão',
            type: ColumnType.number,
          },
          {
            field: 'subdivisaoDescricao',
            name: 'subdivisaoDescricao',
            label: 'Descrição subdivisão',
            type: ColumnType.text,
          },
          {
            field: 'setorSuperiorId',
            name: 'setorSuperiorId',
            label: 'Código setor superior',
            type: ColumnType.number,
          },
          {
            field: 'setorSuperiorDescricao',
            name: 'setorSuperiorDescricao',
            label: 'Descrição setor superior',
            type: ColumnType.text,
          },
          {
            field: 'estabelecimentoId',
            name: 'estabelecimentoId',
            label: 'Estabelecimento',
            type: ColumnType.number,
          },
          {
            field: 'centroCustoId',
            name: 'centroCustoId',
            label: 'Centro de custo',
            type: ColumnType.number,
          },
          {
            field: 'centroCustoDescricao',
            name: 'centroCustoDescricao',
            label: 'Descrição centro de custo',
            type: ColumnType.text,
          },
          {
            field: 'dataInicialVigencia',
            name: 'dataInicialVigencia',
            label: 'Data inicial de vigência',
            type: ColumnType.date,
          },
          {
            field: 'dataFinalVigencia',
            name: 'dataFinalVigencia',
            label: 'Data final de vigência',
            type: ColumnType.date,
          },
          {
            field: 'vagoPadraoId',
            name: 'vagoPadraoId',
            label: 'Código vago padrão',
            type: ColumnType.number,
          },
          {
            field: 'vagoPadraoDescricao',
            name: 'vagoPadraoDescricao',
            label: 'Descrição vago padrão',
            type: ColumnType.text,
          },
          {
            field: 'setorPadraoPedidosId',
            name: 'setorPadraoPedidosId',
            label: 'Código setor padrão para pedidos',
            type: ColumnType.number,
          },
          {
            field: 'setorPadraoPedidosNome',
            name: 'setorPadraoPedidosNome',
            label: 'Setor padrão para pedidos',
            type: ColumnType.text,
          },
          {
            field: 'sedeId',
            name: 'sedeId',
            label: 'Código sede',
            type: ColumnType.number,
          },
          {
            field: 'sedeNome',
            name: 'sedeNome',
            label: 'Descrição sede',
            type: ColumnType.text,
          },
          {
            field: 'isAfetaPremiacao',
            name: 'isAfetaPremiacao',
            label: 'Afeta premiação?',
            type: ColumnType.boolean,
          },
          {
            field: 'isAfetaFaturamento',
            name: 'isAfetaFaturamento',
            label: 'Afeta faturamento?',
            type: ColumnType.boolean,
          },
          {
            field: 'isVisualizarSetorMapaVendas',
            name: 'isVisualizarSetorMapaVendas',
            label: 'Visualizar setor no mapa de vendas?',
            type: ColumnType.boolean,
          },
          {
            field: 'isSellIn',
            name: 'isSellIn',
            label: 'Sell-in?',
            type: ColumnType.boolean,
          },
          {
            field: 'isAlterarVagoPadraoAfastamento',
            name: 'isAlterarVagoPadraoAfastamento',
            label: 'Alterar para vago padrão nos afastamentos?',
            type: ColumnType.boolean,
          },
          {
            field: 'copiaSetorId',
            name: 'copiaSetorId',
            label: 'Código cópia setor',
            type: ColumnType.number,
          },
          {
            field: 'copiaSetorDescricao',
            name: 'copiaSetorDescricao',
            label: 'Descrição cópia setor',
            type: ColumnType.text,
          },
          {
            field: 'statusName',
            name: 'statusName',
            label: 'Status',
            type: ColumnType.text,
          },
          {
            field: 'reasonInactivation',
            name: 'reasonInactivation',
            label: 'Motivo da inativação',
            type: ColumnType.text,
          },
          {
            field: 'createdBy',
            name: 'createdBy',
            label: 'Criado por',
            type: ColumnType.text,
          },
          {
            field: 'createdAt',
            name: 'createdAt',
            label: 'Criado em',
            type: ColumnType.datetime,
          },
          {
            field: 'updatedBy',
            name: 'updatedBy',
            label: 'Atualizado por',
            type: ColumnType.text,
          },
          {
            field: 'updatedAt',
            name: 'updatedAt',
            label: 'Atualizado em',
            type: ColumnType.datetime,
          },
        ],
      },
      jumpForm: {
        title: 'Cadastro de setor',
        fields: [
          {
            label: 'Descrição',
            name: 'descricao',
            type: FormElementType.text,
            style: 'outlined',
            cols: 6,
          } as IJumpInput,
          {
            label: 'Nível hierárquico',
            name: 'nivelHierarquicoId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: '/Setor/ObterNiveisHierarquicos',
            labelNameField: 'nivelHierarquicoDescricao',
          } as IJumpSelectServer,
          {
            label: 'Setor superior',
            name: 'setorSuperiorId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: '/Setor/ObterSetores',
            labelNameField: 'setorSuperiorDescricao',
          } as IJumpSelectServer,
          {
            label: 'Divisão',
            name: 'divisaoId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: '/Setor/obterdivisoes',
            labelNameField: 'divisaoDescricao',
          } as IJumpSelectServer,
          {
            label: 'Subdivisão',
            name: 'subdivisaoId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: '/Setor/ObterSubdivisoes',
            labelNameField: 'subdivisaoDescricao',
          } as IJumpSelectServer,
          {
            label: 'Centro de custo',
            name: 'centroCustoId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: '/Setor/ObterCentrosCusto',
            labelNameField: 'centroCustoDescricao',
          } as IJumpSelectServer,
          {
            label: 'Vago padrão',
            name: 'vagoPadraoId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: '/Setor/ObterRepresentantes',
            labelNameField: 'vagoPadraoDescricao',
          } as IJumpSelectServer,
          {
            label: 'Setor padrão para pedidos',
            name: 'setorPadraoPedidosId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: '/Setor/ObterSetores',
            labelNameField: 'setorPadraoPedidosNome',
          } as IJumpSelectServer,
          {
            label: 'Sede',
            name: 'sedeId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: '/Setor/ObterSedes',
            labelNameField: 'sedeNome',
          } as IJumpSelectServer,
          {
            label: 'Estabelecimento',
            name: 'estabelecimentoId',
            type: FormElementType.number,
            style: 'outlined',
            cols: 6,
          } as IJumpInput,
          {
            label: 'Data inicial de vigência',
            name: 'dataInicialVigencia',
            type: FormElementType.date,
            style: 'outlined',
            cols: 6,
          } as IJumpInput,
          {
            label: 'Data final de vigência',
            name: 'dataFinalVigencia',
            type: FormElementType.date,
            style: 'outlined',
            cols: 6,
          } as IJumpInput,
          {
            label: 'Afeta premiação?',
            name: 'isAfetaPremiacao',
            type: FormElementType.toggle,
            style: 'outlined',
            cols: 12,
            value: false,
          } as IJumpToggle,
          {
            label: 'Afeta faturamento?',
            name: 'isAfetaFaturamento',
            type: FormElementType.toggle,
            style: 'outlined',
            cols: 12,
            value: false,
          } as IJumpToggle,
          {
            label: 'Visualizar setor no mapa de vendas?',
            name: 'isVisualizarSetorMapaVendas',
            type: FormElementType.toggle,
            style: 'outlined',
            cols: 12,
            value: false,
          } as IJumpToggle,
          {
            label: 'Alterar para vago padrão nos afastamentos?',
            name: 'isAlterarVagoPadraoAfastamento',
            type: FormElementType.toggle,
            style: 'outlined',
            cols: 12,
            value: false,
          } as IJumpToggle,
          {
            label: 'Sell-in?',
            name: 'isSellIn',
            type: FormElementType.toggle,
            style: 'outlined',
            cols: 12,
            value: false,
          } as IJumpToggle,
        ],
      },
    });

    return {
      crud,
    };
  },
});
</script>
