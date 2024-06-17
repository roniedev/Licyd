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

import JumpCrud from 'src/components/crud/JumpCrud.vue';
import CarteiraSetorService from '../services/CarteiraSetorService';

export default defineComponent({
  name: 'CarteiraSetorPage',

  components: {
    JumpCrud,
  },

  setup() {
    const service = new CarteiraSetorService('CarteiraSetor');

    const crud = ref<IJumpCrud>({
      jumpCrudTable: {
        title: 'Carteira de setor',
        service: service,
        componentName: 'CarteiraSetorPage',
        rows: [],
        columns: [
          {
            field: 'id',
            name: 'id',
            label: '#',
            type: ColumnType.number,
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
            label: 'Nome sede',
            type: ColumnType.number,
          },
          {
            field: 'sedeSetorId',
            name: 'sedeSetorId',
            label: 'Código setor sede',
            type: ColumnType.text,
            sortable: true,
          },
          {
            field: 'sedeSetorNome',
            name: 'sedeSetorNome',
            label: 'Nome sede setor',
            type: ColumnType.text,
          },
          {
            field: 'setorId',
            name: 'setorId',
            label: 'Cód. setor sede com acesso',
            type: ColumnType.number,
          },
          {
            field: 'setorNome',
            name: 'setorNome',
            label: 'Nome setor sede com acesso',
            type: ColumnType.number,
          },
          {
            field: 'codigoLX',
            name: 'codigoLX',
            label: 'Código LX',
            type: ColumnType.text,
          },
          {
            field: 'codigoLXNome',
            name: 'codigoLXNome',
            label: 'Desc. código LX',
            type: ColumnType.text,
          },
          {
            field: 'fazPedidoParaSetorOriginal',
            name: 'fazPedidoParaSetorOriginal',
            label: 'Faz pedido código original?',
            type: ColumnType.boolean,
          },
          {
            field: 'fazPedidoParaSetorSede',
            name: 'fazPedidoParaSetorSede',
            label: 'Faz pedido para sede?',
            type: ColumnType.boolean,
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
        title: 'Cadastro de carteira de setor',
        fields: [
          {
            name: 'id',
            type: FormElementType.number,
            hidden: true,
          } as IJumpInput,
          {
            label: 'Subdivisão',
            name: 'subdivisaoId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 12,
            urlGetOptions: 'CarteiraSetor/ObterSubdivisoes',
            labelNameField: 'subdivisaoNome',
            emitValue: false,
          } as IJumpSelectServer,
          {
            label: 'Sede',
            name: 'sedeId',
            type: FormElementType.selectServer,
            style: 'outlined',
            optionValue: 'sedeId',
            cols: 12,
            urlGetOptions: 'CarteiraSetor/ObterSedes',
            labelNameField: 'sedeNome',
            emitValue: false,
            urlParams: [{ paramName: 'descricaoCompleta', value: false }],
            formUrlParams: [{ fieldName: 'subdivisaoId' }],
            readonlyRules: [
              {
                name: 'subdivisaoId',
                value: undefined,
              },
              {
                name: 'subdivisaoId',
                value: null,
              },
            ],
            onChange(val) {
              preencherSetorCadastroLX(val);
            },
          } as IJumpSelectServer,
          {
            label: 'Setor',
            name: 'setorNome',
            type: FormElementType.text,
            style: 'outlined',
            cols: 12,
            readonly: true,
          } as IJumpInput,
          {
            label: 'Código LX',
            name: 'codigoLx',
            type: FormElementType.text,
            style: 'outlined',
            cols: 12,
            readonly: true,
          } as IJumpInput,
          {
            label: 'Sedes que serão acessadas',
            name: 'sedes',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 12,
            urlGetOptions: 'CarteiraSetor/ObterSedes',
            labelNameField: 'sedeNome',
            optionValue: 'sedeId',
            emitValue: false,
            multiselect: true,
            behavior: 'dialog',
            useChips: true,
            urlParams: [{ paramName: 'descricaoCompleta', value: false }],
            formUrlParams: [{ fieldName: 'subdivisaoId' }],
            onChange(val) {
              preencherSetorCadastroLX(val);
            },
          } as IJumpSelectServer,
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
            label: 'Faz pedido para o setor original?',
            name: 'fazPedidoParaSetorOriginal',
            type: FormElementType.toggle,
            style: 'outlined',
            cols: 12,
            value: false,
          } as IJumpToggle,
          {
            label: 'Faz pedido para o setor sede?',
            name: 'fazPedidoParaSetorSede',
            type: FormElementType.toggle,
            style: 'outlined',
            cols: 12,
            value: false,
          } as IJumpToggle,
        ],
      },
    });

    const preencherSetorCadastroLX = (val: any) => {
      crud.value.jumpForm.fields.forEach((field) => {
        if (field.name === 'codigoLx') field.value = val.codigoLX;
        if (field.name === 'setorNome') field.value = val.setorNome;
      });
    };

    return {
      crud,
    };
  },
});
</script>
