<template>
  <v-card elevation="7" :loading="isLoading">
    <v-card-title>
      {{ isLogin ? "Login" : "Registrieren" }}
      <v-spacer></v-spacer>
      <v-btn elevation="2" outlined rounded small @click="switchState">
        {{ isLogin ? "Kein Account?" : "Zurück zum Login?" }}
      </v-btn>
    </v-card-title>
    <v-card-text>
      <v-container class="grey lighten-5" rounded>
        <v-row v-if="!isLogin" dense>
          <v-col cols="12" md="6">
            <v-text-field label="Vorname" outlined v-model="firstname">
            </v-text-field>
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field label="Nachname" outlined v-model="lastname">
            </v-text-field>
          </v-col>
        </v-row>
        <v-row dense>
          <v-col>
            <v-text-field label="Nickname" outlined v-model="nickname">
            </v-text-field>
          </v-col>
        </v-row>
        <v-row dense>
          <v-col>
            <v-text-field label="Passwort" type="password" outlined v-model="password">
            </v-text-field>
          </v-col>
        </v-row>
      </v-container>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="primary" class="mb-2" elevation="2" @click="registerLogin">
        {{ isLogin ? "Einloggen" : "Registrieren" }}
      </v-btn>
      <v-spacer></v-spacer>
    </v-card-actions>
  </v-card>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import axios from "@/router/axios";

export default defineComponent({
  data: () => {
    return {
      login: true,
      isLoading: false,
      firstname: "",
      lastname: "",
      nickname: "",
      password: ""
    };
  },
  computed: {
    isLogin: {
      set(val: boolean) {
        this.login = val;
      },
      get(): boolean {
        return this.login;
      },
    },
  },
  methods: {
    switchState(): void {
      this.isLogin = !this.isLogin;
      this.isLoading = false;
    },
    registerLogin(): void {
      this.isLoading = true;

      if (this.isLogin)
        axios
          .post("auth/login", {
            username: this.nickname,
            password: this.password,
          }) // TODO login
          .then((response) => {
            this.$emit("login", response.data.token);
          })
          .catch((err) => console.log(err))
          .finally(() => {
            this.isLoading = false; // TODO authenticate
          });
      else
        axios
          .post("user/adduser", {
            firstName: this.firstname,
            lastName: this.lastname,
            nickName: this.nickname,
            role: "User"
          })
          .then((response) => {
            this.$emit("login", response.data.token);
          })
          .catch((err) => console.log(err))
          .finally(() => {
            this.isLoading = false; // TODO authenticate
          });
    },
  },
});
</script>
