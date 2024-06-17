<template>
  <jump-table-crud
    v-if="isJumpCrudTableVisible"
    :crud-table="crud.jumpCrudTable"
    :refresh="refresh"
    @new="toggleVisibility"
    @edit="edit"
    @show="show"
  />
  <jump-form
    v-if="isJumpFormVisible"
    :form="crud.jumpForm"
    @submit="handleSubmit"
    @cancel="toggleVisibility"
  />
</template>

<script lang="ts">
import { ref } from 'vue';
import { defineComponent } from 'vue';
import { IJumpCrud } from './interfaces/IJumpCrud';

import JumpForm from '../forms/JumpForm.vue';
import JumpTableCrud from '../tables/JumpTableCrud.vue';

export default defineComponent({
  name: 'JumpCrud',

  components: {
    JumpForm,
    JumpTableCrud,
  },

  props: {
    crud: {
      type: Object as () => IJumpCrud,
      required: true,
    },
  },

  setup(props) {
    const refresh = ref<number>(0);
    const isJumpFormVisible = ref<boolean>(false);
    const isJumpCrudTableVisible = ref<boolean>(true);

    const handleSubmit = async (formValues: any) => {
      let result: boolean;

      if (formValues.id) {
        result = await props.crud.jumpCrudTable.service.update(formValues);
      } else {
        result = await props.crud.jumpCrudTable.service.insert(formValues);
      }

      if (result) {
        refreshData();
        toggleVisibility();
      }
    };

    const toggleVisibility = () => {
      isJumpCrudTableVisible.value = !isJumpCrudTableVisible.value;
      isJumpFormVisible.value = !isJumpFormVisible.value;

      if (isJumpCrudTableVisible.value) {
        // eslint-disable-next-line vue/no-mutating-props
        props.crud.jumpForm.data = undefined;
        props.crud.jumpForm.fields.forEach(
          (field) => (field.value = undefined)
        );
      }
    };

    const refreshData = () => {
      refresh.value++;
    };

    const edit = async (row: any) => {
      const data = await props.crud.jumpCrudTable.service.getById(row.id);

      // eslint-disable-next-line vue/no-mutating-props
      if (data) props.crud.jumpForm.data = data;

      // if (data) {
      //   props.crud.jumpForm.fields.forEach((field) => {
      //     field.value = data[field.name];
      //   });
      // }

      toggleVisibility();
    };

    const show = async (row: any) => {
      const data = await props.crud.jumpCrudTable.service.getById(row.id);

      if (data) {
        props.crud.jumpForm.fields.forEach((field) => {
          field.value = data[field.name];
        });
      }

      // eslint-disable-next-line vue/no-mutating-props
      props.crud.jumpForm.disabled = true;

      // eslint-disable-next-line vue/no-mutating-props
      props.crud.jumpForm.showSubmitButton = false;

      toggleVisibility();
    };

    return {
      isJumpFormVisible,
      isJumpCrudTableVisible,
      refresh,
      edit,
      show,
      refreshData,
      handleSubmit,
      toggleVisibility,
    };
  },
});
</script>
