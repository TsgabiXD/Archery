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
        <!-- TODO fix tooltip (nur on focus not on hover) -->
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
          <v-btn tile text block @click="deleteUserForm = true" color="red">
            Löschen
          </v-btn>
        </v-list>
      </v-menu>
    </v-app-bar>
    <v-main>
      <v-overlay :value="deleteUserForm">
        <v-btn color="red" @click="deleteUser"> Sicher? </v-btn>
      </v-overlay>
      <router-view
        :resetToken="token"
        :adminMode="adminMode"
        @login="setToken"
        @error="addErrorMessage"
      />
    </v-main>
    <div class="messageList">
      <error-message
        v-for="(message, i) in errorMessages"
        :key="i"
        :message="message"
      >
      </error-message>
    </div>
  </v-app>
</template>

<script lang="ts">
import Vue from 'vue';

import ErrorMessage from '@/components/ErrorMessage.vue';
import axios from './router/axios';
import { AxiosError } from 'axios';

export default Vue.extend({
  name: 'App',
  components: {
    ErrorMessage,
  },
  data: () => {
    return {
      token: '',
      adminMode: false,
      errorMessages: [] as string[],
      deleteUserForm: false,
    };
  },
  methods: {
    setToken(token: string) {
      this.token = token;
    },
    logout() {
      this.token = '';
    },
    addErrorMessage(errorMessage: string): void {
      this.errorMessages.push(errorMessage);
    },
    deleteUser(): void {
      axios
        .delete('user/deleteuser', this.axiosAuthConfig)
        .then(() => {
          this.logout();
          this.deleteUserForm = false;
        })
        .catch((err: AxiosError) =>
          this.throwError(err.response?.data as string)
        );
    },
    throwError(errorMessage: string): void {
      this.$emit('error', errorMessage);
    },
  },
  computed: {
    axiosAuthConfig(): object {
      return { headers: { Authorization: `Bearer ${this.token}` } };
    },
    tokenData() {
      if (this.token === '') return undefined;

      let base64Url = this.token.split('.')[1];
      let base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      let jsonPayload = decodeURIComponent(
        window
          .atob(base64)
          .split('')
          .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
          .join('')
      );

      let result = JSON.parse(jsonPayload);

      result.role =
        result['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      result.username =
        result['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];

      return result;
    },
  },
  watch: {
    tokenData() {
      this.adminMode = this.tokenData?.role === 'Admin';
    },
  },
});
</script>

<style scoped>
.messageList {
  position: fixed;
  left: 60px;
  bottom: 0;
}
</style>
