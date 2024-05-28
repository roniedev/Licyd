<template>
  <q-toggle
    v-model="modelValue"
    v-show="input.visible === false ? false : true"
    :class="input.dense ? 'q-toggle-dense' : '.q-toggle'"
    :label="input.label"
    :name="input.name"
    :dense="input.dense || false"
    :rules="input.validationRules || []"
    :size="input.size || 'xs'"
    :hide-bottom-space="input.hideBottomSpace === false ? false : true"
    :left-label="input.leftLabel"
    :disable="input.disabled"
    @update:model-value="handleClick"
  />
</template>

<script lang="ts">
import { defineComponent, ref, watch } from 'vue';
import { IJumpToggle } from './interfaces/IJumpToggle';

export default defineComponent({
  name: 'JumpToggle',
  props: {
    input: {
      type: Object as () => IJumpToggle,
      required: true,
    },
    value: {
      required: false,
    },
  },
  setup(props, { emit }) {
    const modelValue = ref<any>();

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

    return {
      modelValue,
      handleClick,
    };
  },
});
</script>

<style scoped>
.q-toggle {
  margin-top: 17.5px;
}

.q-toggle-dense {
  margin-top: 12.5px;
}
</style>
