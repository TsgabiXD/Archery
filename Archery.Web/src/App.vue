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
            <v-list-item-title v-if="token" @click="clearToken"> Logout </v-list-item-title>
          </v-list-item>
        </v-list>
      </v-menu>
    </v-app-bar>
    <v-main>
      <router-view :bearerToken="token" @login="setToken" />
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
    };
  },
  methods: {
    setToken(token: string) {
      this.token = token;
    },
    clearToken() {
      this.token = "";
    },
  },
});
</script>
