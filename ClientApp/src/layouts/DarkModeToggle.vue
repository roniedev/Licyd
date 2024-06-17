<template>
  <q-toggle
    v-model="darkMode"
    checked-icon="mdi-weather-night"
    color="black"
    unchecked-icon="mdi-white-balance-sunny"
    size="lg"
  />
</template>

<script lang="ts">
import { useQuasar } from 'quasar';
import { defineComponent, onMounted, ref, watch } from 'vue';

export default defineComponent({
  setup() {
    const darkMode = ref(false);
    const $q = useQuasar();

    watch(darkMode, (darkMode) => {
      $q.dark.set(darkMode);
      $q.localStorage.set('darkMode', darkMode);
    });

    onMounted(() => {
      const darkModeIsActive = $q.localStorage.getItem('darkMode');
      darkMode.value = darkModeIsActive ? true : false;
    });

    return {
      darkMode,
    };
  },
});
</script>
