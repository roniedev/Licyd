<template>
  <jump-crud :crud="crud" />
</template>

<script lang="ts">
import JumpCrud from 'src/components/crud/JumpCrud.vue';
import CategoriaItemService from '../services/CategoriaItemService';
import IJumpTableCrud from 'src/components/tables/interfaces/IJumpTableCrud';

import { defineComponent, ref } from 'vue';
import { ColumnType } from 'src/components/tables/enums/ColumnType';
import { IJumpForm } from 'src/components/forms/interfaces/IJumpForm';
import { IJumpInput } from 'src/components/forms/interfaces/IJumpInput';
import { FormElementType } from 'src/components/forms/enums/FormElementType';
import { IJumpCrud } from 'src/components/crud/interfaces/IJumpCrud';

export default defineComponent({
  name: 'CategoriaItemPage',

  components: {
    JumpCrud,
  },

  setup() {
    const service = new CategoriaItemService('CategoriaItem');

    const table: IJumpTableCrud = {
      title: 'Listagem de categoria de produtos',
      service: service,
      rows: [],
      componentName: 'CategoriaItemPage',
      columns: [
        {
          field: 'id',
          name: 'id',
          label: '#',
          type: ColumnType.number,
        },
        {
          field: 'nome',
          name: 'nome',
          label: 'Nome',
          type: ColumnType.text,
        },
        {
          field: 'descricao',
          name: 'descricao',
          label: 'Descrição',
          type: ColumnType.text,
        },
        {
          field: 'statusName',
          name: 'statusName',
          label: 'Status',
          type: ColumnType.text,
        },
        {
          field: 'createdBy',
          name: 'createdBy',
          label: 'Criado por',
          type: ColumnType.text,
        },
        {
          field: 'createdAt',
          name: 'createdAt',
          label: 'Criado em',
          type: ColumnType.datetime,
        },
        {
          field: 'updatedBy',
          name: 'updatedBy',
          label: 'Atualizado por',
          type: ColumnType.text,
        },
        {
          field: 'updatedAt',
          name: 'updatedAt',
          label: 'Atualizado em',
          type: ColumnType.datetime,
        },
      ],
    };

    const form: IJumpForm = {
      title: 'Cadastro de categoria de produtos',
      fields: [
        {
          name: 'id',
          type: FormElementType.number,
          hidden: true,
        } as IJumpInput,
        {
          name: 'nome',
          label: 'Nome',
          type: FormElementType.text,
          cols: 6,
          style: 'outlined',
        } as IJumpInput,
        {
          name: 'descricao',
          label: 'Descrição',
          type: FormElementType.text,
          putFieldInNewLine: true,
          cols: 6,
          style: 'outlined',
        } as IJumpInput,
      ],
    };

    const crud = ref<IJumpCrud>({
      jumpCrudTable: table,
      jumpForm: form,
    });

    return {
      crud,
    };
  },
});
</script>
