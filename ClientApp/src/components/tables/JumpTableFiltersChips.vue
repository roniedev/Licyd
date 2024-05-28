<template>
  <div>
    <template v-for="(filter, index) in filters" :key="index">
      <q-chip
        removable
        color="primary"
        text-color="white"
        @remove="removeFilter(filter)"
      >
        <span class="label-chip"> {{ filter.label }}: {{ filter.value }}</span>
      </q-chip>
    </template>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from 'vue';
import { ITableFilter } from './interfaces/ITableFilter';

export default defineComponent({
  name: 'JumpTableFiltersChips',
  props: {
    filters: {
      type: Object as () => Array<ITableFilter>,
      required: true,
    },
  },

  emits: ['removerFilter'],

  setup(props, { emit }) {
    const selectedFilters = ref<Array<ITableFilter>>([]);

    watch(
      () => props.filters,
      (newFilters: ITableFilter[] | undefined) => {
        selectedFilters.value = newFilters || [];
      },
      { immediate: true }
    );

    const removeFilter = (filter: ITableFilter) => {
      emit('removerFilter', filter);
    };

    return {
      selectedFilters,
      removeFilter,
    };
  },
});
</script>

<style scoped>
.label-chip {
  max-width: 125px;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  font-size: 12px;
}
</style>
