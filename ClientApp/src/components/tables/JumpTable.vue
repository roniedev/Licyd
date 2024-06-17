<template>
  <q-page>
    <q-table
      flat
      bordered
      v-model:pagination="pagination"
      v-model:selected="selectedRows"
      class="my-sticky-column-table"
      :dense="table.dense"
      :rows="rows"
      :columns="columns"
      :separator="table.separator || 'cell'"
      :selection="table.selection || 'none'"
      :visible-columns="visibleColumns"
      :row-key="table.rowKey || 'id'"
      @request="fetchRows"
    >
      <template v-slot:header-selection="scope">
        <q-toggle v-model="scope.selected" />
      </template>

      <template v-slot:body-selection="scope">
        <q-toggle v-model="scope.selected" />
      </template>

      <template v-slot:top-left>
        <span class="text-h6 q-mr-lg" v-if="table.title">{{
          table.title
        }}</span>

        <jump-table-filters-chips
          class="q-mr-lg"
          :filters="filters"
          @removerFilter="removeFilter"
        />
      </template>

      <template v-slot:top-right>
        <q-btn
          flat
          class="q-mr-xs q-pa-sm"
          title="Filtrar dados"
          size="md"
          :icon="filters.length > 0 ? 'mdi-filter-check' : 'mdi-filter-outline'"
          @click="isFiltersVisible = !isFiltersVisible"
        />
        <slot name="top-right"></slot>
      </template>

      <template v-slot:no-data>
        <div class="full-width row flex-center q-gutter-sm">
          {{
            filters.length > 0 ? 'Nenhum registro encontrado' : 'Não há dados'
          }}
        </div>
      </template>

      <template v-slot:body="props">
        <q-tr :props="props">
          <q-td
            auto-width
            :props="props"
            v-for="col in props.cols"
            :key="col.name"
            :style="{
              'max-width': col.maxWidth ? col.maxWidth + 'px' : 'none',
              'white-space': 'nowrap',
              overflow: 'hidden',
              'text-overflow': 'ellipsis',
            }"
          >
            <table-actions
              v-if="
                table.actions &&
                table.actions?.length > 0 &&
                col.name === 'actions'
              "
              :actions="table.actions"
              :row="props.row"
            />
            <template v-else-if="col.type === 'boolean'">
              <q-toggle size="xs" v-model="col.value" disable />
            </template>
            <template v-else>
              <span :title="col.value">{{
                getColumnValue(col.value, col)
              }}</span>
            </template>
          </q-td>
        </q-tr>
      </template>
    </q-table>

    <table-filters
      ref="tableFilters"
      :columns="table.columns"
      :visible="isFiltersVisible"
      @get-filters="getFilters"
    />
  </q-page>
</template>

<script lang="ts">
import { date } from 'quasar';
import { ColumnType } from './enums/ColumnType';
import { IJumpTable } from './interfaces/IJumpTable';
import { ITableFilter } from './interfaces/ITableFilter';
import { ITableColumn } from './interfaces/ITableColumn';
import { computed, defineComponent, ref, watch } from 'vue';

import TableActions from './TableActions.vue';
import TableFilters from './TableFilters.vue';
import JumpTableFiltersChips from './JumpTableFiltersChips.vue';
import { IGetAllRequest } from './interfaces/IGetAllRequest';

export default defineComponent({
  name: 'JumpTable',

  components: {
    TableFilters,
    TableActions,
    JumpTableFiltersChips,
  },

  props: {
    table: {
      type: Object as () => IJumpTable,
      required: true,
    },
    refresh: {
      type: Number,
      default: 0,
    },
  },

  emits: ['fetchRows', 'getFilters', 'getSelectedRows'],

  setup(props) {
    const tableFilters = ref<any>(null);

    const rows = ref<Array<any>>([]);
    const filteredRows = ref<Array<any | undefined>>();

    const filters = ref<Array<ITableFilter>>([]);
    const isFiltersVisible = ref<boolean>(false);

    const selectedRows = ref<Array<any>>([]);

    const pagination = ref<any>({
      page: 1,
      rowsPerPage: 10,
      rowsNumber: undefined,
      descending: false,
      sortBy: 'id',
    });

    const columns = computed(() => {
      const maxWidth = 200;

      const columns = props.table.columns.map((item: ITableColumn) => {
        const columnMap: ITableColumn = {
          name: item.name,
          label: item.label,
          field: item.field,
          type: item.type,
          align: item.align || 'center',
          sortable: item.sortable || true,
          visible: true,
          maxWidth: item.maxWidth || props.table.maxColumnWidth || maxWidth,
        };

        return columnMap;
      });

      if (props.table.actions && props.table.actions.length > 0) {
        const action: ITableColumn = {
          name: 'actions',
          label: '',
          field: 'actions',
          type: ColumnType.custom,
          align: 'center',
          sortable: false,
          visible: true,
          maxWidth: props.table.maxColumnWidth || maxWidth,
        };

        columns.unshift(action);
      }

      return columns;
    });

    const visibleColumns = computed(() => {
      const visibleColumns = columns.value
        .filter((coluna: ITableColumn) => coluna.visible ?? true)
        .map((coluna: ITableColumn) => coluna.name);

      if (props.table.actions && props.table.actions.length > 0)
        visibleColumns.push('actions');

      return visibleColumns;
    });

    const fetchRows = async (request?: any) => {
      if (!props.table.getServerRows) {
        console.log('JumpTable.getRows:', 'O método getRows é obrigatório.');
        rows.value = [];
        return;
      }

      const page = request?.pagination.page || 1;
      const rowsPerPage = request?.pagination.rowsPerPage || 8;

      const data: IGetAllRequest = {
        skip: (page - 1) * rowsPerPage,
        take: rowsPerPage,
        orderBy: request?.pagination.sortBy,
        isAscending: request?.pagination.descending,
        filters: [...(props.table.filters || []), ...filters.value],
      };

      const response = await props.table.getServerRows(data);

      if (response) {
        pagination.value = {
          page: page,
          rowsNumber: response.totalRows,
          rowsPerPage: rowsPerPage,
          sortBy: request?.pagination.sortBy,
          descending: request?.pagination.descending,
        };

        rows.value = response.rows;
      }
    };

    const getColumnValue = (valor: string, coluna: ITableColumn) => {
      switch (coluna.type) {
        case ColumnType.date:
          return date.formatDate(valor, 'DD/MM/YYYY');
        case ColumnType.datetime:
          return date.formatDate(valor, 'DD/MM/YYYY HH:mm:ss');
        case ColumnType.currency:
          const valorNumerico = parseFloat(valor);
          return !isNaN(valorNumerico)
            ? valorNumerico.toLocaleString('pt-BR', {
                style: 'currency',
                currency: 'BRL',
              })
            : valor;
        default:
          return valor;
      }
    };

    const getFilters = (formData: any) => {
      filters.value = formData;

      if (props.table.isServerRows) fetchRows();
      else {
        filteredRows.value = rows.value?.filter((row) => {
          return filters.value.every((filter) => {
            const name = filter.name;
            const value = filter.value.toLowerCase();
            return (
              row[name] && row[name].toString().toLowerCase().includes(value)
            );
          });
        });
      }
    };

    const removeFilter = (filter: ITableFilter) => {
      if (tableFilters.value) tableFilters.value.removeFilter(filter);
    };

    const getRows = async (newRows: any = undefined) => {
      if (props.table.isServerRows) {
        fetchRows();
        return;
      }

      rows.value =
        typeof props.table.rows === 'function'
          ? props.table.rows()
          : newRows || [];
    };

    watch(
      () => props.refresh,
      () => {
        getRows();
      }
    );

    watch(
      () => props.table.rows,
      async (newRows) => {
        getRows(newRows);
      },
      { immediate: true }
    );

    return {
      tableFilters,
      filters,
      pagination,
      columns,
      filteredRows,
      isFiltersVisible,
      selectedRows,
      visibleColumns,
      rows,
      fetchRows,
      getColumnValue,
      getFilters,
      removeFilter,
    };
  },
});
</script>

<style lang="sass">
.my-sticky-column-table
  max-width: 100%

  // .q-table__top
  //   background-color: #FFF

  thead
    font-weight: bolder

  // thead tr:first-child th:first-child
  //   background-color: #e5f4ff

  // td:first-child
  //   background-color: #fff

  th:first-child,
  td:first-child
    position: sticky
    left: 0
    z-index: 1

.my-sticky-last-column-table
  max-width: 100%

  // thead tr:last-child th:last-child
  //   background-color: #e0f2ff

  // td:last-child
  //   background-color: #e0f2ff

  th:last-child,
  td:last-child
    position: sticky
    right: 0
    z-index: 1
</style>
