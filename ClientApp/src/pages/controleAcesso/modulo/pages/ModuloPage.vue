<template>
  <jump-crud :crud="crud" />
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { ColumnType } from 'src/components/tables/enums/ColumnType';
import { IJumpCrud } from 'src/components/crud/interfaces/IJumpCrud';
import { IJumpForm } from 'src/components/forms/interfaces/IJumpForm';
import { IJumpInput } from 'src/components/forms/interfaces/IJumpInput';
import { FormElementType } from 'src/components/forms/enums/FormElementType';
import { IJumpSelectServer } from 'src/components/forms/interfaces/IJumpSelectServer';

import ModuloService from '../services/ModuloService';
import JumpCrud from 'src/components/crud/JumpCrud.vue';
import IJumpTableCrud from 'src/components/tables/interfaces/IJumpTableCrud';

export default defineComponent({
  name: 'ModuloPage',
  components: {
    JumpCrud,
  },
  setup() {
    const service = new ModuloService('Modulo');

    const table: IJumpTableCrud = {
      title: 'Listagem de módulos',
      service: service,
      rows: [],
      componentName: 'ModuloPage',
      columns: [
        {
          label: 'id',
          field: 'id',
          name: 'id',
          type: ColumnType.number,
        },
        {
          label: 'Nome',
          field: 'nome',
          name: 'nome',
          type: ColumnType.text,
        },
        {
          label: 'Módulo pai',
          field: 'moduloPaiNome',
          name: 'moduloPaiNome',
          type: ColumnType.text,
        },
        {
          label: 'Ícone',
          field: 'icone',
          name: 'icone',
          type: ColumnType.text,
        },
        {
          label: 'Observação',
          field: 'observacao',
          name: 'observacao',
          type: ColumnType.text,
        },
        {
          label: 'Status',
          field: 'statusName',
          name: 'statusName',
          type: ColumnType.text,
        },
        {
          label: 'Motivo da inativação',
          field: 'reasonInactivation',
          name: 'reasonInactivation',
          type: ColumnType.text,
        },
        {
          label: 'Criado por',
          field: 'createdBy',
          name: 'createdBy',
          type: ColumnType.text,
        },
        {
          label: 'Criado em',
          field: 'createdAt',
          name: 'createdAt',
          type: ColumnType.datetime,
        },
        {
          label: 'Atualizado por',
          field: 'updatedBy',
          name: 'updatedBy',
          type: ColumnType.text,
        },
        {
          label: 'Atualizado em',
          field: 'updatedAt',
          name: 'updatedAt',
          type: ColumnType.datetime,
        },
      ],
    };

    const form: IJumpForm = {
      title: 'Cadastro de módulo',
      fields: [
        {
          name: 'id',
          type: FormElementType.number,
          hidden: true,
        } as IJumpInput,
        {
          label: 'Nome',
          name: 'nome',
          type: FormElementType.text,
          style: 'outlined',
          cols: 6,
        } as IJumpInput,
        {
          label: 'Modulo pai',
          name: 'moduloId',
          type: FormElementType.selectServer,
          style: 'outlined',
          cols: 6,
          urlGetOptions: 'ConsultaPadrao/GetModulos',
          emitValue: true,
          labelNameField: 'moduloPaiNome',
        } as IJumpSelectServer,
        {
          label: 'Ícone do menu',
          name: 'iconeMenu',
          type: FormElementType.text,
          style: 'outlined',
          cols: 6,
        } as IJumpInput,
        {
          label: 'Observação',
          name: 'observacao',
          type: FormElementType.textarea,
          style: 'outlined',
          cols: 6,
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
