<template>
  <q-page>
    <jump-table-crud
      v-if="tableMasterVisible"
      :crud-table="tableMaster"
      @new="toggleVisibility"
      @edit="handleEdit"
      @show="handleShow"
    />
    <jump-form
      v-else
      :form="formMaster"
      @cancel="toggleVisibility"
      @submit="handleSubmit"
    >
      <template v-slot:after-form>
        <jump-table-crud
          ref="tableDetailRef"
          :crud-table="tableDetail"
          @new="toggleVisibilityFormDetail"
          @edit="handleEditDetail"
          @show="handleShowDetail"
        />
      </template>
    </jump-form>

    <q-dialog
      v-model="formDetailVisible"
      persistent
      transition-show="slide-up"
      transition-hide="slide-down"
    >
      <q-card style="width: 700px">
        <q-card-section class="q-pt-none">
          <jump-form
            :form="formDetail"
            @cancel="toggleVisibilityFormDetail"
            @submit="handleSubmitDetail"
          />
        </q-card-section>
      </q-card>
    </q-dialog>
  </q-page>
</template>

<script lang="ts">
import { ref, computed, defineComponent } from 'vue';

import JumpForm from '../forms/JumpForm.vue';
import JumpTableCrud from '../tables/JumpTableCrud.vue';
import ICadastroMestreDetalhe from './interfaces/ICadastroMestreDetalhe';
import { ITableFilter } from '../tables/interfaces/ITableFilter';

export default defineComponent({
  name: 'CadastroMestreDetalhe',

  props: {
    cadastro: {
      type: Object as () => ICadastroMestreDetalhe,
      required: true,
    },
  },

  components: {
    JumpForm,
    JumpTableCrud,
  },

  setup(props) {
    const tableDetailRef = ref<any>(null);

    const tableMasterVisible = ref<boolean>(true);
    const formDetailVisible = ref<boolean>(false);

    const parentKey = props.cadastro.parentKey;
    const service = props.cadastro.tableMaster.service;
    const serviceDetail = props.cadastro.tableDetail.service;

    let enabledForms = false;

    const tableMaster = computed(() => {
      return props.cadastro.tableMaster;
    });

    const formMaster = computed(() => {
      return props.cadastro.formMaster;
    });

    const tableDetail = computed(() => {
      if (props.cadastro.formMaster.data) {
        const parentKey = props.cadastro.parentKey;
        const parentValue = props.cadastro.formMaster.data['id']?.toString();

        const filters: ITableFilter[] = [
          {
            name: parentKey,
            value: parentValue,
          },
        ];

        // eslint-disable-next-line vue/no-mutating-props
        props.cadastro.tableDetail.filters = filters;
      } else {
        // eslint-disable-next-line vue/no-mutating-props
        props.cadastro.tableDetail.loadDataOnStart = false;
      }

      // eslint-disable-next-line vue/no-mutating-props
      props.cadastro.tableDetail.showAddButton =
        props.cadastro.formMaster.data != undefined && enabledForms;

      return props.cadastro.tableDetail;
    });

    const formDetail = computed(() => {
      return props.cadastro.formDetail;
    });

    const toggleVisibility = () => {
      tableMasterVisible.value = !tableMasterVisible.value;
    };

    const toggleVisibilityFormDetail = () => {
      formDetailVisible.value = !formDetailVisible.value;
    };

    const handleSubmit = async (formData: any) => {
      const data = formData.id
        ? await service.update(formData)
        : await service.insert(formData);

      if (data) {
        // eslint-disable-next-line vue/no-mutating-props
        props.cadastro.formMaster.data = data;
        updateTableDetailFilters();
        tableDetailRef.value.refreshRows();
      }
    };

    const getById = async (id: number): Promise<any> => {
      const data = await service.getById(id);

      if (data) {
        // eslint-disable-next-line vue/no-mutating-props
        props.cadastro.formMaster.data = data;

        return data;
      }
    };

    const handleEdit = async (row: any) => {
      const data = await getById(row.id);

      if (data) {
        // eslint-disable-next-line vue/no-mutating-props
        props.cadastro.formMaster.disabled = false;
        enabledForms = true;
        updateTableDetailFilters();
        toggleVisibility();
        tableDetailRef.value.refreshRows();
      }
    };

    const handleShow = async (row: any) => {
      const data = await getById(row.id);

      if (data) {
        // eslint-disable-next-line vue/no-mutating-props
        props.cadastro.formMaster.disabled = true;

        // eslint-disable-next-line vue/no-mutating-props
        props.cadastro.formMaster.showSubmitButton = false;

        enabledForms = false;

        toggleVisibility();
      }
    };

    const handleSubmitDetail = async (formData: any) => {
      formData[parentKey] = props.cadastro.formMaster.data['id'];

      const data = formData.id
        ? await serviceDetail.update(formData)
        : await serviceDetail.insert(formData);

      if (data) {
        tableDetailRef.value.refreshRows();
        toggleVisibilityFormDetail();
      }
    };

    const getByIdDetail = async (id: number): Promise<any> => {
      const data = await serviceDetail.getById(id);

      if (data) {
        formDetail.value.data = data;
        tableDetail.value.rows = data[parentKey];
      }

      return data;
    };

    const handleEditDetail = async (row: any) => {
      const data = await getByIdDetail(row.id);
      if (data) {
        toggleVisibilityFormDetail();
        formDetail.value.data = data;

        // eslint-disable-next-line vue/no-mutating-props
        props.cadastro.tableDetail.showAddButton = true;
      }
    };

    const handleShowDetail = async (row: any) => {
      const data = await getByIdDetail(row.id);
      if (data) {
        toggleVisibilityFormDetail();
        formDetail.value.disabled = true;
      }
    };

    const updateTableDetailFilters = () => {
      const parentValue = props.cadastro.formMaster.data['id'].toString();

      const filters: ITableFilter[] = [
        {
          name: parentKey,
          value: parentValue,
        },
      ];

      // eslint-disable-next-line vue/no-mutating-props
      props.cadastro.tableDetail.filters = filters;
    };

    return {
      tableMaster,
      formMaster,
      tableDetail,
      formDetail,
      tableDetailRef,
      formDetailVisible,
      tableMasterVisible,
      toggleVisibility,
      handleSubmit,
      handleEdit,
      handleShow,
      toggleVisibilityFormDetail,
      handleSubmitDetail,
      handleEditDetail,
      handleShowDetail,
    };
  },
});
</script>
