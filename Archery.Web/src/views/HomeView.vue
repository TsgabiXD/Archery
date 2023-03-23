<template>
  <div>
    <v-container v-if="!token">
      <login-register-form @login="setToken" />
    </v-container>
    <user-home v-else-if="!isAdmin" :token="token" />
    <v-container v-else>
      <start-event-form :token="token" />
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
      token: "",
      isAdmin: false,
    };
  },
  methods:{
    setToken(token: string){
      this.token = token;
    }
  }
});
</script>
