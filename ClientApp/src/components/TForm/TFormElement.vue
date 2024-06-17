<template>
  <fieldset class="fieldset" v-if="element.type === 'radio'">
    <legend>{{ element.label }}</legend>
    <q-option-group
      :options="element.options"
      type="radio"
      :inline="element.radioInline"
      :dense="element.dense"
      :rules="element.required ? element.requiredRules : []"
      v-model="selectedValue"
      size="xs"
    />
  </fieldset>

  <q-select
    class="input"
    v-if="element.type === 'select'"
    :name="element.name"
    :label="element.label"
    :options="options"
    :outlined="element.design === 'outlined'"
    :filled="element.design === 'filled'"
    :dense="element.dense"
    :rules="element.validationRules"
    option-value="value"
    option-label="label"
    use-input
    map-options
    emit-value
    v-model="selectedValue"
    @filter="(inputValue: string, doneFn: any, abortFn: any) =>
      handleFilter(inputValue, doneFn, abortFn, element.urlRequestOption)"
  >
    <template v-slot:prepend v-if="element.icon">
      <q-icon :name="element.icon" />
    </template>
  </q-select>

  <component
    v-if="
      element.type != 'select' &&
      element.type !== 'multiselect' &&
      element.type != 'radio'
    "
    class="input"
    :outlined="element.design === 'outlined'"
    :filled="element.design === 'filled'"
    :dense="element.dense"
    :is="elementToComponent[element.type]"
    :type="element.type"
    :name="element.name"
    :label="element.label"
    :rules="element.validationRules"
    v-model="selectedValue"
    :step="element.step"
  >
    <template v-slot:prepend v-if="element.icon">
      <q-icon :name="element.icon" />
    </template>
  </component>
</template>

<script lang="ts">
import {
  defineComponent,
  isProxy,
  onBeforeMount,
  ref,
  toRaw,
  watch,
} from 'vue';
import { hideLoading, showLoading } from 'src/helpers/Loading';
import { IFormElement, IOption } from 'src/interfaces/components/IForm';
import { notificar } from 'src/helpers/Notificacao';
import { ElementToComponentMap } from 'src/types/ElementToComponentMap';

import AxiosServiceOld from 'src/services/AxiosServiceOld';

export default defineComponent({
  name: 'TFormElement',

  props: {
    element: {
      type: Object as () => IFormElement,
      required: true,
    },
    modelValue: {} as any,
  },

  computed: {
    elementToComponent() {
      return {
        text: 'q-input',
        password: 'q-input',
        date: 'q-input',
        number: 'q-input',
        textarea: 'q-input',
        toggle: 'q-toggle',
        checkbox: 'q-checkbox',
        file: 'q-file',
      } as ElementToComponentMap;
    },
  },

  setup(props, { emit }) {
    const options = ref([] as Array<IOption>);
    const selectedValue = ref<any>();

    watch(
      selectedValue,
      () => {
        emit('update:modelValue', selectedValue.value);
      },
      { immediate: true }
    );

    watch(
      () => props.modelValue,
      (value) => {
        options.value =
          options.value.length > 0
            ? options.value
            : props.element.options
            ? props.element.options
            : [];

        if (props.element.type === 'select') {
          // Quebra a reatividade do componente para
          // quando setar o valor ele não entrar em loop ou limpar o campo
          if (isProxy(value)) {
            const option = toRaw<IOption>(value);
            value = option.value;
          }

          selectedValue.value = options.value.find((item) => {
            return item.value === value;
          });

          return;
        }

        selectedValue.value =
          props.element.type === 'toggle' && !value ? false : value;
      }
    );

    onBeforeMount(() => {
      options.value = props.element.options || [];
    });

    async function fetchData(url: string, filtro: string) {
      url += `/${filtro}`;
      const response = await AxiosServiceOld.get(url);

      if (response.sucesso) return response.objeto;

      notificar(response.mensagem, 'negative', false, 0);
    }

    function handleFilter(val: string, update: any, abort: any, apiUrl: any) {
      options.value = [];

      update(async () => {
        showLoading();
        const filtro = val.toLowerCase();
        const data = await fetchData(apiUrl, filtro);

        options.value = data;
        hideLoading();
      });
    }

    return {
      options,
      selectedValue,
      handleFilter,
    };
  },
});
</script>

<style lang="sass" scoped>
.fieldset
  border: 1px solid rgb(0, 0, 0, 0.20)
  margin-bottom: 10px
  margin-right: 15px
  font-size: 12.5px
  padding: 10px

.input
  font-size: 12.5px
  margin: 5px

.q-icon
  font-size: 15px !important
</style>
