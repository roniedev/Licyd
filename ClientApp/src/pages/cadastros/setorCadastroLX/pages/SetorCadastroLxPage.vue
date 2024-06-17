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
import SetorCadastroLxService from '../services/SetorCadastroLxService';

export default defineComponent({
  name: 'SetorPage',

  components: {
    JumpCrud,
  },

  setup() {
    const service = new SetorCadastroLxService('SetorCadastroLX');

    const crud = ref<IJumpCrud>({
      jumpCrudTable: {
        title: 'Setor x Cadastro LX',
        service: service,
        componentName: 'SetorCadastroLxPage',
        rows: [],
        showDeleteImport: false,
        showActivateImport: false,
        columns: [
          {
            field: 'id',
            name: 'id',
            label: 'Id (código setor x cadastro LX)',
            type: ColumnType.number,
          },
          {
            field: 'setorId',
            name: 'setorId',
            label: 'Código Setor',
            type: ColumnType.number,
          },
          {
            field: 'setorDescricao',
            name: 'setorDescricao',
            label: 'Descrição setor',
            type: ColumnType.text,
          },
          {
            field: 'codigoLX',
            name: 'codigoLX',
            label: 'Código LX',
            type: ColumnType.text,
          },
          {
            field: 'codigoLxDescricao',
            name: 'codigoLxDescricao',
            label: 'Descrição LX',
            type: ColumnType.text,
          },
          {
            field: 'matricula',
            name: 'matricula',
            label: 'Matrícula',
            type: ColumnType.text,
          },
          {
            field: 'nomeColaborador',
            name: 'nomeColaborador',
            label: 'Nome colaborador',
            type: ColumnType.text,
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
            field: 'codigoLXSuperior',
            name: 'codigoLXSuperior',
            label: 'Código LX superior',
            type: ColumnType.text,
          },
          {
            field: 'codigoLXSuperiorDescricao',
            name: 'codigoLXSuperiorDescricao',
            label: 'Descrição LX superior',
            type: ColumnType.text,
          },
          {
            field: 'DivisaoId',
            name: 'DivisaoId',
            label: 'Cód. divisão (setor)',
            type: ColumnType.number,
          },
          {
            field: 'DivisaoDescricao',
            name: 'DivisaoDescricao',
            label: 'Descrição divisão (setor)',
            type: ColumnType.text,
          },
          {
            field: 'subdivisaoId',
            name: 'subdivisaoId',
            label: 'Código subdiv. setor',
            type: ColumnType.number,
          },
          {
            field: 'subdivisaoDescricao',
            name: 'subdivisaoDescricao',
            label: 'Descrição subdiv. (setor)',
            type: ColumnType.text,
          },
          {
            field: 'afastamentoId',
            name: 'afastamentoId',
            label: 'Código afastamento',
            type: ColumnType.number,
          },
          {
            field: 'motivoAfastamentoDescricao',
            name: 'motivoAfastamentoDescricao',
            label: 'Descrição afastamento',
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
        title: 'Cadastro de setor x cadastro LX',
        fields: [
          {
            label: 'Setor',
            name: 'setorId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: '/SetorCadastroLX/ObterSetores',
            labelNameField: 'setorDescricao',
          } as IJumpSelectServer,
          {
            label: 'Código LX',
            name: 'codigoLX',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: '/SetorCadastroLX/ObterRepresentantes',
            labelNameField: 'codigoLxDescricao',
          } as IJumpSelectServer,
          {
            label: 'Mátricula',
            name: 'matricula',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: '/ConsultaPadrao/ObterMatriculas',
            labelNameField: 'nomeColaborador',
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
        ],
      },
    });

    return {
      crud,
    };
  },
});
</script>
