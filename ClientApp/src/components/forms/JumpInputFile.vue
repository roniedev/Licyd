<template>
  <div>
    <q-file
      v-model="modelValue"
      v-show="input.visible === false ? input.visible : true"
      :label="input.label"
      :name="input.name"
      :dense="input.dense || false"
      :stack-label="input.stackLabel || false"
      :outlined="input.style === 'outlined'"
      :standout="input.style === 'standout'"
      :filled="input.style === 'filled'"
      :square="input.square || false"
      :rounded="input.rounded || false"
      :borderless="input.borderless || false"
      :rules="input.validationRules || []"
      :disable="input.disabled || false"
      :readonly="input.readonly || false"
      :lazy-rules="input.lazyRules === false ? input.lazyRules : true"
      :hide-bottom-space="
        input.hideBottomSpace === false ? input.hideBottomSpace : true
      "
      :use-chips="input.useChips === false ? input.useChips : true"
      @update:model-value="handleFile"
      @blur="onBlur"
      @focus="onFocus"
    >
      <template v-slot:before v-if="input.beforeIcon">
        <q-icon :name="input.beforeIcon" />
      </template>
      <template v-slot:append v-if="input.appendIcon">
        <q-icon :name="input.appendIcon" />
      </template>
      <template v-slot:prepend v-if="input.prependIcon">
        <q-icon :name="input.prependIcon" />
      </template>
    </q-file>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { IJumpInputFile } from './interfaces/IJumpInputFile';

export default defineComponent({
  name: 'JumpInputFile',

  props: {
    input: {
      type: Object as () => IJumpInputFile,
      required: true,
    },
    value: {
      required: false,
    },
  },

  emits: ['update:modelValue'],

  setup(props, { emit }) {
    const modelValue = ref<any>();

    const handleFile = (value: any) => {
      modelValue.value = value;
      emit('update:modelValue', modelValue.value, props.input.name);
    };

    const onFocus = () => {
      if (props.input.onFocus) props.input.onFocus(modelValue.value);
    };

    const onBlur = () => {
      if (props.input.onBlur) props.input.onBlur(modelValue.value);
    };

    return {
      modelValue,
      handleFile,
      onFocus,
      onBlur,
    };
  },
});
</script>
