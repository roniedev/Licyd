<template>
  <q-card flat class="q-pa-xs">
    <div class="row">
      <div :class="`col-md-${form.cols || 12} col-xs-12`">
        <q-form @submit.prevent.stop="handleSubmit">
          <q-card-section>
            <span class="text-h6" v-if="form.title">{{ form.title }}</span>
            <q-separator v-if="form.title" />
            <div class="row">
              <div class="col-md-12 col-xs-12 q-pa-xs">
                <div class="row">
                  <slot name="before-fields"></slot>
                </div>
              </div>
              <template v-for="(field, index) in form.fields" :key="index">
                <template v-if="field.putFieldInNewLine">
                  <div class="col-md-12 col-xs-12">
                    <div class="row">
                      <div :class="`col-md-${field.cols || 12} col-xs-12`">
                        <jump-form-field
                          :field="field"
                          v-model:model-value="field.value"
                        />
                      </div>
                    </div>
                  </div>
                </template>
                <template v-else>
                  <div :class="`col-md-${field.cols || 12} col-xs-12`">
                    <jump-form-field
                      :field="field"
                      v-model:model-value="field.value"
                    />
                  </div>
                </template>
              </template>
              <div class="col-md-12 col-xs-12">
                <div class="row">
                  <div class="col">
                    <slot name="after-fields"></slot>
                  </div>
                </div>
              </div>
            </div>
          </q-card-section>
          <q-card-actions v-if="form.showActions === false ? false : true">
            <slot name="left-actions-before"></slot>
            <q-btn
              flat
              size="md"
              color="negative"
              v-show="form.showCancelButton === false ? false : true"
              :icon="form.iconCancelButton || 'mdi-arrow-left'"
              :label="form.labelCancelButton"
              @click="handleCancel"
            />
            <slot name="left-actions-after"></slot>
            <q-space />
            <slot name="right-actions-before"></slot>
            <q-btn
              v-if="!form.disabled"
              flat
              size="md"
              type="submit"
              color="primary"
              v-show="form.showSubmitButton === false ? false : true"
              :icon="form.iconSubmitButton || 'mdi-content-save-outline'"
              :label="form.labelSubmitButton"
              :disable="form.disabled || false"
            >
              <q-tooltip
                v-if="form.useToolipOnSubmitButton || false"
                anchor="top middle"
                self="bottom middle"
                :offset="[10, 10]"
              >
                {{ form.tooltipTextSubmitButton }}
              </q-tooltip>
            </q-btn>
            <slot name="right-actions-after"></slot>
          </q-card-actions>
        </q-form>
        <slot name="after-form"></slot>
      </div>
    </div>
  </q-card>
</template>

<script lang="ts">
import JumpFormField from './JumpFormField.vue';

import { date } from 'quasar';
import { groupBy } from 'lodash';
import { IJumpForm } from './interfaces/IJumpForm';
import { ICustomRule } from './interfaces/ICustomRule';
import { FormElementType } from './enums/FormElementType';
import { defineComponent, onBeforeMount, watch } from 'vue';
import { IJumpSelectServer } from './interfaces/IJumpSelectServer';

export default defineComponent({
  components: {
    JumpFormField,
  },

  props: {
    form: {
      type: Object as () => IJumpForm,
      required: true,
    },
  },

  emits: ['submit', 'cancel'],

  setup(props, { emit }) {
    const handleSubmit = () => {
      let formData: any = {};

      props.form.fields.forEach((field) => {
        if (!field.disabled && field.value != undefined) {
          if (
            field.type === FormElementType.selectClient ||
            field.type === FormElementType.selectServer
          ) {
            const optionValue =
              (field as IJumpSelectServer).optionValue || 'value';

            if ((field as IJumpSelectServer).multiselect) {
              formData[field.name] = field.value;
            } else if ((field as IJumpSelectServer).emitValue === false) {
              formData[field.name] =
                typeof field.value === 'number'
                  ? field.value
                  : field.value[optionValue];
            } else {
              formData[field.name] =
                typeof field.value == 'object'
                  ? field.value[optionValue]
                  : field.value;
            }
          } else {
            formData[field.name] = field.value;
          }
        }
      });

      emit('submit', formData);
    };

    const handleCancel = () => {
      emit('cancel');
      resetForm();
    };

    const watchFieldsValue = () => {
      props.form.fields.forEach((field) => {
        watch(
          () => field.value,
          () => {
            props.form.fields.forEach((field) => {
              executeFieldCustomRules(field);
              if (field.type === FormElementType.selectServer)
                getSelectServerFormUrlParams(field as IJumpSelectServer);
            });
          },
          { immediate: true }
        );
      });
    };

    const getSelectServerFormUrlParams = (field: IJumpSelectServer) => {
      if (field.formUrlParams) {
        field.formUrlParams.forEach((item) => {
          const value = getValueByName(item.fieldName);
          if (value) item.value = value?.toString();
        });
      }
    };

    const executeFieldCustomRules = (field: any) => {
      if (props.form.disabled !== undefined)
        field.disabled = props.form.disabled;
      else {
        field.readonly = field.readonlyRules
          ? validateCustomRules(field.readonlyRules)
          : field.readonly != undefined && field.readonly != null
          ? field.readonly
          : false;

        field.disabled = field.disabledRules
          ? validateCustomRules(field.disabledRules)
          : field.disabled != undefined && field.disabled != null
          ? field.disabled
          : false;
      }

      field.visible = field.visibleRules
        ? validateCustomRules(field.visibleRules)
        : field.visible != undefined && field.visible != null
        ? field.visible
        : true;
    };

    const executeFormCustomRules = (form: IJumpForm) => {
      form.disabled = props.form.disabled
        ? true
        : props.form.disabledRules
        ? validateCustomRules(props.form.disabledRules)
        : undefined;
    };

    const validateCustomRules = (rules: Array<ICustomRule>): boolean => {
      let results: Array<boolean> = [];

      const grouper = groupBy(rules, 'grouper');

      for (const key in grouper) {
        if (Object.prototype.hasOwnProperty.call(grouper, key)) {
          const rules = grouper[key];
          const ruleResults: Array<boolean> = [];

          rules.forEach((x) => {
            const value = getValueByName(x.name);
            const result =
              typeof x.value === 'function' ? x.value(value) : x.value == value;

            ruleResults.push(result);
          });

          results.push(ruleResults.every((x) => x === true));
        }
      }

      return results.every((x) => x === true);
    };

    const getValueByName = (
      name: string
    ): string | number | boolean | Array<any> | null | undefined => {
      let input = props.form.fields.find((field) => field.name === name);

      if (!input) return undefined;

      let value = undefined;
      const isSelectServer = input.type === FormElementType.selectServer;
      const isSelectClient = input.type === FormElementType.selectClient;

      if (isSelectClient || isSelectServer) {
        const select = input as IJumpSelectServer;
        const optionValue = select.optionValue || 'value';
        value = select.value
          ? typeof select.value !== 'object'
            ? select.value
            : select.value[optionValue]
          : undefined;
      } else {
        value = input.value;
      }

      return value;
    };

    onBeforeMount(() => {
      inicializar();
    });

    const inicializar = () => {
      executeFormCustomRules(props.form);
    };

    const setData = () => {
      props.form.fields.forEach((field) => {
        if (!props.form.data) {
          if (
            field.type === FormElementType.toggle ||
            field.type === FormElementType.checkbox
          ) {
            field.value = false;
          }
        } else if (props.form.data && field.type === FormElementType.date) {
          field.value = date.formatDate(
            props.form.data[field.name] as Date,
            'YYYY-MM-DD'
          );
        } else if (field.type === FormElementType.selectServer) {
          const select = field as IJumpSelectServer;
          const labelNameField = select.labelNameField || '';
          if (labelNameField && props.form.data) {
            const optionValue = select.optionValue || 'value';
            const optionLabel = select.optionLabel || 'label';
            const option: any = {};
            option[optionValue] = field.value;
            option[optionLabel] = props.form.data[labelNameField];
            (field as IJumpSelectServer).options = [option];
          } else {
            console.error(
              'JumpForm.SelectServer:',
              'Defina o nome do atributo labelNameField.'
            );
          }
        } else if (props.form.data) {
          field.value = props.form.data[field.name];
        }
      });
    };

    const resetForm = () => {
      // eslint-disable-next-line vue/no-mutating-props
      props.form.data = undefined;
      props.form.fields.forEach((field) => (field.value = undefined));
    };

    watch(
      () => props.form.data,
      () => {
        setData();
      },
      {
        immediate: true,
      }
    );

    watchFieldsValue();

    return {
      handleSubmit,
      handleCancel,
    };
  },
});
</script>
