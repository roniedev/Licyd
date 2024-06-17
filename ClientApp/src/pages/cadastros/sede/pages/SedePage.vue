<template>
  <jump-crud :crud="crud"></jump-crud>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { ColumnType } from 'src/components/tables/enums/ColumnType';
import { IJumpCrud } from 'src/components/crud/interfaces/IJumpCrud';
import { IJumpInput } from 'src/components/forms/interfaces/IJumpInput';
import { fieldMaxLength } from 'src/components/forms/utils/formValidations';
import { FormElementType } from 'src/components/forms/enums/FormElementType';
import { fieldIsRequired } from 'src/components/forms/utils/formValidations';
import { IJumpSelectClient } from 'src/components/forms/interfaces/IJumpSelectClient';

import SedeService from '../services/SedeService';
import JumpCrud from 'src/components/crud/JumpCrud.vue';

export default defineComponent({
  name: 'SedePage',

  components: {
    JumpCrud,
  },

  setup() {
    const service = new SedeService('Sede');

    const crud = ref<IJumpCrud>({
      jumpCrudTable: {
        title: 'Listagem de sedes',
        service: service,
        componentName: 'SedePage',
        showDeleteImport: false,
        isEditActiveRows: true,
        rows: [],
        columns: [
          {
            field: 'id',
            name: 'id',
            label: 'Código',
            type: ColumnType.number,
          },
          {
            field: 'nome',
            name: 'nome',
            label: 'Nome',
            type: ColumnType.text,
          },
          {
            field: 'unidadeFederativa',
            name: 'unidadeFederativa',
            label: 'Unidade federativa (UF)',
            type: ColumnType.text,
          },
          {
            field: 'observacao',
            name: 'observacao',
            label: 'Observação',
            type: ColumnType.text,
          },
          {
            field: 'statusName',
            name: 'statusName',
            label: 'Status',
            type: ColumnType.text,
          },
          {
            field: 'reasonInactivation',
            name: 'reasonInactivation',
            label: 'Motivo da inativação',
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
      },
      jumpForm: {
        title: 'Cadastro de sede',
        fields: [
          {
            name: 'id',
            type: FormElementType.text,
            hidden: true,
          } as IJumpInput,
          {
            label: 'Nome',
            name: 'nome',
            type: FormElementType.text,
            style: 'outlined',
            cols: 6,
            validationRules: [fieldIsRequired(), fieldMaxLength(64)],
          } as IJumpInput,
          {
            label: 'Unidade federativa (UF)',
            name: 'unidadeFederativa',
            type: FormElementType.selectClient,
            style: 'outlined',
            cols: 6,
            emitValue: true,
            validationRules: [fieldIsRequired(), fieldMaxLength(2)],
            lazyRules: false,
            options: [
              { value: 'AC', label: 'Acre' },
              { value: 'AL', label: 'Alagoas' },
              { value: 'AM', label: 'Amazonas' },
              { value: 'AP', label: 'Amapá' },
              { value: 'BA', label: 'Bahia' },
              { value: 'CE', label: 'Ceará' },
              { value: 'DF', label: 'Distrito Federal' },
              { value: 'ES', label: 'Espírito Santo' },
              { value: 'GO', label: 'Goiás' },
              { value: 'MA', label: 'Maranhão' },
              { value: 'MG', label: 'Minas Gerais' },
              { value: 'MS', label: 'Mato Grosso do Sul' },
              { value: 'MT', label: 'Mato Grosso' },
              { value: 'PA', label: 'Pará' },
              { value: 'PB', label: 'Paraíba' },
              { value: 'PE', label: 'Pernambuco' },
              { value: 'PI', label: 'Piauí' },
              { value: 'PR', label: 'Paraná' },
              { value: 'RJ', label: 'Rio de Janeiro' },
              { value: 'RN', label: 'Rio Grande do Norte' },
              { value: 'RO', label: 'Rondônia' },
              { value: 'RR', label: 'Roraima' },
              { value: 'RS', label: 'Rio Grande do Sul' },
              { value: 'SC', label: 'Santa Catarina' },
              { value: 'SE', label: 'Sergipe' },
              { value: 'SP', label: 'São Paulo' },
              { value: 'TO', label: 'Tocantins' },
            ],
          } as IJumpSelectClient,
          {
            label: 'Observação',
            name: 'observacao',
            type: FormElementType.textarea,
            style: 'outlined',
            cols: 6,
            validationRules: [fieldMaxLength(512)],
          } as IJumpInput,
        ],
      },
    });

    return {
      crud,
    };
  },
});
</script>
