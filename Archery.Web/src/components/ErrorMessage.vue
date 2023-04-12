<template>
  <v-alert class="alert ma-5" color="red" dismissible elevation="25" type="error">
    {{message}}
  </v-alert>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  props: {
    message: String,
  },
  data() {
    return {};
  },
});
</script>

<style scoped>
.alert {
  position: absolute;
  left: 50px;
  bottom: 10px;
  transform: translate(-50%, -50%);
}
</style>
