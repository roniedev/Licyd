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
          v-if="hasInsertPermission && (crudTable.showAddButton ?? true)"
          @click="onClickNew"
        />
      </template>
    </jump-table>
    <q-dialog v-model="inactivateFormVisible">
      <jump-form
        style="width: 500px"
        :form="crudTable.inactivateForm || inactivateForm"
        @submit="onSubmitInactivateForm"
      />
    </q-dialog>
  </q-page>
</template>

<script lang="ts">
import JumpTable from './JumpTable.vue';
import JumpForm from '../forms/JumpForm.vue';
import IJumpTableCrud from './interfaces/IJumpTableCrud';
import useAutenticacaoStore from 'src/stores/auth-store';

import { Dialog } from 'quasar';
import { ref, onBeforeMount, defineComponent, watch } from 'vue';
import { IJumpForm } from '../forms/interfaces/IJumpForm';
import { FormElementType } from '../forms/enums/FormElementType';
import { IJumpInput } from '../forms/interfaces/IJumpInput';
import { IJumpTable } from './interfaces/IJumpTable';
import { IBaseInactivate } from 'src/interfaces/IBaseInactivate';
import { ITableFilter } from './interfaces/ITableFilter';

export default defineComponent({
  name: 'JumpCrudTable',

  components: {
    JumpTable,
    JumpForm,
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

    const tableRef = ref<any>(null);

    const filters = ref<Array<ITableFilter>>([]);

    const refresh = ref<number>(0);
    const inactivateFormVisible = ref<boolean>(false);

    const store = useAutenticacaoStore();
    const component = props.crudTable.componentName;

    const hasGetAllPaginatedPermission = store.temAcessoNaAcao(
      component,
      'GetAllPaginated'
    );

    const hasInsertPermission = store.temAcessoNaAcao(component, 'Insert');
    const hasExportPermission = store.temAcessoNaAcao(component, 'Export');

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
      loadDataOnStart: props.crudTable.loadDataOnStart,
      filters: props.crudTable.filters,
      getServerRows: props.crudTable.service.getAllPaginated.bind(
        props.crudTable.service
      ),
      actions: [
        {
          name: 'Ativar',
          icon: '',
          visible(row) {
            const temAcesso = store.temAcessoNaAcao(
              props.crudTable.componentName,
              'Activate'
            );

            return row.statusId && row.statusId !== 3 && temAcesso;
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
            const canGetById = store.temAcessoNaAcao(component, 'GetById');
            const canUpdate = store.temAcessoNaAcao(component, 'Update');
            const canEditActive = props.crudTable.isEditActiveRows === true;
            const hasStatus = row.statusId != undefined && row.statudId != null;
            const hasPermission = canGetById && canUpdate;
            const isStatusValid = hasStatus
              ? row.statusId === 1 || row.statusId === 4
              : true;

            return (hasPermission && isStatusValid) || canEditActive;
          },
          onClick(row) {
            emit('edit', row);
          },
        },
        {
          name: 'Excluir',
          icon: '',
          visible() {
            return true;
            // const temAcesso = store.temAcessoNaAcao(
            //   props.crudTable.componentName,
            //   'Delete'
            // );
            // return (row.statusId === 1 || row.statusId === 2) && temAcesso;
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
            const temAcesso = store.temAcessoNaAcao(
              props.crudTable.componentName,
              'Inactivate'
            );
            return row.statusId === 3 && temAcesso;
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
            const temAcessoGetById = store.temAcessoNaAcao(
              props.crudTable.componentName,
              'GetById'
            );

            return temAcessoGetById;
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

    onBeforeMount(() => {
      setActions();
    });

    watch(
      () => props.crudTable.filters,
      (newFilters) => {
        table.value.filters = [...filters.value, ...(newFilters || [])];
      }
    );

    return {
      table,
      tableRef,
      refresh,
      inactivateForm,
      inactivateFormVisible,
      hasInsertPermission,
      hasExportPermission,
      hasGetAllPaginatedPermission,
      onClickNew,
      refreshRows,
      exportToExcel,
      onSubmitInactivateForm,
    };
  },
});
</script>
