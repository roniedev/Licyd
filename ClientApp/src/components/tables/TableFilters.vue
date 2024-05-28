<template>
  <q-dialog v-model="isVisible" position="top" persistent>
    <q-card
      style="
        width: 800px;
        max-width: 80vw;
        border-radius: 10px;
        margin-top: 5px;
      "
    >
      <div class="row">
        <div class="col col-md-12">
          <q-card-section>
            <jump-form
              :form="form"
              @submit="applyFilters"
              @cancel="isVisible = !isVisible"
            >
              <template #bottomFields>
                <template v-for="(filtro, index) in filters" :key="index">
                  <q-chip
                    removable
                    color="primary"
                    style="color: white"
                    @remove="removeFilter(filtro)"
                    >{{ filtro.label }}: {{ filtro.value }}</q-chip
                  >
                </template>
              </template>
              <template v-slot:right-actions-before>
                <q-btn
                  flat
                  size="md"
                  color="negative"
                  icon="mdi-filter-remove-outline"
                  @click="removeAllFilters"
                >
                  <q-tooltip
                    anchor="top middle"
                    self="bottom middle"
                    :offset="[10, 10]"
                  >
                    Remover filtros
                  </q-tooltip></q-btn
                >
                <q-btn
                  flat
                  size="md"
                  color="primary"
                  icon="mdi-filter-plus-outline"
                  @click="insertFilter"
                >
                  <q-tooltip
                    anchor="top middle"
                    self="bottom middle"
                    :offset="[10, 10]"
                  >
                    Filtrar e novo
                  </q-tooltip></q-btn
                >
              </template>
            </jump-form>
            <q-separator />
          </q-card-section>
          <q-card-section>
            <jump-table-filters-chips
              :filters="filters"
              @removerFilter="removeFilter"
            />
          </q-card-section>
        </div>
      </div>
    </q-card>
  </q-dialog>
</template>

<script lang="ts">
import { ref } from 'vue';
import { computed, defineComponent, reactive, watch } from 'vue';
import { ITableColumn } from './interfaces/ITableColumn';
import { ITableFilter } from './interfaces/ITableFilter';
import { IJumpForm } from '../forms/interfaces/IJumpForm';
import { IJumpInput } from '../forms/interfaces/IJumpInput';

import JumpTableFiltersChips from './JumpTableFiltersChips.vue';
import JumpForm from '../forms/JumpForm.vue';

export default defineComponent({
  name: 'TableFilters',

  components: {
    JumpForm,
    JumpTableFiltersChips,
  },

  props: {
    columns: {
      type: Object as () => Array<ITableColumn>,
      required: true,
    },
    visible: {
      type: Boolean,
      required: true,
    },
  },

  emits: ['getFilters'],

  setup(props, { emit }) {
    const filters = ref<Array<ITableFilter>>([]);
    const isVisible = ref<boolean>(props.visible);

    watch(
      () => props.visible,
      () => (isVisible.value = !isVisible.value)
    );

    const form = computed(() => {
      const form: IJumpForm = {
        iconSubmitButton: 'mdi-filter-outline',
        labelSubmitButton: '',
        useToolipOnSubmitButton: true,
        tooltipTextSubmitButton: 'Filtrar e fechar',
        fields: [],
      };

      const filtrableColumns = props.columns.filter(
        (column) => column.filtrable ?? true
      );

      form.fields = reactive(
        filtrableColumns.map((column) => {
          if (column.filter) {
            return column.filter;
          }

          const input: IJumpInput = {
            label: column.label,
            name: column.name,
            style: 'outlined',
            cols: 6,
            dense: true,
          };

          return input;
        })
      );

      return form;
    });

    const applyFilters = () => {
      insertFilter();
      toggleIsVisible();
      emit('getFilters', filters.value);
    };

    const insertFilter = () => {
      form.value.fields.forEach((field) => {
        if (field.value) {
          const index = filters.value.findIndex(
            (filter) =>
              filter.name === field.name && filter.value === field.value
          );

          if (index === -1) {
            filters.value.push({
              label: field.label || field.name,
              name: field.name,
              value: field.value,
            });
          }
        }
      });

      emit('getFilters', filters.value);

      resetForm();
    };

    const removeFilter = (filter: ITableFilter) => {
      const index = filters.value.findIndex(
        (item) => item.name == filter.name && item.value == filter.value
      );

      if (index > -1) filters.value.splice(index, 1);
      emit('getFilters', filters.value);
    };

    const removeAllFilters = () => {
      filters.value = [];
      emit('getFilters', filters.value);
    };

    const resetForm = () => {
      form.value.fields.forEach((x) => (x.value = undefined));
    };

    const toggleIsVisible = () => {
      isVisible.value = !isVisible.value;
    };

    return {
      filters,
      isVisible,
      form,
      applyFilters,
      insertFilter,
      removeFilter,
      removeAllFilters,
    };
  },
});
</script>
