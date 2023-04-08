<template>
  <v-app>
    <v-app-bar app color="primary" dark>
      <v-app-bar-nav-icon>
        <v-img contain max-width="65" src="../public/Acherry.png"> </v-img>
      </v-app-bar-nav-icon>
      <v-spacer></v-spacer>
      <v-tooltip v-if="tokenData?.role === 'Admin'">
        <template v-slot:activator="{ on, attrs }">
          <v-switch
            color="amber lighten-1"
            v-bind="attrs"
            v-on="on"
            v-model="adminMode"
            style="margin-top: auto; padding-top: 14px"
          >
          </v-switch>
        </template>
        <span>mitspielen</span>
        <!-- TODO fix tooltip -->
      </v-tooltip>
      <v-menu
        offset-y
        open-on-hover
        :close-on-content-click="false"
        :disabled="!token"
      >
        <template v-slot:activator="{ on, attrs }">
          <v-btn icon v-bind="attrs" v-on="on">
            <v-icon>mdi-account</v-icon>
          </v-btn>
        </template>
        <v-list>
          <v-list-item>
            <v-list-item-title class="title">
              {{ tokenData?.username }}
            </v-list-item-title>
          </v-list-item>
          <v-btn tile text block @click="logout"> Logout </v-btn>
        </v-list>
      </v-menu>
    </v-app-bar>
    <v-main>
      <router-view
        :resetToken="token"
        :adminMode="adminMode"
        @login="setToken"
      />
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
      adminMode: false,
    };
  },
  methods: {
    setToken(token: string) {
      this.token = token;
    },
    logout() {
      this.token = "";
    },
  },
  computed: {
    tokenData() {
      if (this.token === "") return undefined;

      let base64Url = this.token.split(".")[1];
      let base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
      let jsonPayload = decodeURIComponent(
        window
          .atob(base64)
          .split("")
          .map((c) => "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2))
          .join("")
      );

      let result = JSON.parse(jsonPayload);

      result.role =
        result["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      result.username =
        result["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];

      return result;
    },
  },
  watch: {
    tokenData() {
      this.adminMode = this.tokenData?.role === "Admin";
    },
  },
});
</script>
