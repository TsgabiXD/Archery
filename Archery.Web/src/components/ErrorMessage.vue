<template>
  <v-alert border="left" color="red" dismissible elevation="24" type="error">
    {{ message }}
  </v-alert>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  props: {
    message: { type: String, required: true },
  },
  data() {
    return {};
  },
});
</script>

<style scoped>
.v-alert {
  position: absolute;
  left: 60px;
  bottom: 50px;
  transform: translate(-50px, -40%);
}
</style>
