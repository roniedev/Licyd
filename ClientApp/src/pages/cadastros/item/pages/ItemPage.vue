<template>
  <jump-crud :crud="crud" />
</template>

<script lang="ts">
import JumpCrud from 'src/components/crud/JumpCrud.vue';
import ItemService from '../services/ItemService';
import IJumpTableCrud from 'src/components/tables/interfaces/IJumpTableCrud';

import { defineComponent, ref } from 'vue';
import { ColumnType } from 'src/components/tables/enums/ColumnType';
import { IJumpForm } from 'src/components/forms/interfaces/IJumpForm';
import { IJumpInput } from 'src/components/forms/interfaces/IJumpInput';
import { FormElementType } from 'src/components/forms/enums/FormElementType';
import { IJumpCrud } from 'src/components/crud/interfaces/IJumpCrud';
import { IJumpSelectServer } from 'src/components/forms/interfaces/IJumpSelectServer';
import { IJumpToggle } from 'src/components/forms/interfaces/IJumpToggle';

export default defineComponent({
  name: 'ItemPage',

  components: {
    JumpCrud,
  },

  setup() {
    const service = new ItemService('Item');

    const table: IJumpTableCrud = {
      title: 'Listagem de Itens',
      service: service,
      rows: [],
      componentName: 'ItemPage',
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
          field: 'isitemadicional',
          name: 'isitemadicional',
          label: 'item adicional?',
          type: ColumnType.boolean,
        },
        {
          label: 'Categoria',
          field: 'categoriaNome',
          name: 'categoriaNome',
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
      title: 'Cadastro Itens',
      fields: [
        {
          name: 'id',
          type: FormElementType.number,
          hidden: true,
        } as IJumpInput,
        {
          name: 'isItemAdicional',
          label: 'Item Adicional?',
          type: FormElementType.toggle,
          cols: 4,
          value: false,
          dense: true,
          putFieldInNewLine: true,
        } as IJumpToggle,
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
        {
          label: 'Categoria',
          name: 'categoriaId',
          type: FormElementType.selectServer,
          style: 'outlined',
          cols: 6,
          urlGetOptions: 'ConsultaPadrao/GetCategoriaItens',
          emitValue: true,
        } as IJumpSelectServer,
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
