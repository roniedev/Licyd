<template>
  <t-table-crud
    :table-props="tableProps"
    :service="service"
    :form-component="formComponent"
    :update-data="updateData"
  >
    <template #acoes>
      <q-btn
        flat
        icon="fa-regular fa-file-excel"
        class="q-ml-sm q-pa-sm"
        title="Inativar registros via importação"
        label="inativar (importação)"
        @click="formImportacaoInativarVisivel = !formImportacaoInativarVisivel"
        size="sm"
      />
    </template>
  </t-table-crud>
  <q-dialog persistent v-model="formImportacaoInativarVisivel">
    <t-form
      @submit="inativarViaImportacao"
      @cancelar="formImportacaoInativarVisivel = !formImportacaoInativarVisivel"
      :form="formInativarImportacao"
      style="width: 1000px"
    >
      <template #acoesEsquerda>
        <q-btn
          flat
          size="md"
          color="secondary"
          label="layout"
          icon="fa-regular fa-file-excel"
          @click="obterInativarLayout"
        />
      </template>
    </t-form>
  </q-dialog>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import TTableCrud from 'src/components/TTableCrud.vue';
import SetorCadastroLxService from 'src/services/SetorCadastroLxService';
import {
  ITableColumn,
  ITableProps,
} from 'src/interfaces/components/ITableCrud';
import { IForm } from 'src/interfaces/components/IForm';
import TForm from 'src/components/TForm/TForm.vue';
import { hideLoading, showLoading } from 'src/helpers/Loading';

export const colunas: ITableColumn[] = [
  {
    name: 'id',
    required: true,
    align: 'center',
    label: 'Id (código setor x cadastro LX)',
    field: 'id',
    sortable: false,
    type: 'number',
  },
  {
    name: 'setorId',
    required: true,
    align: 'center',
    label: 'Código Setor',
    field: 'setorId',
    sortable: false,
    type: 'text',
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
    name: 'codigoLX',
    required: true,
    align: 'center',
    label: 'Código LX',
    field: 'codigoLX',
    sortable: false,
    type: 'text',
  },
  {
    name: 'codigoLxDescricao',
    required: true,
    align: 'center',
    label: 'Descrição LX',
    field: 'codigoLxDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'matricula',
    required: true,
    align: 'center',
    label: 'Mátricula',
    field: 'matricula',
    sortable: false,
    type: 'text',
  },
  {
    name: 'nomeColaborador',
    required: true,
    align: 'center',
    label: 'Nome colaborador',
    field: 'nomeColaborador',
    sortable: false,
    type: 'text',
  },
  {
    name: 'vagoPadraoId',
    required: true,
    align: 'center',
    label: 'Código vago padrão',
    field: 'vagoPadraoId',
    sortable: false,
    type: 'number',
  },
  {
    name: 'vagoPadraoDescricao',
    required: true,
    align: 'center',
    label: 'Descrição vago padrão',
    field: 'vagoPadraoDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'setorSuperiorId',
    required: true,
    align: 'center',
    label: 'Código setor superior',
    field: 'setorSuperiorId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'setorSuperiorDescricao',
    required: true,
    align: 'center',
    label: 'Desc. setor superior',
    field: 'setorSuperiorDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'codigoLXSuperior',
    required: true,
    align: 'center',
    label: 'Código LX superior',
    field: 'codigoLXSuperior',
    sortable: false,
    type: 'text',
  },
  {
    name: 'codigoLXSuperiorDescricao',
    required: true,
    align: 'center',
    label: 'Descrição LX superior',
    field: 'descricaoLXSuperior',
    sortable: false,
    type: 'text',
  },
  {
    name: 'DivisaoId',
    required: true,
    align: 'center',
    label: 'Cód. divisão (setor)',
    field: 'divisaoId',
    sortable: false,
    type: 'number',
  },
  {
    name: 'DivisaoDescricao',
    required: true,
    align: 'center',
    label: 'Descrição divisão (setor)',
    field: 'divisaoDescricao',
    sortable: false,
    type: 'number',
  },
  {
    name: 'subdivisaoId',
    required: true,
    align: 'center',
    label: 'Código subdiv. setor',
    field: 'subdivisaoId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'subdivisaoDescricao',
    required: true,
    align: 'center',
    label: 'Descrição subdiv. setor',
    field: 'subdivisaoDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'afastamentoId',
    required: true,
    align: 'center',
    label: 'Código afastamento',
    field: 'afastamentoId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'motivoAfastamentoDescricao',
    required: true,
    align: 'center',
    label: 'Descrição afastamento',
    field: 'motivoAfastamentoDescricao',
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
    name: 'copiaSetorId',
    required: true,
    align: 'center',
    label: 'Código cópia setor',
    field: 'copiaSetorId',
    sortable: false,
    type: 'text',
  },
  {
    name: 'copiaSetorDescricao',
    required: true,
    align: 'center',
    label: 'Descrição cópia setor',
    field: 'copiaSetorDescricao',
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
    name: 'criadoEm',
    required: true,
    align: 'center',
    label: 'Criado em',
    field: 'criadoEm',
    sortable: false,
    type: 'date',
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
    name: 'atualizadoEm',
    required: true,
    align: 'center',
    label: 'Atualizado em',
    field: 'atualizadoEm',
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
];

export default defineComponent({
  name: 'SetorCadastroLxIndex',

  components: {
    TTableCrud,
    TForm,
  },

  setup() {
    const updateData = ref(0);
    const formInativarImportacao = ref({} as IForm);
    const formImportacaoInativarVisivel = ref(false);

    const service = SetorCadastroLxService;
    const formComponent = ref('SetorCadastroLxForm');

    const tableProps: ITableProps = {
      titulo: 'Setor x Cadastro LX',
      colunas: colunas,
      acoes: [],
      selecao: 'none',
      bordas: 'cell',
      filtros: [],
    };

    tableProps.formInativar = {
      title: 'Inativação e troca de setor x cadastro LX',
      fields: [
        {
          name: 'motivoInativacao',
          label: 'Motivo da inativação',
          type: 'text',
          cols: 12,
          dense: true,
          required: true,
          design: 'outlined',
        },
        {
          name: 'dataFinalVigencia',
          label: 'Data final de vigência',
          type: 'date',
          cols: 12,
          dense: true,
          required: true,
          design: 'outlined',
        },
        {
          name: 'novoCodigoLX',
          label: 'Novo código LX',
          type: 'select',
          cols: 12,
          dense: true,
          required: true,
          design: 'outlined',
          urlRequestOption: '/ConsultaPadrao/ObterRepresentantes',
        },
        {
          name: 'dataInicialVigencia',
          label: 'Data inicial de vigência',
          type: 'date',
          cols: 12,
          dense: true,
          required: true,
          design: 'outlined',
        },
        {
          name: 'matricula',
          label: 'Matrícula',
          type: 'select',
          cols: 12,
          dense: true,
          required: true,
          design: 'outlined',
          urlRequestOption: '/ConsultaPadrao/ObterMatriculas',
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

    formInativarImportacao.value = {
      title: 'Inativar via importação',
      fields: [
        {
          cols: 12,
          dense: true,
          label: 'Arquivo',
          name: 'file',
          required: true,
          type: 'file',
          design: 'outlined',
        },
      ],
    };

    async function inativarViaImportacao(formData: any) {
      showLoading();

      await service.inativarViaImportacao(formData);

      updateData.value++;

      formImportacaoInativarVisivel.value =
        !formImportacaoInativarVisivel.value;

      hideLoading();
    }

    async function obterInativarLayout() {
      await service.obterInativarLayout();
    }

    return {
      service,
      tableProps,
      formComponent,
      formInativarImportacao,
      formImportacaoInativarVisivel,
      updateData,
      inativarViaImportacao,
      obterInativarLayout,
    };
  },
});
</script>
