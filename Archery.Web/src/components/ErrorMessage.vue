<template>
    <v-alert border="left" color="red" dismissible elevation="24"  type="error">
    {{message}}
    </v-alert>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  props: {
    message: String,
  },
});
</script>

<style scoped>
.v-alert {
  position: absolute;
  left: 50%;
  bottom: 50px;
  transform: translate(-700%, -40%);

}
</style>
