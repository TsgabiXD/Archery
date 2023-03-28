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
            <v-text-field
              label="Nickname"
              outlined
              v-model="nickname"
              @blur="checkNickName"
              :success-messages="
                !login && !nickIsValid && nickname !== ''
                  ? 'Benutzername ist frei'
                  : ''
              "
              :error-messages="
                !login && nickIsValid && nickname !== ''
                  ? 'Benutzername ist vergeben'
                  : ''
              "
            >
            </v-text-field>
          </v-col>
        </v-row>
        <v-row dense>
          <v-col>
            <v-text-field
              label="Passwort"
              type="password"
              outlined
              v-model="password"
            >
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
      password: "",
<<<<<<< HEAD
      nickIsValid: "" as string | boolean,
=======
>>>>>>> 2a3c12e91e675b0f6ba5957a6905ed50cf979226
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
            this.$emit("login", {
              token: response.data.token,
              username: response.data.username,
              role: response.data.role,
            });
          })
          .catch((err) => console.log(err))
          .finally(() => {
            this.isLoading = false; // TODO authenticate
          });
      else
        axios
          .post("auth/register", {
            firstName: this.firstname,
            lastName: this.lastname,
            username: this.nickname,
            password: this.password,
          })
          .then((response) => {
            this.$emit("login", {
              token: response.data.token,
              username: response.data.username,
              role: response.data.role,
            });
          })
          .catch((err) => console.log(err))
          .finally(() => {
            this.isLoading = false; // TODO authenticate
          });
    },
    checkNickName(): void {
<<<<<<< HEAD
      if (!this.login && this.nickname !== "")
        axios
          .get(`user/checkuser/${this.nickname}`)
          .then((response) => {
            this.nickIsValid = response.data;
          })
          .catch((err) => console.log(err));
=======
      axios.get(`user/checkuser/${this.nickname}`);
>>>>>>> 2a3c12e91e675b0f6ba5957a6905ed50cf979226
    },
  },
});
</script>
