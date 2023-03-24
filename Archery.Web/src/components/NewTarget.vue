<template>
  <v-overlay :value="show">
    <!-- <v-dialog
        v-model="dialog"
        fullscreen
        hide-overlay
        transition="dialog-bottom-transition"
      >
      </v-dialog> -->
  </v-overlay>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  props: {
    show: { type: Boolean, required: true },
  },
});
</script>
