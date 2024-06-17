<template>
  <div>
    <q-btn
      flat
      dense
      icon="fa-solid fa-filter"
      size="sm"
      title="Mostrar/Ocultar filtros"
      class="btn-filtro"
      @click="$emit('toggleVisibility')"
    />
    <q-card flat bordered class="q-mb-md">
      <div class="filtro table-filtros q-pa-md" v-show="visible">
        <q-select
          outlined
          dense
          color="primary"
          label="Selecione um filtro"
          :options="filterProps"
          v-model="selectedFilter"
          map-options
          class="q-select"
        />
        <q-input
          outlined
          dense
          :type="inputFilterType"
          v-model="filterValue"
          :disable="!selectedFilter"
          class="q-input"
          @keyup.enter="inserir"
        >
          <template v-slot:after>
            <q-btn
              dense
              flat
              icon="fa-solid fa-magnifying-glass"
              size="sm"
              :disable="!filterValue"
              @click="inserir"
            />
          </template>
        </q-input>
        <div class="q-chips">
          <template v-for="(item, index) in chips" :key="index">
            <q-chip
              removable
              color="primary"
              text-color="white"
              @remove="remover(item.propriedade)"
            >
              {{ item.label }}:
              {{
                item.type === 'date'
                  ? new Date(item.valor).toLocaleDateString('pt-BR')
                  : item.valor
              }}
            </q-chip>
          </template>
        </div>
      </div>
    </q-card>
  </div>
</template>

<script lang="ts">
import moment from 'moment';
import { QInputProps } from 'quasar';
import { IFiltro, IFiltroProps } from 'src/interfaces/components/IFiltro';
import { computed, defineComponent, ref } from 'vue';

export default defineComponent({
  name: 'TFilter',

  props: {
    filterProps: {
      type: Array<IFiltroProps>,
      required: true,
      validator(values: Array<IFiltroProps>) {
        return values?.length > 0;
      },
    },
    visible: {
      type: Boolean,
      required: true,
      default: true,
    },
  },

  emits: ['toggleVisibility', 'updateFilters'],

  setup(props, { emit }) {
    const filterValue = ref('');
    const filters = ref([] as IFiltro[]);
    const selectedFilter = ref(null as unknown as IFiltroProps);

    const inputFilterType = computed(() => {
      let inputFilterType: QInputProps['type'] = 'text';

      if (selectedFilter.value) {
        if (selectedFilter.value.type === 'date') {
          inputFilterType = 'date';
        } else if (selectedFilter.value.type === 'number') {
          inputFilterType = 'number';
        }
      }

      return inputFilterType;
    });

    function inserir() {
      filters.value = filters.value.filter((item) => {
        return item.propriedade !== selectedFilter.value.value;
      });

      const filter: IFiltro = {
        label: selectedFilter.value.label,
        propriedade: selectedFilter.value.value || '',
        valor: filterValue.value,
        type: selectedFilter.value.type,
      };

      filters.value.push(filter);

      emit('updateFilters', filters.value);

      filterValue.value = '';
      selectedFilter.value = null as unknown as IFiltroProps;
    }

    function remover(prop: string) {
      filters.value = filters.value.filter((item) => {
        return item.propriedade !== prop;
      });

      emit('updateFilters', filters.value);
    }

    const chips = computed(() => {
      const chips: Array<IFiltro> = [];

      filters.value.forEach((filter) => {
        chips.push({
          label: filter.label,
          valor:
            filter.type === 'date'
              ? moment(filter.valor).format('DD/MM/YYYY')
              : filter.valor,
          propriedade: filter.propriedade,
        });
      });

      return chips;
    });

    return {
      filters,
      filterValue,
      selectedFilter,
      inputFilterType,
      chips,
      inserir,
      remover,
    };
  },
});
</script>

<style lang="sass" scoped>
.filtro
  display: flex
  align-content: flex-start
  align-items: flex-start
  margin: 0 0 10px 0

.filtro .q-select
  width: 25%
  margin-right: 15px

.filtro .q-input
  width: 35%

.filtro .q-chips
  width: 40%
  margin-left: 30px

.btn-filtro
  margin: 0 0 10px 0
</style>
