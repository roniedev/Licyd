<template>
  <q-btn dense flat icon="mdi-dots-vertical">
    <q-menu transition-show="jump-down" transition-hide="jump-up">
      <q-list dense>
        <q-item
          clickable
          v-close-popup
          class="q-ma-sm"
          v-for="(action, index) in visibleActions"
          :key="index"
          @click="action.onClick(row)"
        >
          <q-item-section avatar>
            <q-avatar :icon="action.icon || 'mdi-chevron-right'" size="md" />
          </q-item-section>
          <q-item-section style="margin-right: 15px">
            <q-item-label>{{ action.name }}</q-item-label>
          </q-item-section>
        </q-item>
      </q-list>
    </q-menu>
  </q-btn>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { computed } from 'vue';
import { ITableAction } from './interfaces/ITableAction';

export default defineComponent({
  name: 'TableActions',

  props: {
    actions: {
      type: Object as () => Array<ITableAction>,
      required: true,
    },
    row: {
      type: Object as () => any,
      required: true,
    },
  },

  setup(props) {
    const isVisible = (item: ITableAction) => {
      if (typeof item.visible === 'function') return item.visible(props.row);
      return item.visible !== false;
    };

    const visibleActions = computed(() => {
      return props.actions.filter(isVisible);
    });

    return {
      visibleActions,
    };
  },
});
</script>
