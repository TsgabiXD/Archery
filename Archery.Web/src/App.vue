<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <v-spacer></v-spacer>
      <v-menu offset-y :disabled="!token">
        <template v-slot:activator="{ on, attrs }">
          <v-btn icon v-bind="attrs" v-on="on">
            <v-icon>mdi-account</v-icon>
          </v-btn>
        </template>
        <v-list>
          <v-list-item>
            <v-list-item-title class="title">
              {{ username }}
            </v-list-item-title>
          </v-list-item>
          <v-list-item>
            <v-list-item-title @click="clearTokenAndUser">
              Logout
            </v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-app-bar>
    <v-main>
      <router-view :resetToken="token" @login="setTokenAndUser" />
    </v-main>
  </v-app>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  name: "App",
  data: () => {
    return {
      token: "",
      isAdmin: false,
      username: "",
      userId: -1,
    };
  },
  methods: {
    setTokenAndUser(e: { token: string; username: string; userId: number }) {
      this.token = e.token;
      this.username = e.username;
      this.userId = e.userId;
    },
    clearTokenAndUser() {
      this.token = "";
      this.username = "";
      this.userId = -1;
    },
  },
});
</script>
