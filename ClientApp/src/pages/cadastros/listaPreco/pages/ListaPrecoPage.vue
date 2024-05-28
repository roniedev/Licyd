<template>
  <jump-table-crud
    v-if="master.isTableVisible.value"
    :crud-table="master.table.value"
    @new="toggleVisibility"
    @edit="edit"
    @show="show"
  />

  <q-card v-if="master.isFormVisible.value">
    <q-card-section>
      <q-tabs
        v-model="tab"
        dense
        class="text-grey"
        active-color="primary"
        indicator-color="primary"
        align="left"
      >
        <q-tab name="listaPreco" label="Lista de Preço" />
        <q-tab
          name="listaPrecoItem"
          label="Itens"
          v-if="detail.isTabVisible.value"
        />
      </q-tabs>

      <q-tab-panels v-model="tab" animated>
        <q-tab-panel name="listaPreco" class="q-pa-none">
          <jump-form
            :form="master.form.value"
            @submit="submitListaPreco"
            @cancel="toggleVisibility"
          />
        </q-tab-panel>

        <q-tab-panel
          name="listaPrecoItem"
          class="q-pa-none"
          v-if="detail.isTabVisible.value"
        >
          <jump-form
            :form="detail.form.value"
            @cancel="toggleVisibility"
            @submit="detail.submit"
          >
          </jump-form>
        </q-tab-panel>
      </q-tab-panels>
    </q-card-section>
  </q-card>
</template>

<script lang="ts">
import ListaPrecoService from '../services/ListaPrecoService';
import JumpForm from 'src/components/forms/JumpForm.vue';
import JumpTable from 'src/components/tables/JumpTable.vue';
import ListaPrecoItemService from '../services/ListaPrecoItemService';

import JumpTableCrud from 'src/components/tables/JumpTableCrud.vue';
import IJumpTableCrud from 'src/components/tables/interfaces/IJumpTableCrud';

import { defineComponent, ref } from 'vue';
import { FormElementType } from 'src/components/forms/enums/FormElementType';
import { IJumpForm } from 'src/components/forms/interfaces/IJumpForm';
import { IJumpInput } from 'src/components/forms/interfaces/IJumpInput';
import { IJumpSelectServer } from 'src/components/forms/interfaces/IJumpSelectServer';

import { ColumnType } from 'src/components/tables/enums/ColumnType';
import { IJumpTable } from 'src/components/tables/interfaces/IJumpTable';

export default defineComponent({
  name: 'ListaPrecoPage',

  components: {
    JumpForm,
    JumpTableCrud,
  },

  setup() {
    const tab = ref<string>('listaPreco');

    const setupListaPreco = () => {
      let isTableVisible = ref<boolean>(true);
      let isFormVisible = ref<boolean>(false);

      const service = new ListaPrecoService('ListaPreco');

      const form = ref<IJumpForm>({
        title: 'Lista de Preço',
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
            label: 'Categoria',
            name: 'categoriaId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: 'ConsultaPadrao/GetCategoriaItens',
            emitValue: true,
            dense: true,
            labelNameField: 'categoriaNome',
          } as IJumpSelectServer,
          {
            name: 'dataVigenciaInicio',
            label: 'Data Vigência - Início',
            type: FormElementType.datetimeLocal,
            cols: 6,
            style: 'outlined',
          } as IJumpInput,
          {
            name: 'dataVigenciaFim',
            label: 'Data Vigência - Fim',
            type: FormElementType.date,
            cols: 6,
            style: 'outlined',
          } as IJumpInput,
        ],
      });

      const table = ref<IJumpTableCrud>({
        title: 'Lista de Preço',
        service: service,
        rows: [],
        isEditActiveRows: true,
        componentName: 'ListaPrecoPage',

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
            field: 'dataVigenciaInicio',
            name: 'dataVigenciaInicio',
            label: 'Data de Vigência - Início',
            type: ColumnType.date,
          },
          {
            field: 'dataVigenciaFim',
            name: 'dataVigenciaFim',
            label: 'Data de Vigência - Fim',
            type: ColumnType.date,
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
      });

      const submit = async (formData: any) => {
        const data = formData.id
          ? await service.update(formData)
          : await service.insert(formData);

        return data;
      };

      const getById = async (id: number): Promise<any> => {
        const data = await service.getById(id);
        return data;
      };

      const edit = async (row: any) => {
        const data = await getById(row.id);
        return data;
      };

      return {
        form,
        table,
        submit,
        getById,
        edit,
        isTableVisible,
        isFormVisible,
      };
    };

    const setupListaPrecoItem = () => {
      let isTableVisible = ref<boolean>(true);
      let isFormVisible = ref<boolean>(false);
      const isTabVisible = ref<boolean>(false);

      const service = new ListaPrecoItemService('ListaPrecoItem');

      const form = ref<IJumpForm>({
        title: 'Itens da lista de preço',
        data: {},
        fields: [
          {
            name: 'id',
            type: FormElementType.number,
            hidden: true,
          } as IJumpInput,
          {
            name: 'precoListaId',
            type: FormElementType.number,
            hidden: true,
          } as IJumpInput,
          {
            label: 'Item',
            name: 'ItemId',
            type: FormElementType.selectServer,
            style: 'outlined',
            cols: 6,
            urlGetOptions: 'ConsultaPadrao/GetItens',
            emitValue: true,
          } as IJumpSelectServer,
          {
            label: 'Valor unitário',
            name: 'valorUnitario',
            style: 'outlined',
            type: FormElementType.number,
            cols: 6,
            dense: true,
          } as IJumpInput,
        ],
      });

      const table = ref<IJumpTable>({
        title: 'Lista de Preço',
        rows: [],
        dense: true,
        columns: [
          {
            field: 'id',
            name: 'id',
            label: '#',
            type: ColumnType.number,
          },
          {
            field: 'itemNome',
            name: 'itemNome',
            label: 'Item',
            type: ColumnType.text,
          },
          {
            field: 'valorUnitario',
            name: 'valorUnitario',
            label: 'Valor Unitário',
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
      });

      const getById = async (id: number): Promise<any> => {
        const data = await service.getById(id);

        if (data) {
          form.value.data = data;
        }

        return data;
      };

      const edit = async (row: any) => {
        const data = await master.getById(row.id);
        if (data) {
          form.value.data = data;
        }
      };
      const submit = async (formData: any) => {
        const data = formData.id
          ? await service.update(formData)
          : await service.insert(formData);

        if (data) {
          form.value.data = {};
          (table.value.rows as Array<any>).push(data);
        }
      };

      return {
        isFormVisible,
        isTableVisible,
        edit,
        getById,
        table,
        form,
        isTabVisible,
        submit,
      };
    };

    const master = setupListaPreco();
    const detail = setupListaPrecoItem();

    const resetForm = () => {
      master.form.value.data = {};
      detail.form.value.data = {};
      detail.table.value.rows = [];
      detail.isTabVisible.value = false;
    };

    const toggleVisibility = () => {
      master.isTableVisible.value = !master.isTableVisible.value;
      master.isFormVisible.value = !master.isFormVisible.value;
      if (!detail.isTabVisible.value) tab.value = 'listaPreco';
      if (master.isTableVisible.value) resetForm();
    };

    const show = async (row: any) => {
      const data = await master.getById(row.id);

      if (data) {
        toggleVisibility();

        master.form.value.disabled = true;
        detail.form.value.disabled = true;
        master.form.value.showSubmitButton = false;
        detail.form.value.showSubmitButton = false;
        detail.table.value.actions?.forEach((action) => {
          action.visible = false;
        });

        detail.isTabVisible.value = true;
      }
    };

    const submitListaPreco = async (formData: any) => {
      const data = await master.submit(formData);

      if (data) {
        detail.form.value.data = detail.form.value.data
          ? detail.form.value.data
          : {};

        detail.form.value.data['ListaPrecoId'] = data.id;
        detail.isTabVisible.value = true;
        if (formData.id) tab.value = 'listaPrecoItem';
      }
    };

    const getById = async (id: number): Promise<any> => {
      const data = await master.getById(id);

      if (data) {
        master.form.value.data = data;
        detail.table.value.rows = data.acoes;
        detail.form.value.data['listaPrecoId'] = data.id;
      }

      return data;
    };

    const edit = async (row: any) => {
      const data = await master.getById(row.id);

      if (data) {
        master.form.value.data = data;
        detail.table.value.rows = data.Itens;
        detail.form.value.data['precoListaId'] = data.id;
      }

      if (data) {
        tab.value = 'listaPreco';
        detail.isTabVisible.value = !detail.isTabVisible.value;

        toggleVisibility();

        detail.table.value.actions?.forEach((action) => {
          action.visible = true;
        });
      }
    };

    return {
      tab,
      master,
      detail,
      getById,
      show,
      submitListaPreco,
      toggleVisibility,
      edit,
    };
  },
});
</script>
