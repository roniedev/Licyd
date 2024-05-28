<template>
  <input
    type="hidden"
    :name="input.name"
    v-model="localValue"
    v-if="input.hidden"
  />
  <q-input
    v-else
    v-model="localValue"
    :label="input.label"
    :type="inputType"
    :name="input.name"
    :dense="input.dense || false"
    :stack-label="input.stackLabel || false"
    :square="input.square || false"
    :rounded="input.rounded || false"
    :borderless="input.borderless || false"
    :rules="input.validationRules || []"
    :disable="input.disabled || false"
    :readonly="input.readonly || false"
    :outlined="input.style === 'outlined'"
    :standout="input.style === 'standout'"
    :filled="input.style === 'filled'"
    :mask="input.mask"
    :unmasked-value="input.unmaskedValue"
    :reverse-fill-mask="input.reverseFillMask"
    :fill-mask="input.fillMask"
    :hint="input.hint"
    :lazy-rules="input.lazyRules ?? true"
    :hide-bottom-space="input.hideBottomSpace ?? true"
    @update:model-value="handleUpdate"
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
  </q-input>
</template>

<script lang="ts">
import { computed, defineComponent, onBeforeMount, ref } from 'vue';
import { IJumpInput } from './interfaces/IJumpInput';
import { FormElementType } from './enums/FormElementType';

export default defineComponent({
  name: 'JumpInput',

  props: {
    input: {
      type: Object as () => IJumpInput,
      required: true,
    },
    value: {
      required: false,
    },
  },

  emits: ['update:modelValue'],

  setup(props, { emit }) {
    const localValue = ref<any>();

    const onFocus = () => {
      if (props.input.onFocus) props.input.onFocus(localValue.value);
    };

    const onBlur = () => {
      if (props.input.onBlur) props.input.onBlur(localValue.value);
    };

    const handleUpdate = (value: any) => {
      localValue.value = value;
      emit('update:modelValue', localValue.value);
    };

    onBeforeMount(() => {
      localValue.value = props.input.value;
    });

    const inputType = computed(() => {
      const inputType = props.input.type ?? FormElementType.text;

      switch (inputType) {
        case FormElementType.text:
          return 'text';
        case FormElementType.number:
          return 'number';
        case FormElementType.textarea:
          return 'textarea';
        case FormElementType.time:
          return 'time';
        case FormElementType.password:
          return 'password';
        case FormElementType.email:
          return 'email';
        case FormElementType.search:
          return 'search';
        case FormElementType.tel:
          return 'tel';
        case FormElementType.url:
          return 'url';
        case FormElementType.date:
          return 'date';
        case FormElementType.datetimeLocal:
          return 'datetime-local';
        case FormElementType.file:
          return 'file';
        default:
          return 'text';
      }
    });

    return {
      inputType,
      localValue,
      handleUpdate,
      onFocus,
      onBlur,
    };
  },
});
</script>
