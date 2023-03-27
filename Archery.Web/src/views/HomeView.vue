<template>
  <div>
    <v-container v-if="!bearerToken">
      <login-register-form @login="setTokenAndUser" />
    </v-container>
    <user-home v-else-if="!isAdmin" :token="bearerToken" />
    <v-container v-else>
      <start-event-form :token="bearerToken" />
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
  props: {
    resetToken: { type: String, required: true },
  },
  data: () => {
    return {
      token: "",
      username: "",
      isAdmin: false,
    };
  },
  methods: {
    setTokenAndUser(e: { token: string; username: string; role: string }) {
      this.token = e.token;
      this.username = e.username;

      if (e.role === "Admin") this.isAdmin = true;

      this.$emit("login", {
        token: e.token,
        username: e.username,
      });
    },
  },
  computed: {
    bearerToken: {
      get(): string {
        if (this.resetToken !== this.token) return this.resetToken;
        else return this.token;
      },
    },
  },
});
</script>
