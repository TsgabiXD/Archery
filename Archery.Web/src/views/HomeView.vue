<template>
  <div>
    <v-container v-if="!bearerToken">
      <login-register-form @login="setTokenAndUser" />
    </v-container>
    <user-home v-else-if="!isAdmin" :userId="userId" :token="bearerToken" />
    <v-container v-else>
      <start-event-form
        :token="bearerToken"
        :userId="userId"
        @new-event="newEvent"
      />
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
      username: "",
      isAdmin: false,
      userId: -1,
      newEventId: -1,
    };
  },
  methods: {
    setTokenAndUser(e: {
      token: string;
      username: string;
      role: string;
      userId: number;
    }): void {
      this.token = e.token;
      this.username = e.username;
      this.userId = e.userId;

      if (e.role === "Admin") this.isAdmin = true;
      else this.isAdmin = false;

      this.$emit("login", {
        token: e.token,
        username: e.username,
        userId: e.userId,
      });
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
  },
});
</script>
