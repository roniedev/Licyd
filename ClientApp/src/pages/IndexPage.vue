<template>
  <p></p>
</template>

<script lang="ts">
import NivelHierarquicoService from 'src/services/NivelHierarquicoService';

export default {
  name: 'IndexPage',

  setup() {
    var service = NivelHierarquicoService;

    return {
      service,
    };
  },
};
</script>
