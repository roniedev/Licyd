<template>
  <div class="q-pa-md">
    <q-card flat class="q-pa-md form">
      <q-form @submit.prevent="handleSubmit">
        <q-card-section>
          <span class="text-h5">{{ title }}</span>
        </q-card-section>
        <q-card-section>
          <slot name="top"></slot>
          <template v-for="(field, index) in formProps" :key="index">
            <template v-if="field.type === 'radio'">
              <fieldset class="fieldset">
                <legend>{{ field.labelFieldset }}</legend>
                <q-option-group
                  :options="field.options"
                  type="radio"
                  v-model="fieldValueMap[field.name]"
                  inline
                />
              </fieldset>
            </template>
            <template v-else>
              <component
                filled
                class="input"
                :dense="field.dense"
                :is="fieldToComponent[field.type]"
                :type="field.type"
                :options="field.options"
                :multiple="field.type === 'multiselect'"
                :name="field.name"
                :label="field.label"
                :rules="field.required ? field.requiredRules : []"
                :step="field.step"
                v-model="fieldValueMap[field.name]"
                use-input
                emit-value
                map-options
                @filter="(inputValue: string, doneFn: any, abortFn: any) =>
                      filterFn(inputValue, doneFn, abortFn, field.urlRequestOption, field)"
              >
                <template
                  v-if="field.type === 'select' || field.type === 'multiselect'"
                  v-slot:append
                >
                  <q-icon
                    v-if="fieldValueMap[field.name]"
                    class="cursor-pointer"
                    name="clear"
                    @click.stop.prevent="fieldValueMap[field.name] = null"
                  />
                </template>
              </component>
            </template>
          </template>
          <slot name="bottom"></slot>
        </q-card-section>
        <q-card-actions align="right">
          <q-btn
            flat
            size="md"
            color="negative"
            label="Cancelar"
            @click="cancelar"
            v-close-popup
          />
          <q-btn
            flat
            size="md"
            color="primary"
            label="Salvar"
            type="submit"
            v-close-popup
          />
        </q-card-actions>
      </q-form>
    </q-card>
  </div>
</template>

<script lang="ts">
import moment from 'moment';
import { hideLoading, showLoading } from 'src/helpers/Loading';
import { notificar } from 'src/helpers/Notificacao';
import { IFormProps } from 'src/interfaces/components/IForm';
import AxiosServiceOld from 'src/services/AxiosServiceOld';
import { computed, defineComponent, ref, watch } from 'vue';
import { useRouter } from 'vue-router';

type FieldToComponentMap = {
  [key: string]: string;
};

export default defineComponent({
  name: 'TForm',

  props: {
    title: String,
    formProps: {
      type: Array<IFormProps>,
      required: true,
    },
    formData: {
      type: Object,
      default: () => ({}),
    },
  },

  emits: ['onSubmit'],

  setup(props, { emit }) {
    const router = useRouter();
    let fieldValueMap = ref({} as any);

    watch(
      () => props.formData,
      (newData) => {
        for (const field of props.formProps) {
          if (newData[field.name]) {
            if (field.type === 'date') {
              fieldValueMap.value[field.name] = moment(
                newData[field.name]
              ).format('YYYY-MM-DD');
            } else if (field.type === 'select') {
              field.options = [
                {
                  value: newData[field.name],
                  label: newData[field.labelSelect || ''],
                },
              ];

              fieldValueMap.value[field.name] = newData[field.name];
            } else {
              fieldValueMap.value[field.name] = newData[field.name];
            }
          } else {
            if (field.type === 'toggle') {
              fieldValueMap.value[field.name] = false;
            }
          }
        }
      },
      { immediate: true }
    );

    const fieldToComponent = computed(() => {
      return {
        text: 'q-input',
        password: 'q-input',
        date: 'q-input',
        number: 'q-input',
        textarea: 'q-input',
        select: 'q-select',
        multiselect: 'q-select',
        toggle: 'q-toggle',
        checkbox: 'q-checkbox',
        file: 'q-file',
      } as FieldToComponentMap;
    });

    function handleSubmit() {
      let errors = '';
      const formData: any = {};

      if (errors) {
        notificar(errors, 'warning');
        return;
      }

      for (const field of props.formProps) {
        formData[field.name] = fieldValueMap.value[field.name];
      }

      emit('onSubmit', formData);
    }

    function cancelar() {
      router.back();
    }

    async function fetchData(url: string, filtro: string) {
      url += `/${filtro}`;
      const response = await AxiosServiceOld.get(url);

      if (response.sucesso) return response.objeto;

      notificar(response.mensagem, 'negative', false, 0);
    }

    function filterFn(
      val: string,
      update: any,
      abort: any,
      apiUrl: any,
      field: IFormProps
    ) {
      field.options = [];

      update(async () => {
        showLoading();
        const filtro = val.toLowerCase();
        const data = await fetchData(apiUrl, filtro);
        field.options = data;
        hideLoading();
      });
    }

    return {
      fieldToComponent,
      fieldValueMap,
      handleSubmit,
      cancelar,
      filterFn,
    };
  },
});
</script>

<style lang="sass" scoped>
.form
  border: 1px solid rgb(0, 0, 0, 0.20)

.input
  margin-bottom: 10px
  padding-top: 10px
  font-size: 12px
  margin-right: 15px

.fieldset
  border: 1px solid rgb(0, 0, 0, 0.20)
  margin-bottom: 10px
  margin-right: 15px
</style>
