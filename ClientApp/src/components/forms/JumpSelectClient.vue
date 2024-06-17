<template>
  <div>
    <q-select
      v-show="isVisible"
      :name="select.name"
      :label="select.label"
      :options="select.options"
      :dense="select.dense || false"
      :stack-label="select.stackLabel || false"
      :square="select.square || false"
      :rounded="select.rounded || false"
      :borderless="select.borderless || false"
      :rules="select.validationRules || []"
      :outlined="select.style === 'outlined'"
      :standout="select.style === 'standout'"
      :filled="select.style === 'filled'"
      :multiple="select.multiselect"
      :option-value="select.optionValue || 'value'"
      :option-label="select.optionLabel || 'label'"
      :hide-bottom-space="select.hideBottomSpace ?? true"
      :clearable="select.clearable"
      :use-input="select.useInput ?? true"
      :map-options="select.mapOptions ?? true"
      :use-chips="select.useChips"
      :emit-value="select.emitValue"
      :disable="select.disabled || false"
      :readonly="select.readonly || false"
      @update:model-value="handleChange"
      v-model="modelValue"
    >
      <template v-slot:before v-if="select.beforeIcon">
        <q-icon :name="select.beforeIcon" />
      </template>
      <template v-slot:append v-if="select.appendIcon">
        <q-icon :name="select.appendIcon" />
      </template>
      <template v-slot:prepend v-if="select.prependIcon">
        <q-icon :name="select.prependIcon" />
      </template>
      <template v-slot:no-option>
        <q-item>
          <q-item-section class="text-grey"> Sem resultados </q-item-section>
        </q-item>
      </template>
    </q-select>
  </div>
</template>

<script lang="ts">
import { computed, defineComponent, onMounted, ref } from 'vue';
import { IJumpSelectClient } from './interfaces/IJumpSelectClient';

export default defineComponent({
  name: 'SelectClient',

  props: {
    select: {
      type: Object as () => IJumpSelectClient,
      required: true,
    },
    value: {
      required: false,
    },
  },

  emits: ['update:modelValue'],

  setup(props, { emit }) {
    const modelValue = ref<any>();

    const isVisible = computed(() => {
      return props.select.visible === undefined || props.select.visible === null
        ? true
        : props.select.visible;
    });

    function handleChange(value: any) {
      modelValue.value = value;
      emit('update:modelValue', modelValue.value);
      if (props.select.onChange) props.select.onChange(modelValue.value);
    }

    onMounted(() => (modelValue.value = props.select.value));

    return {
      modelValue,
      isVisible,
      handleChange,
    };
  },
});
</script>
