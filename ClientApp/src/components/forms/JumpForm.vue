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

import { groupBy } from 'lodash';
import { IJumpForm } from './interfaces/IJumpForm';
import { ICustomRule } from './interfaces/ICustomRule';
import { FormElementType } from './enums/FormElementType';
import { defineComponent, onBeforeMount, watch } from 'vue';
import { IJumpSelectServer } from './interfaces/IJumpSelectServer';

export default defineComponent({
  name: 'JumpForm',
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
          formData[field.name] = field.value;
        }
      });

      emit('submit', formData);
    };

    const handleCancel = () => {
      emit('cancel');

      // eslint-disable-next-line vue/no-mutating-props
      props.form.data = {};
    };

    const watchFieldsValue = () => {
      props.form.fields.forEach((field) => {
        watch(
          () => field.value,
          () => {
            props.form.fields.forEach((field) => {
              executeFieldCustomRules(field);
              if (field.type === FormElementType.selectServer)
                setSelectUrlParams(field as IJumpSelectServer);
            });
          },
          { immediate: true }
        );
      });
    };

    const setSelectUrlParams = (field: IJumpSelectServer) => {
      if (field.urlParams) {
        field.urlParams.forEach((item) => {
          const value = item.value
            ? item.value
            : getValueByName(item.fieldName);

          if (value) item.value = value?.toString();
          else
            console.log(
              'JumpForm.urlParams',
              `Valor não encontrado para o atributo ${item.fieldName}.`
            );
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
        : false;
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
      return input ? input.value : undefined;
    };

    onBeforeMount(() => {
      executeFormCustomRules(props.form);
      if (props.form.disabled) {
        props.form.fields.forEach((field) => {
          field.disabled = true;
        });
      }
    });

    const setData = () => {
      props.form.fields.forEach((field) => {
        if (props.form.data) field.value = props.form.data[field.name];

        if (field.type === FormElementType.selectServer) {
          const select = field as IJumpSelectServer;
          const labelNameField = select.labelNameField || '';

          if (labelNameField && props.form.data) {
            const optionValue = select.optionValue || 'value';
            const optionLabel = select.optionLabel || 'label';

            const option: any = {};
            option[optionValue] = field.value;
            option[optionLabel] = props.form.data[labelNameField];

            (field as IJumpSelectServer).options = [];
          } else {
            console.log(
              'JumpForm.SelectServer:',
              'Defina o nome do atributo labelNameField.'
            );
          }
        }
      });
    };

    watch(
      () => props.form.data,
      () => setData(),
      { immediate: true }
    );

    watchFieldsValue();

    return {
      handleSubmit,
      handleCancel,
    };
  },
});
</script>
