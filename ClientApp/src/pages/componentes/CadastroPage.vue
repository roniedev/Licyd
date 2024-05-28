<template>
  <q-btn class="q-mb-md" color="primary" @click="addFilter">Add Filtro</q-btn>
  <jump-table-crud :crud-table="crud" />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import JumpTableCrud from 'src/components/tables/JumpTableCrud.vue';
import { ref } from 'vue';
import IJumpTableCrud from 'src/components/tables/interfaces/IJumpTableCrud';
import PaginaService from '../controleAcesso/pagina/services/PaginaService';
import { ColumnType } from 'src/components/tables/enums/ColumnType';

export default defineComponent({
  name: 'CadastroPage',

  components: {
    JumpTableCrud,
  },

  setup() {
    const crud = ref<IJumpTableCrud>({
      componentName: 'PaginaPage',
      service: new PaginaService('pagina'),
      rows: [],
      columns: [
        {
          label: '#',
          name: 'id',
          field: 'id',
          type: ColumnType.number,
        },
        {
          label: 'Nome',
          name: 'nome',
          field: 'nome',
          type: ColumnType.text,
        },
        {
          label: 'Módulo',
          name: 'moduloNome',
          field: 'moduloNome',
          type: ColumnType.number,
        },
        {
          label: 'Componente',
          name: 'componente',
          field: 'componente',
          type: ColumnType.text,
        },
        {
          label: 'API controller',
          name: 'apiController',
          field: 'apiController',
          type: ColumnType.text,
        },
        {
          label: 'Mostrar no menu',
          name: 'isVisivelNoMenu',
          field: 'isVisivelNoMenu',
          type: ColumnType.boolean,
        },
        {
          label: 'Ícone',
          name: 'icone',
          field: 'icone',
          type: ColumnType.text,
        },
        {
          label: 'Status',
          name: 'statusName',
          field: 'statusName',
          type: ColumnType.text,
          align: 'center',
        },
        {
          label: 'Motivo da inativação',
          name: 'reasonInactivation',
          field: 'reasonInactivation',
          type: ColumnType.text,
        },
        {
          label: 'Criado por',
          name: 'createdBy',
          field: 'createdBy',
          type: ColumnType.text,
        },
        {
          label: 'Criado em',
          name: 'createdAt',
          field: 'createdAt',
          type: ColumnType.datetime,
        },
        {
          label: 'Atualizado por',
          name: 'updatedBy',
          field: 'updatedBy',
          type: ColumnType.text,
        },
        {
          label: 'Atualizado em',
          name: 'updatedAt',
          field: 'updatedAt',
          type: ColumnType.datetime,
        },
      ],
    });

    const addFilter = () => {
      crud.value.filters = [];
      crud.value.filters.push({
        name: 'nome',
        value: 'Pagina',
        label: 'Nome',
      });
    };

    return {
      crud,
      addFilter,
    };
  },
});
</script>
