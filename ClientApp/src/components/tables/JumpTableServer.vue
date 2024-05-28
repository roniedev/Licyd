<template>
  <jump-table
    :table="table"
    @fetch-rows="fetchRows"
    @get-filters="getFilters"
  />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { IJumpTableServer } from './interfaces/IJumpTableServer';
import { ref } from 'vue';

import JumpTable from './JumpTable.vue';
import { ITableFilter } from './interfaces/ITableFilter';
import { IJumpTable } from './interfaces/IJumpTable';
import { IGetAllRequest } from 'src/interfaces/IGetAllRequest';

export default defineComponent({
  name: 'JumpTableServer',
  props: {
    tableServer: {
      type: Object as () => IJumpTableServer,
      required: true,
    },
  },
  components: {
    JumpTable,
  },
  setup(props) {
    let filters: Array<ITableFilter> = [];

    const table = ref<IJumpTable>({
      title: props.tableServer.title,
      columns: props.tableServer.columns,
      actions: props.tableServer.actions,
      selection: props.tableServer.selection,
      separator: props.tableServer.separator,
      dense: props.tableServer.dense,
      selectedRows: props.tableServer.selectedRows,
      rowKey: props.tableServer.rowKey,
      rows: [],
    });

    const fetchRows = async (pagination: any) => {
      const { page, rowsPerPage } = pagination;

      const data: IGetAllRequest = {
        skip: (page - 1) * rowsPerPage,
        take: rowsPerPage,
        filters: filters,
      };

      const response = await props.tableServer.getRows(data);

      if (response) {
        table.value.pagination = {
          page: page,
          rowsNumber: response.totalRows,
          rowsPerPage: rowsPerPage,
        };

        table.value.rows = response.rows;
      }
    };

    const getFilters = async (values: Array<ITableFilter>) => {
      filters = values || [];

      table.value.pagination = {
        page: 1,
        rowsPerPage: 10,
      };

      await fetchRows(table.value.pagination);
    };

    return {
      table,
      fetchRows,
      getFilters,
    };
  },
});
</script>
