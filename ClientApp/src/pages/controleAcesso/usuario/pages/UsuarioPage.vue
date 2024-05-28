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

import UsuarioService from '../services/UsuarioService';
import JumpCrud from 'src/components/crud/JumpCrud.vue';
import IJumpTableCrud from 'src/components/tables/interfaces/IJumpTableCrud';
import { IJumpToggle } from 'src/components/forms/interfaces/IJumpToggle';

export default defineComponent({
  name: 'UsuarioPage',
  components: {
    JumpCrud,
  },
  setup() {
    const service = new UsuarioService('Usuario');

    const table: IJumpTableCrud = {
      title: 'Listagem de usuários',
      service: service,
      componentName: 'UsuarioPage',
      isEditActiveRows: true,
      rows: [],
      columns: [
        {
          label: '#',
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
          label: 'Nome de usuário',
          field: 'userName',
          name: 'userName',
          type: ColumnType.text,
        },
        {
          label: 'E-mail',
          field: 'email',
          name: 'email',
          type: ColumnType.text,
        },
        {
          label: 'Hóspede?',
          field: 'isHospede',
          name: 'isHospede',
          type: ColumnType.boolean,
        },
        {
          label: 'Data de entrada',
          field: 'dataEntrada',
          name: 'dataEntrada',
          type: ColumnType.date,
        },
        {
          label: 'Data de saída',
          field: 'dataSaida',
          name: 'dataSaida',
          type: ColumnType.date,
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
      title: 'Cadastro de usuário',
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
          label: 'Nome de usuário',
          name: 'userName',
          type: FormElementType.text,
          style: 'outlined',
          cols: 6,
        } as IJumpInput,
        {
          label: 'E-mail',
          name: 'email',
          type: FormElementType.email,
          style: 'outlined',
          cols: 6,
        } as IJumpInput,
        {
          label: 'Hóspede?',
          name: 'isHospede',
          type: FormElementType.toggle,
          style: 'outlined',
          cols: 6,
        } as IJumpToggle,
        {
          label: 'Data de entrada',
          name: 'dataEntrada',
          type: FormElementType.date,
          style: 'outlined',
          cols: 6,
        } as IJumpInput,
        {
          label: 'Data de saída',
          name: 'dataSaida',
          type: FormElementType.date,
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
