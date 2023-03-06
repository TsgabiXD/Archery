<template>
  <v-container>
    <login-register-form />
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import LoginRegisterForm from "../components/LoginRegisterForm.vue";

export default Vue.extend({
  name: "HomeView",
  data: () => {
    return {};
  },
  components: {
    LoginRegisterForm,
  },
});
</script>
