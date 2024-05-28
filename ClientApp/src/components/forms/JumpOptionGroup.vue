<template>
  <div
    :class="
      input.borderless ? 'jump-option-group' : 'jump-option-group--border'
    "
  >
    <span v-if="input.label" class="label">{{ input.label }}</span>
    <q-option-group
      v-model="modelValue"
      v-show="input.visible === false ? false : true"
      :name="input.name"
      :type="inputType"
      :options="input.options"
      :inline="input.inline === false ? false : true"
      :dense="input.dense"
      :rules="input.validationRules || []"
      :size="input.size || 'xs'"
      :hide-bottom-space="input.hideBottomSpace === false ? false : true"
      :left-label="input.leftLabel"
      @update:model-value="handleClick"
    />
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, ref, watch } from 'vue';
import { IJumpOptionGroup } from './interfaces/IJumpOptionGroup';
import { FormElementType } from './enums/FormElementType';

export default defineComponent({
  name: 'JumpOptionGroup',

  props: {
    input: {
      type: Object as () => IJumpOptionGroup,
      required: true,
    },
    value: {
      required: false,
    },
  },

  emits: ['update:modelValue'],

  setup(props, { emit }) {
    const modelValue = ref(
      props.input.type === FormElementType.radio
        ? ref<any>
        : ref<Array<string>>([])
    );

    function handleClick(value: any) {
      modelValue.value = value;
      emit('update:modelValue', modelValue.value, props.input.name);
    }

    watch(
      () => props.input.value,
      (value) => {
        modelValue.value = value;
      },
      { immediate: true }
    );

    const inputType = computed(() => {
      const inputType = props.input.type ?? FormElementType.text;

      switch (inputType) {
        case FormElementType.radio:
          return 'radio';
        case FormElementType.checkbox:
          return 'checkbox';
        case FormElementType.toggle:
          return 'toggle';
        default:
          return 'radio';
      }
    });

    return {
      inputType,
      modelValue,
      handleClick,
    };
  },
});
</script>

<style lang="sass">
.jump-option-group
  border-radius: 4px
  margin-top: 10px

  .q-radio
    padding: 5px

  .label
    background-color: white
    padding: 0 5px
    color: rgba(0, 0, 0, 0.60)
    font-size: 14px

.jump-option-group--border
  position: relative
  border: 1px solid rgba(0, 0, 0, 0.24)
  padding: 5px
  border-radius: 4px
  margin-top: 10px

  .q-radio
    padding: 5px

  .label
    position: absolute
    top: -10px
    background-color: white
    padding: 0 5px
    color: rgba(0, 0, 0, 0.60)
    font-size: 14px
</style>
