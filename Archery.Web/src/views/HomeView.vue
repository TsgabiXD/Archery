<template>
  <div>
    <v-container v-if="!bearerToken">
      <login-register-form @login="setToken" />
    </v-container>
    <user-home v-else-if="!isAdmin" :token="bearerToken" />
    <v-container v-else>
      <start-event-form
        :token="bearerToken"
        :userId="userId"
        @new-event="newEvent"
      />
      <!-- TODO remove :userId="userId" -->
      <running-events :newEventId="newEventId" :token="bearerToken" />
    </v-container>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import LoginRegisterForm from "../components/LoginRegisterForm.vue";
import UserHome from "../components/UserHome.vue";
import StartEventForm from "../components/StartEventForm.vue";
import RunningEvents from "../components/RunningEvents.vue";

export default Vue.extend({
  name: "HomeView",
  components: {
    LoginRegisterForm,
    UserHome,
    StartEventForm,
    RunningEvents,
  },
  props: {
    resetToken: { type: String, required: true },
  },
  data: () => {
    return {
      token: "",
      isAdmin: false,
      newEventId: -1,
    };
  },
  methods: {
    setToken(token: string): void {
      this.token = token;

      if (this.tokenData.role === "Admin") this.isAdmin = true;
      else this.isAdmin = false;

      this.$emit("login", token);
    },
    newEvent(eventId: number): void {
      this.newEventId = eventId;
    },
  },
  computed: {
    bearerToken: {
      get(): string {
        if (this.resetToken !== this.token) return this.resetToken;
        else return this.token;
      },
    },
    tokenData() {
      if (this.token === "") return undefined;

      var base64Url = this.token.split(".")[1];
      var base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
      var jsonPayload = decodeURIComponent(
        window
          .atob(base64)
          .split("")
          .map(function (c) {
            return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
          })
          .join("")
      );

      let result = JSON.parse(jsonPayload);

      result.username = result['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];

      return result;
    },
  },
});
</script>
