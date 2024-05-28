<template>
  <div>
    <jump-input
      v-if="isJumpInput(field)"
      v-model="modelValue"
      class="q-ma-xs"
      :input="field"
      @update:model-value="handleUpdate"
    />
    <jump-select-client
      v-else-if="isJumpSelectClient(field)"
      class="q-ma-xs"
      v-model="modelValue"
      :select="field"
      @update:model-value="handleUpdate"
    />
    <jump-select-server
      v-else-if="isJumpSelectServer(field)"
      class="q-ma-xs"
      v-model="modelValue"
      :select="field"
      @update:model-value="handleUpdate"
    />
    <jump-input-file
      v-else-if="isJumpInputFile(field)"
      class="q-ma-xs"
      v-model="modelValue"
      :input="field"
      @update:model-value="handleUpdate"
    />
    <jump-option-group
      v-else-if="isJumpOptionGroup(field)"
      class="q-ma-xs"
      v-model="modelValue"
      :input="field"
      @update:model-value="handleUpdate"
    />
    <jump-toggle
      v-else-if="isJumpToggle(field)"
      class="q-ma-xs"
      v-model="modelValue"
      :input="field"
      @update:model-value="handleUpdate"
    />
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from 'vue';
import JumpInput from './JumpInput.vue';
import JumpToggle from './JumpToggle.vue';
import JumpSelectClient from './JumpSelectClient.vue';
import JumpSelectServer from './JumpSelectServer.vue';
import JumpInputFile from './JumpInputFile.vue';
import JumpOptionGroup from './JumpOptionGroup.vue';
import { IJumpInput } from './interfaces/IJumpInput';
import { IJumpSelectClient } from './interfaces/IJumpSelectClient';
import { IJumpSelectServer } from './interfaces/IJumpSelectServer';
import { IJumpOptionGroup } from './interfaces/IJumpOptionGroup';
import { IJumpInputFile } from './interfaces/IJumpInputFile';
import { IJumpToggle } from './interfaces/IJumpToggle';
import { FormElementType } from './enums/FormElementType';

export default defineComponent({
  name: 'JumpFormField',
  props: {
    field: {
      type: Object as () =>
        | IJumpInput
        | IJumpSelectClient
        | IJumpSelectServer
        | IJumpOptionGroup,
      required: true,
    },
    value: {
      required: false,
    },
  },
  components: {
    JumpInput,
    JumpToggle,
    JumpInputFile,
    JumpSelectClient,
    JumpSelectServer,
    JumpOptionGroup,
  },

  emits: ['update:modelValue'],

  setup(props, { emit }) {
    const modelValue = ref<string | number | boolean | null | undefined>();

    const isJumpInput = (
      field:
        | IJumpInput
        | IJumpInputFile
        | IJumpSelectClient
        | IJumpSelectServer
        | IJumpOptionGroup
        | IJumpToggle
    ): field is IJumpInput => {
      return (
        field.type !== FormElementType.selectServer &&
        field.type !== FormElementType.selectClient &&
        field.type !== FormElementType.file &&
        field.type !== FormElementType.checkbox &&
        field.type !== FormElementType.radio &&
        field.type !== FormElementType.toggle
      );
    };

    const isJumpInputFile = (
      field:
        | IJumpInput
        | IJumpInputFile
        | IJumpSelectClient
        | IJumpSelectServer
        | IJumpOptionGroup
        | IJumpToggle
    ): field is IJumpInput => {
      return field.type === FormElementType.file;
    };

    const isJumpSelectClient = (
      field:
        | IJumpInput
        | IJumpInputFile
        | IJumpSelectClient
        | IJumpSelectServer
        | IJumpOptionGroup
        | IJumpToggle
    ): field is IJumpSelectClient => {
      return field.type === FormElementType.selectClient;
    };

    const isJumpSelectServer = (
      field:
        | IJumpInput
        | IJumpInputFile
        | IJumpSelectClient
        | IJumpSelectServer
        | IJumpOptionGroup
        | IJumpToggle
    ): field is IJumpSelectServer => {
      return field.type === FormElementType.selectServer;
    };

    const isJumpOptionGroup = (
      field:
        | IJumpInput
        | IJumpInputFile
        | IJumpSelectClient
        | IJumpSelectServer
        | IJumpOptionGroup
        | IJumpToggle
    ): field is IJumpOptionGroup => {
      return (
        field.type === FormElementType.checkbox ||
        field.type === FormElementType.radio
      );
    };

    const isJumpToggle = (
      field:
        | IJumpInput
        | IJumpInputFile
        | IJumpSelectClient
        | IJumpSelectServer
        | IJumpOptionGroup
        | IJumpToggle
    ): field is IJumpOptionGroup => {
      return field.type === FormElementType.toggle;
    };

    const handleUpdate = (value: any) => {
      modelValue.value = value;
      emit('update:modelValue', modelValue.value);
    };

    watch(
      () => props.field.value,
      (value) => {
        modelValue.value = value;
      },
      { immediate: true }
    );

    return {
      modelValue,
      isJumpInput,
      isJumpToggle,
      isJumpInputFile,
      isJumpSelectClient,
      isJumpSelectServer,
      isJumpOptionGroup,
      handleUpdate,
    };
  },
});
</script>
