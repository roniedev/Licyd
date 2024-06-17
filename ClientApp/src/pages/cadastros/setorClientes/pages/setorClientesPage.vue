<template>
  <jump-crud :crud="crud"></jump-crud>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { ColumnType } from 'src/components/tables/enums/ColumnType';
import { IJumpCrud } from 'src/components/crud/interfaces/IJumpCrud';
import { IJumpInput } from 'src/components/forms/interfaces/IJumpInput';
import { FormElementType } from 'src/components/forms/enums/FormElementType';
import { IJumpSelectServer } from 'src/components/forms/interfaces/IJumpSelectServer';

import JumpCrud from 'src/components/crud/JumpCrud.vue';
import SetorClientesService from '../services/setorClientesService';
import { IJumpToggle } from 'src/components/forms/interfaces/IJumpToggle';

export default defineComponent({
  name: 'SetorClientesPage',

  components: {
    JumpCrud,
  },

  setup() {
    const service = new SetorClientesService('SetorClientes');

    const crud = ref<IJumpCrud>({
      jumpCrudTable: {
        title: 'Setor x Clientes',
        service: service,
        componentName: 'SetorClientesPage',
        showDeleteImport: false,
        showActivateImport: false,
        rows: [],
        columns: [
          {
            field: 'id',
            name: 'id',
            label: 'Id (código setor x cliente)',
            type: ColumnType.number,
          },
          {
            field: 'clienteId',
            name: 'clienteId',
            label: 'Código cliente',
            type: ColumnType.number,
          },
          {
            field: 'clienteDescricao',
            name: 'clienteDescricao',
            label: 'Descrição cliente',
            type: ColumnType.text,
          },
          {
            field: 'setorId',
            name: 'setorId',
            label: 'Código setor',
            type: ColumnType.number,
          },
          {
            field: 'setorDescricao',
            name: 'setorDescricao',
            label: 'Descrição setor',
            type: ColumnType.text,
          },
          {
            field: 'vendedorId',
            name: 'vendedorId',
            label: 'Código vendedor',
            type: ColumnType.text,
          },
          {
            field: 'vendedorDescricao',
            name: 'vendedorDescricao',
            label: 'Descrição vendedor',
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
            label: 'Desc. setor superior',
            type: ColumnType.text,
          },
          {
            field: 'codigoAssociacaoSetor',
            name: 'codigoAssociacaoSetor',
            label: 'Cód. de associação setores',
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
            field: 'isInativarCadastroAnterior',
            name: 'isInativarCadastroAnterior',
            label: 'Inativar cadastro anterior',
            type: ColumnType.boolean,
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
            label: 'Código subdivisão divisão',
            type: ColumnType.number,
          },
          {
            field: 'subdivisaoDescricao',
            name: 'subdivisaoDescricao',
            label: 'Descrição subdivisão divisão',
            type: ColumnType.text,
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
            field: 'isCompraCentralizada',
            name: 'isCompraCentralizada',
            label: 'Compra centralizada?',
            type: ColumnType.boolean,
          },
          {
            field: 'centralComprasId',
            name: 'centralComprasId',
            label: 'Código da central de compras',
            type: ColumnType.number,
          },
          {
            field: 'centralComprasDescricao',
            name: 'centralComprasDescricao',
            label: 'Descrição da central de compras',
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
        title: 'Cadastro de setor x cadastro LX',
        fields: [
          {
            name: 'id',
            type: FormElementType.number,
            style: 'outlined',
            hidden: true,
          } as IJumpInput,
          {
            label: 'Setor',
            name: 'setorId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: '/SetorClientes/ObterSetores',
            labelNameField: 'setorDescricao',
            dense: true,
          } as IJumpSelectServer,
          {
            label: 'Cliente',
            name: 'clienteId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: '/SetorClientes/ObterClientes',
            labelNameField: 'clienteDescricao',
            dense: true,
          } as IJumpSelectServer,
          {
            label: 'Data inicial de vigência',
            name: 'dataInicialVigencia',
            type: FormElementType.date,
            style: 'outlined',
            cols: 6,
            dense: true,
          } as IJumpInput,
          {
            label: 'Data final de vigência',
            name: 'dataFinalVigencia',
            type: FormElementType.date,
            style: 'outlined',
            cols: 6,
            dense: true,
          } as IJumpInput,
          {
            label: 'Código associação setor (CAS)',
            name: 'codigoAssociacaoSetor',
            type: FormElementType.number,
            style: 'outlined',
            cols: 6,
            dense: true,
          } as IJumpInput,
          {
            label: 'Compra centralizada',
            name: 'isCompraCentralizada',
            type: FormElementType.toggle,
            style: 'outlined',
            cols: 6,
            dense: true,
          } as IJumpToggle,
          {
            label: 'Central de compras',
            name: 'centralComprasId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: '/SetorClientes/ObterClientes',
            labelNameField: 'centralComprasDescricao',
            dense: true,
            disabledRules: [
              {
                name: 'isCompraCentralizada',
                value: false,
              },
            ],
          } as IJumpSelectServer,
          {
            label: 'Inativar cadastro anterior (importação ou manual)',
            name: 'isInativarCadastroAnterior',
            type: FormElementType.toggle,
            style: 'outlined',
            cols: 12,
            dense: true,
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
