<template>
  <q-page>
    <jump-table ref="tableRef" :table="table" :refresh="refresh">
      <template v-slot:top-right>
        <q-btn
          flat
          class="q-mr-xs q-pa-sm"
          title="Exportar para excel"
          icon="mdi-microsoft-excel"
          size="md"
          v-if="hasExportPermission"
          @click="exportToExcel"
        />
        <q-btn
          flat
          class="q-mr-xs q-pa-sm"
          title="Atualizar"
          icon="mdi-refresh"
          size="md"
          v-if="hasGetAllPaginatedPermission"
          @click="refreshRows"
        />
        <q-btn
          flat
          class="q-mr-xs q-pa-sm"
          title="Novo"
          icon="mdi-plus"
          size="md"
          v-if="hasInsertPermission"
          @click="onClickNew"
        />
        <button-dropdown :prop="headerActions" />
      </template>
    </jump-table>
    <q-dialog v-model="inactivateFormVisible">
      <jump-form
        style="width: 500px"
        :form="crudTable.inactivateForm || inactivateForm"
        @submit="onSubmitInactivateForm"
      />
    </q-dialog>

    <q-dialog persistent v-model="inserirPorImportacaoDialog">
      <jump-form
        style="width: 500px"
        :form="inserirPlanilhaForm"
        @submit="insertByImportSubmit"
        @cancel="inserirPorImportacaoDialog = !inserirPorImportacaoDialog"
      >
        <template v-slot:left-actions-after>
          <q-btn
            flat
            size="md"
            color="green"
            icon="mdi-download"
            label="layout"
            @click="getInsertLayout"
          />
        </template>
      </jump-form>
    </q-dialog>

    <q-dialog persistent v-model="atualizarPorImportacaoDialog">
      <jump-form
        style="width: 500px"
        :form="alterarPlanilhaForm"
        @submit="updateByImportSubmit"
        @cancel="atualizarPorImportacaoDialog = !atualizarPorImportacaoDialog"
      >
        <template v-slot:left-actions-after>
          <q-btn
            flat
            size="md"
            color="green"
            icon="mdi-download"
            label="layout"
            @click="getUpdateLayout"
          />
        </template>
      </jump-form>
    </q-dialog>

    <q-dialog persistent v-model="inativarPorImportacaoDialog">
      <jump-form
        style="width: 500px"
        :form="inativarPlanilhaForm"
        @submit="inactivateByImportSubmit"
        @cancel="inativarPorImportacaoDialog = !inativarPorImportacaoDialog"
      >
        <template v-slot:left-actions-after>
          <q-btn
            flat
            size="md"
            color="green"
            icon="mdi-download"
            label="layout"
            @click="getInactivateLayout"
          />
        </template>
      </jump-form>
    </q-dialog>

    <q-dialog persistent v-model="excluirPorImportacaoDialog">
      <jump-form
        style="width: 500px"
        :form="excluirPlanilhaForm"
        @submit="excluirByImportSubmit"
        @cancel="excluirPorImportacaoDialog = !excluirPorImportacaoDialog"
      >
        <template v-slot:left-actions-after>
          <q-btn
            flat
            size="md"
            color="green"
            icon="mdi-download"
            label="layout"
            @click="getDeleteLayout"
          />
        </template>
      </jump-form>
    </q-dialog>

    <q-dialog persistent v-model="ativarPorImportacaoDialog">
      <jump-form
        style="width: 500px"
        :form="ativarPlanilhaForm"
        @submit="ativarByImportSubmit"
        @cancel="ativarPorImportacaoDialog = !ativarPorImportacaoDialog"
      >
        <template v-slot:left-actions-after>
          <q-btn
            flat
            size="md"
            color="green"
            icon="mdi-download"
            label="layout"
            @click="getActivateLayout"
          />
        </template>
      </jump-form>
    </q-dialog>
  </q-page>
</template>

<script lang="ts">
import JumpTable from './JumpTable.vue';
import JumpForm from '../forms/JumpForm.vue';
import IJumpTableCrud from './interfaces/IJumpTableCrud';
import ButtonDropdown from '../buttons/ButtonDropdown.vue';
import useAutenticacaoStore from 'src/stores/autenticacao.store';

import { Dialog } from 'quasar';
import { ref, onBeforeMount, defineComponent } from 'vue';
import { IJumpForm } from '../forms/interfaces/IJumpForm';
import { FormElementType } from '../forms/enums/FormElementType';
import { IJumpInput } from '../forms/interfaces/IJumpInput';
import { IJumpTable } from './interfaces/IJumpTable';
import { IBaseInactivate } from './interfaces/IBaseInactivate';
import { IJumpInputFile } from '../forms/interfaces/IJumpInputFile';
import { IButtonDropdown } from '../buttons/interfaces/IButtonDropdown';

export default defineComponent({
  name: 'JumpCrudTable',

  components: {
    JumpTable,
    JumpForm,
    ButtonDropdown,
  },

  props: {
    crudTable: {
      type: Object as () => IJumpTableCrud,
      required: true,
    },
  },

  emits: ['new', 'edit', 'show'],

  setup(props, { emit }) {
    let rowId = 0;
    const store = useAutenticacaoStore();

    const tableRef = ref<any>(null);
    const refresh = ref<number>(0);
    const inserirFormVisible = ref<boolean>(false);
    const inactivateFormVisible = ref<boolean>(false);

    const inserirPorImportacaoDialog = ref<boolean>(false);
    const inativarPorImportacaoDialog = ref<boolean>(false);
    const atualizarPorImportacaoDialog = ref<boolean>(false);
    const excluirPorImportacaoDialog = ref<boolean>(false);
    const ativarPorImportacaoDialog = ref<boolean>(false);

    const headerActions = ref<IButtonDropdown>({
      dense: true,
      flat: true,
      items: [
        {
          label: 'Inserir por planilha',
          onClick: () => {
            inserirPorImportacaoDialog.value =
              !inserirPorImportacaoDialog.value;
          },
          visible: () => {
            const habilitado = props.crudTable.showInsertImport != false;

            const download = store.validarPaginaAcao(
              component,
              'GetInsertLayout'
            );

            const importar = store.validarPaginaAcao(
              component,
              'InsertByImport'
            );

            return habilitado && download && importar;
          },
        },
        {
          label: 'Alterar por planilha',
          onClick: () => {
            atualizarPorImportacaoDialog.value =
              !atualizarPorImportacaoDialog.value;
          },
          visible: () => {
            const habilitado = props.crudTable.showUpdateImport != false;

            const download = store.validarPaginaAcao(
              component,
              'GetUpdateLayout'
            );

            const importar = store.validarPaginaAcao(
              component,
              'UpdateByImport'
            );

            return habilitado && download && importar;
          },
        },
        {
          label: 'Inativar por planilha',
          onClick: () => {
            inativarPorImportacaoDialog.value =
              !inativarPorImportacaoDialog.value;
          },
          visible: () => {
            const habilitado = props.crudTable.showInactivateImport != false;

            const download = store.validarPaginaAcao(
              component,
              'InactivateByImport'
            );

            const importar = store.validarPaginaAcao(
              component,
              'GetInactivateLayout'
            );

            return habilitado && download && importar;
          },
        },
        {
          label: 'Excluir por planilha',
          onClick: () => {
            excluirPorImportacaoDialog.value =
              !excluirPorImportacaoDialog.value;
          },
          visible: () => {
            const habilitado = props.crudTable.showDeleteImport != false;

            const download = store.validarPaginaAcao(
              component,
              'DeleteByImport'
            );

            const importar = store.validarPaginaAcao(
              component,
              'GetDeleteLayout'
            );

            return habilitado && download && importar;
          },
        },
        {
          label: 'Ativar por planilha',
          onClick: () => {
            ativarPorImportacaoDialog.value = !ativarPorImportacaoDialog.value;
          },
          visible: () => {
            const habilitado = props.crudTable.showActivateImport != false;

            const download = store.validarPaginaAcao(
              component,
              'ActivateByImport'
            );

            const importar = store.validarPaginaAcao(
              component,
              'GetActivateLayout'
            );

            return habilitado && download && importar;
          },
        },
      ],
    });

    // eslint-disable-next-line vue/no-setup-props-destructure
    const component = props.crudTable.componentName;

    const hasGetAllPaginatedPermission = store.validarPaginaAcao(
      component,
      'GetAllPaginated'
    );

    const hasInsertPermission = store.validarPaginaAcao(component, 'Insert');
    const hasExportPermission = store.validarPaginaAcao(
      component,
      'ExportToExcel'
    );

    const inactivateForm = ref<IJumpForm>({
      title: 'Inativar registro',
      labelSubmitButton: 'enviar',
      fields: [
        {
          label: 'Motivo',
          name: 'reasonInactivation',
          type: FormElementType.textarea,
          style: 'outlined',
        } as IJumpInput,
      ],
    });

    const inserirPlanilhaForm = ref<IJumpForm>({
      title: 'Inserir por planilha',
      fields: [
        {
          name: 'file',
          label: 'Planilha',
          type: FormElementType.file,
          style: 'outlined',
        } as IJumpInputFile,
      ],
    });

    const alterarPlanilhaForm = ref<IJumpForm>({
      title: 'Atualizar por planilha',
      fields: [
        {
          name: 'file',
          label: 'Planilha',
          type: FormElementType.file,
          style: 'outlined',
        } as IJumpInputFile,
      ],
    });

    const inativarPlanilhaForm = ref<IJumpForm>({
      title: 'Inativar por planilha',
      fields: [
        {
          name: 'file',
          label: 'Planilha',
          type: FormElementType.file,
          style: 'outlined',
        } as IJumpInputFile,
      ],
    });

    const excluirPlanilhaForm = ref<IJumpForm>({
      title: 'Excluir por planilha',
      fields: [
        {
          name: 'file',
          label: 'Planilha',
          type: FormElementType.file,
          style: 'outlined',
        } as IJumpInputFile,
      ],
    });

    const ativarPlanilhaForm = ref<IJumpForm>({
      title: 'Ativar por planilha',
      fields: [
        {
          name: 'file',
          label: 'Planilha',
          type: FormElementType.file,
          style: 'outlined',
        } as IJumpInputFile,
      ],
    });

    // eslint-disable-next-line vue/no-setup-props-destructure
    const table = ref<IJumpTable>({
      title: props.crudTable.title,
      columns: props.crudTable.columns,
      dense: props.crudTable.dense,
      pagination: props.crudTable.pagination,
      selection: props.crudTable.selection,
      separator: props.crudTable.separator,
      selectedRows: props.crudTable.selectedRows,
      rowKey: props.crudTable.rowKey,
      rows: props.crudTable.rows,
      maxColumnWidth: props.crudTable.maxColumnWidth,
      isServerRows: props.crudTable.isServerRows || true,
      // eslint-disable-next-line vue/no-setup-props-destructure
      getServerRows: props.crudTable.service.getAll.bind(
        props.crudTable.service
      ),
      actions: [
        {
          name: 'Ativar',
          icon: '',
          visible(row) {
            const temAcesso = store.validarPaginaAcao(component, 'Activate');
            const statusValido = row?.statusId === 1 || row?.statusId === 3;
            return statusValido && temAcesso;
          },
          async onClick(row) {
            Dialog.create({
              title: 'Confirmação de ativação',
              message: 'Tem certeza que deseja ativar o registro?',
              cancel: true,
              persistent: true,
            }).onOk(async () => {
              const result = await props.crudTable.service.activate(row.id);
              if (result) refreshRows();
            });
          },
        },
        {
          name: 'Editar',
          icon: '',
          visible(row) {
            let editarAtivos = true;
            let statusValido = true;

            const podeListar = store.validarPaginaAcao(component, 'GetById');
            const podeAtualizar = store.validarPaginaAcao(component, 'Update');

            if (row?.statusId === 2) {
              editarAtivos = props.crudTable.isEditActiveRows || false;
            } else {
              statusValido = row?.statusId === 1 || row?.statusId === 4;
            }

            return podeListar && editarAtivos && statusValido && podeAtualizar;
          },
          onClick(row) {
            emit('edit', row);
          },
        },
        {
          name: 'Excluir',
          icon: '',
          visible(row) {
            const temAcesso = store.validarPaginaAcao(component, 'Delete');
            const statusValido = row.statusId === 1 || row.statusId == 4;
            return statusValido && temAcesso;
          },
          async onClick(row) {
            Dialog.create({
              title: 'Confirmação de exclusão',
              message: 'Tem certeza que deseja excluir o registro?',
              cancel: true,
              persistent: true,
            }).onOk(async () => {
              const result = await props.crudTable.service.delete(row.id);
              if (result) refreshRows();
            });
          },
        },
        {
          name: 'Inativar',
          icon: '',
          visible(row) {
            const temAcesso = store.validarPaginaAcao(component, 'Inactivate');
            return row?.statusId === 2 && temAcesso;
          },
          async onClick(row) {
            rowId = row.id;
            inactivateFormVisible.value = !inactivateFormVisible.value;
          },
        },
        {
          name: 'Visualizar',
          icon: '',
          visible() {
            return store.validarPaginaAcao(component, 'GetById');
          },
          onClick(row) {
            emit('show', row);
          },
        },
      ],
    });

    const onSubmitInactivateForm = async (formValues: any) => {
      const data: IBaseInactivate = {
        id: rowId,
        reasonInactivation: formValues['reasonInactivation'],
      };

      const result = await props.crudTable.service.inactivate(data);

      if (result) {
        rowId = 0;
        inactivateFormVisible.value = !inactivateFormVisible.value;
        refreshRows();
      }
    };

    const refreshRows = () => {
      refresh.value++;
    };

    const setActions = () => {
      table.value.actions = [
        ...(table.value.actions || []),
        ...(props.crudTable.actions || []),
      ];
    };

    const exportToExcel = async () => {
      await props.crudTable.service.export(
        tableRef.value.filters,
        'exportacao'
      );
    };

    const onClickNew = () => {
      emit('new');
    };

    const getInsertLayout = async () => {
      await props.crudTable.service.getInsertLayout();
    };

    const getUpdateLayout = async () => {
      await props.crudTable.service.getUpdateLayout();
    };

    const getInactivateLayout = async () => {
      await props.crudTable.service.getInactivateLayout();
    };

    const getDeleteLayout = async () => {
      await props.crudTable.service.getDeleteLayout();
    };

    const getActivateLayout = async () => {
      await props.crudTable.service.getActivateLayout();
    };

    const insertByImportSubmit = async (formValues: any) => {
      await props.crudTable.service.insertByImport(formValues);
      inserirPorImportacaoDialog.value = !inserirPorImportacaoDialog.value;
      refreshRows();
    };

    const updateByImportSubmit = async (formValues: any) => {
      await props.crudTable.service.updateByImport(formValues);
      atualizarPorImportacaoDialog.value = !atualizarPorImportacaoDialog.value;
      refreshRows();
    };

    const inactivateByImportSubmit = async (formValues: any) => {
      await props.crudTable.service.inactivateByImport(formValues);
      inativarPorImportacaoDialog.value = !inativarPorImportacaoDialog.value;
      refreshRows();
    };

    const excluirByImportSubmit = async (formValues: any) => {
      await props.crudTable.service.deleteByImport(formValues);
      excluirPorImportacaoDialog.value = !excluirPorImportacaoDialog.value;
      refreshRows();
    };

    const ativarByImportSubmit = async (formValues: any) => {
      await props.crudTable.service.activateByImport(formValues);
      ativarPorImportacaoDialog.value = !ativarPorImportacaoDialog.value;
      refreshRows();
    };

    onBeforeMount(() => {
      setActions();
    });

    return {
      table,
      tableRef,
      refresh,
      inactivateForm,
      inactivateFormVisible,
      hasInsertPermission,
      hasExportPermission,
      hasGetAllPaginatedPermission,
      inserirPlanilhaForm,
      inserirFormVisible,
      headerActions,
      alterarPlanilhaForm,
      inativarPlanilhaForm,
      inserirPorImportacaoDialog,
      atualizarPorImportacaoDialog,
      inativarPorImportacaoDialog,
      excluirPlanilhaForm,
      ativarPlanilhaForm,
      excluirPorImportacaoDialog,
      ativarPorImportacaoDialog,
      onClickNew,
      refreshRows,
      exportToExcel,
      onSubmitInactivateForm,
      getInsertLayout,
      getUpdateLayout,
      getInactivateLayout,
      insertByImportSubmit,
      updateByImportSubmit,
      inactivateByImportSubmit,
      getDeleteLayout,
      getActivateLayout,
      excluirByImportSubmit,
      ativarByImportSubmit,
    };
  },
});
</script>
