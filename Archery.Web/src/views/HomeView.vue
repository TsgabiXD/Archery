<template>
  <div>
    <v-container v-if="!token">
      <login-register-form @login="bearerToken" />
    </v-container>
    <user-home v-else-if="!isAdmin" />
    <v-container v-else>
      <start-event-form />
    </v-container>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import LoginRegisterForm from "../components/LoginRegisterForm.vue";
import UserHome from "../components/UserHome.vue";
import StartEventForm from "../components/StartEventForm.vue";

export default Vue.extend({
  name: "HomeView",
  components: {
    LoginRegisterForm,
    UserHome,
    StartEventForm,
  },
  data: () => {
    return {
      token: "x", // TODO default ""
      isAdmin: true, // TODO default false
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
