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
import SetorClientesService from 'src/services/SetorClientesService';
import {
  ITableColumn,
  ITableProps,
} from 'src/interfaces/components/ITableCrud';
import { hideLoading, showLoading } from 'src/helpers/Loading';
import { IForm } from 'src/interfaces/components/IForm';
import TForm from 'src/components/TForm/TForm.vue';

export const colunas: ITableColumn[] = [
  {
    name: 'id',
    required: true,
    label: 'Id (código setor x cliente)',
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
    name: 'setorId',
    required: true,
    align: 'center',
    label: 'Cód. setor',
    field: 'setorId',
    sortable: false,
    type: 'number',
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
    name: 'vendedorId',
    required: true,
    align: 'center',
    label: 'Código vendedor',
    field: 'vendedorId',
    sortable: false,
    type: 'number',
  },
  {
    name: 'vendedorDescricao',
    required: true,
    align: 'center',
    label: 'Descrição vendedor',
    field: 'vendedorDescricao',
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
    type: 'number',
  },
  {
    name: 'setorSuperiorDescricao',
    required: true,
    align: 'center',
    label: 'Descrição setor superior',
    field: 'setorSuperiorDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'codigoAssociacaoSetor',
    required: true,
    align: 'center',
    label: 'Cód. de associação setores',
    field: 'codigoAssociacaoSetor',
    sortable: false,
    type: 'text',
  },
  {
    name: 'dataInicialVigencia',
    required: true,
    align: 'center',
    label: 'Data inicial',
    field: 'dataInicialVigencia',
    sortable: false,
    type: 'date',
  },
  {
    name: 'dataFinalVigencia',
    required: true,
    align: 'center',
    label: 'Data final',
    field: 'dataFinalVigencia',
    sortable: false,
    type: 'date',
  },
  {
    name: 'isInativarCadastroAnterior',
    required: true,
    align: 'center',
    label: 'Inativar cadastro anterior',
    field: 'isInativarCadastroAnterior',
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
    label: 'Descrição divisão',
    field: 'divisaoDescricao',
    sortable: false,
    type: 'text',
  },
  {
    name: 'subdivisaoId',
    required: true,
    align: 'center',
    label: 'Código subdivisão divisão',
    field: 'subdivisaoId',
    sortable: false,
    type: 'number',
  },
  {
    name: 'subdivisaoDescricao',
    required: true,
    align: 'center',
    label: 'Descrição subdivisão divisão',
    field: 'subdivisaoDescricao',
    sortable: false,
    type: 'text',
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
  name: 'SetorClientesIndex',

  components: {
    TTableCrud,
    TForm,
  },

  setup() {
    const updateData = ref(0);
    const service = SetorClientesService;
    const formComponent = ref('SetorClientesForm');
    const formInativarImportacao = ref({} as IForm);
    const formImportacaoInativarVisivel = ref(false);

    const tableProps: ITableProps = {
      titulo: 'Setor x Clientes',
      colunas: colunas,
      acoes: [],
      selecao: 'none',
      bordas: 'cell',
      filtros: [],
      isEditarVisible: false,
    };

    tableProps.colunas.forEach((item) => {
      tableProps.filtros.push({
        label: item.label,
        value: item.name,
        type: item.type,
      });
    });

    tableProps.formInativar = {
      title: 'Inativação setor x cliente',
      fields: [
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
          name: 'motivoInativacao',
          label: 'Motivo da inativação',
          type: 'textarea',
          cols: 12,
          dense: true,
          required: true,
          design: 'outlined',
        },
      ],
    };

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
      updateData,
      inativarViaImportacao,
      formImportacaoInativarVisivel,
      obterInativarLayout,
      formInativarImportacao,
    };
  },
});
</script>
