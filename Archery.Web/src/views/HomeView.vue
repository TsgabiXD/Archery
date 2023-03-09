<template>
  <div>
    <v-container v-if="!token">
      <login-register-form @login="bearerToken" />
    </v-container>
    <user-home v-else-if="!isAdmin" />
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import LoginRegisterForm from "../components/LoginRegisterForm.vue";
import UserHome from "../components/UserHome.vue";

export default Vue.extend({
  name: "HomeView",
  components: {
    LoginRegisterForm,
    UserHome,
  },
  data: () => {
    return {
      token: "",
      isAdmin: false,
    };
  },
  computed: {
    bearerToken: {
      set(value: string): void {
        this.token = value;
      },
      get(): string {
        return this.token;
      },
    },
  },
});
</script>
