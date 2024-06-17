<template>
  <q-btn-dropdown
    v-if="prop.visible || true"
    :label="prop.label"
    :dense="prop.dense || false"
    :flat="prop.flat || false"
    :icon="prop.icon || 'mdi-dots-vertical'"
  >
    <q-list dense>
      <q-item
        clickable
        v-close-popup
        class="q-ma-sm"
        v-for="(item, index) in items"
        :key="index"
        @click="item.onClick(row)"
      >
        <q-item-section avatar>
          <q-avatar :icon="item.icon || 'mdi-chevron-right'" size="md" />
        </q-item-section>
        <q-item-section style="font-size: 13px">
          <q-item-label>{{ item.label }}</q-item-label>
        </q-item-section>
      </q-item>
    </q-list>
  </q-btn-dropdown>
</template>

<script lang="ts">
import { computed, defineComponent } from 'vue';
import { IButtonDropdown } from './interfaces/IButtonDropdown';
import { IButtonDropdownItem } from './interfaces/IButtonDropdown';

export default defineComponent({
  name: 'ButtonDropdown',

  props: {
    prop: {
      type: Object as () => IButtonDropdown,
      required: true,
    },
    row: {
      type: Object as () => any,
    },
  },

  setup(props) {
    const visible = (item: IButtonDropdownItem) => {
      if (typeof item.visible === 'function') return item.visible();
      return item.visible !== false;
    };

    const items = computed(() => {
      return props.prop.items.filter(visible);
    });

    return {
      items,
    };
  },
});
</script>
