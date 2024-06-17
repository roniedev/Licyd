<template>
  <div class="q-pa-md">
    <q-card flat class="q-pa-md form">
      <q-form @submit.prevent.stop="handleSubmit">
        <q-card-section>
          <div class="flex items-center justify-center">
            <span class="text-h5">{{ form.title }}</span>
          </div>
          <q-separator />
          <div class="row">
            <template v-for="(field, index) in form.fields" :key="index">
              <div :class="`col-${field.cols}`">
                <t-form-element
                  :element="field"
                  v-model="modelValues[field.name]"
                />
              </div>
            </template>
          </div>
          <slot name="bottom"></slot>
        </q-card-section>
        <q-card-actions class="text-teal">
          <slot name="acoesEsquerda"></slot>
          <q-space />
          <slot name="acoesDireita"></slot>
          <q-btn
            flat
            size="md"
            color="negative"
            label="Cancelar"
            @click="handleCancelar"
          />
          <q-btn
            flat
            size="md"
            color="primary"
            :label="form.btnSubmitLabel || 'enviar'"
            type="submit"
          />
        </q-card-actions>
      </q-form>
    </q-card>
  </div>
</template>

<script lang="ts">
import moment from 'moment';
import TFormElement from './TFormElement.vue';

import { IForm } from 'src/interfaces/components/IForm';
import { defineComponent, ref, watch } from 'vue';

export default defineComponent({
  name: 'TForm',

  props: {
    form: {
      type: Object as () => IForm,
      required: true,
    },
    initialData: {
      type: Object as () => any,
      default: () => ({} as any),
    },
  },

  components: {
    TFormElement,
  },

  emits: ['submit', 'cancelar'],

  setup(props, { emit }) {
    // eslint-disable-next-line vue/no-setup-props-destructure
    let modelValues = ref<any>(props.initialData);

    function handleSubmit() {
      const formValues = {} as any;

      for (const field of props.form.fields) {
        if (!modelValues.value[field.name] && field.type !== 'toggle') {
          formValues[field.name] = null;
          continue;
        }

        if (field.type === 'select') {
          formValues[field.name] = modelValues.value[field.name].value;
          continue;
        }

        formValues[field.name] = modelValues.value[field.name];
      }

      emit('submit', formValues);
    }

    function handleCancelar() {
      emit('cancelar');
    }

    watch(
      () => props.initialData,
      (value) => {
        for (const field of props.form.fields) {
          if (value[field.name]) {
            if (field.type === 'select') {
              field.options = [
                {
                  value: value[field.name],
                  label: value[field.labelSelect || field.name],
                },
              ];

              modelValues.value[field.name] = value[field.name];
              continue;
            }

            if (field.type === 'date') {
              const data = value[field.name]
                ? moment(value[field.name]).format('yyyy-MM-DD')
                : null;

              modelValues.value[field.name] = data;
              continue;
            }

            modelValues.value[field.name] = value[field.name];
            continue;
          }

          if (field.type === 'toggle') {
            modelValues.value[field.name] = false;
          }
        }
      },
      { immediate: true }
    );

    return {
      modelValues,
      handleSubmit,
      handleCancelar,
    };
  },
});
</script>

<style lang="sass" scoped>
.form
  border: 1px solid rgb(0, 0, 0, 0.10)

.text-h5
  font-size: 22.5px
</style>
