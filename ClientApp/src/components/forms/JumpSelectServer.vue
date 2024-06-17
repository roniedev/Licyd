<template>
  <q-select
    v-show="isVisible"
    v-model="modelValue"
    :name="select.name"
    :label="select.label"
    :options="options"
    :dense="select.dense"
    :stack-label="select.stackLabel"
    :square="select.square"
    :rounded="select.rounded"
    :borderless="select.borderless"
    :rules="select.validationRules || []"
    :outlined="select.style === 'outlined'"
    :standout="select.style === 'standout'"
    :filled="select.style === 'filled'"
    :multiple="select.multiselect"
    :option-value="select.optionValue || 'value'"
    :option-label="select.optionLabel || 'label'"
    :hide-bottom-space="select.hideBottomSpace ?? true"
    :clearable="select.clearable ?? true"
    :use-input="select.useInput ?? true"
    :map-options="select.mapOptions ?? true"
    :use-chips="select.useChips ?? true"
    :emit-value="select.emitValue ?? true"
    :disable="select.disabled || false"
    :readonly="select.readonly || false"
    :behavior="select.behavior || 'menu'"
    @update:model-value="handleChange"
    @filter="(inputValue: string, doneFn: any, abortFn: any) =>
      filtrarFn(inputValue, doneFn, abortFn, select.urlGetOptions)"
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
    <template
      v-if="select.multiselect"
      v-slot:option="{ itemProps, opt, selected, toggleOption }"
    >
      <q-item v-bind="itemProps">
        <q-item-section side>
          <q-toggle
            :model-value="selected"
            @update:model-value="toggleOption(opt)"
          />
        </q-item-section>
        <q-item-section>
          <q-item-label>{{ opt[select?.optionLabel ?? 'label'] }}</q-item-label>
        </q-item-section>
      </q-item>
    </template>
  </q-select>
</template>

<script lang="ts">
import { computed, defineComponent, onBeforeMount, ref } from 'vue';
import { IJumpSelectServer } from './interfaces/IJumpSelectServer';
import { getAxiosInstance } from 'src/boot/axios';

export default defineComponent({
  name: 'SelectServer',

  props: {
    select: {
      type: Object as () => IJumpSelectServer,
      required: true,
    },
    value: {
      required: false,
    },
  },

  emits: ['update:modelValue'],

  setup(props, { emit }) {
    const modelValue = ref<any>();
    const options = ref<Array<any>>([]);
    const api = getAxiosInstance();

    async function fetchData(url: string, filter: string) {
      const length = props.select.searchOnCharLength || 0;

      if (filter?.length >= length) {
        url = getUrlRequest(url, filter);

        const response = await api.get(url);
        return response.data;

        // const params = new URLSearchParams();

        // if (filter) {
        //   params.append('filter', filter);
        // }

        // const urlParams = props.select.urlParams || [];

        // urlParams.forEach((item) => {
        //   if (item.value) {
        //     params.append(item.paramName || item.fieldName, item.value);
        //   } else
        //     console.log(
        //       'JumpSelectServer.filters',
        //       'Valor inválido para o atributo filters.'
        //     );
        // });

        // if (params.toString()) {
        //   url += url.includes('?') ? '&' : '?';
        //   url += params.toString();
        // }

        // const response = await api.get(url);
        // return response.data;
      }

      return [];
    }

    const getUrlRequest = (url: string, filter: string): string => {
      const params = getUrlFilter(filter);
      getCustomUrlParams(params);
      getFormUrlParams(params);

      if (params.toString()) {
        url += url.includes('?') ? '&' : '?';
        url += params.toString();
      }

      return url;
    };

    const getUrlFilter = (filter: string): URLSearchParams => {
      const params = new URLSearchParams();

      if (filter) {
        params.append('filter', filter);
      }

      return params;
    };

    const getCustomUrlParams = (params: URLSearchParams) => {
      const urlParams = props.select.urlParams || [];

      if (urlParams.length == 0) return params;

      urlParams.forEach((param) => {
        params.append(param.paramName, param.value.toString());
      });
    };

    const getFormUrlParams = (params: URLSearchParams) => {
      const urlParams = props.select.formUrlParams || [];

      if (urlParams.length == 0) return params;

      urlParams.forEach((param) => {
        param.value
          ? params.append(param.fieldName, param.value.toString())
          : console.error(
              'JumpSelectServer.montarFormUrlParametros',
              'O campo value deve ter um valor!'
            );
      });
    };

    function filtrarFn(val: string, update: any, abort: any, apiUrl: any) {
      const length = props.select.searchOnCharLength || 0;

      if (val.length < length) {
        abort();
        return;
      }

      options.value = [];

      update(async () => {
        const filtro = val.toLowerCase();
        const data = await fetchData(apiUrl, filtro);
        options.value = data;
      });
    }

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

    onBeforeMount(() => {
      options.value = props.select.options || [];
    });

    return {
      modelValue,
      options,
      isVisible,
      filtrarFn,
      handleChange,
    };
  },
});
</script>
