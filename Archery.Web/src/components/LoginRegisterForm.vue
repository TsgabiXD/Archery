<template>
  <v-card elevation="7" :loading="isLoading" v-focus>
    <v-card-title>
      {{ isLogin ? 'Login' : 'Registrieren' }}
      <v-spacer></v-spacer>
      <v-btn elevation="2" outlined rounded small @click="switchState">
        {{ isLogin ? 'Kein Account?' : 'Zurück zum Login?' }}
      </v-btn>
    </v-card-title>
    <v-card-text>
      <v-container class="grey lighten-5" rounded>
        <v-row v-if="!isLogin" dense>
          <v-col cols="12" md="6">
            <v-text-field
              label="Vorname"
              outlined
              v-model="firstname"
              @keypress.native.enter="registerLogin"
            >
            </v-text-field>
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field
              label="Nachname"
              outlined
              v-model="lastname"
              @keypress.native.enter="registerLogin"
            >
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
              @keypress.native.enter="registerLogin"
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
              @keypress.native.enter="registerLogin"
            >
            </v-text-field>
          </v-col>
        </v-row>
      </v-container>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="primary" class="mb-2" elevation="2" @click="registerLogin">
        {{ isLogin ? 'Einloggen' : 'Registrieren' }}
      </v-btn>
      <v-spacer></v-spacer>
    </v-card-actions>
  </v-card>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import axios from '@/router/axios';
import { AxiosError } from 'axios';

export default defineComponent({
  data: () => {
    return {
      login: true,
      isLoading: false,
      firstname: '',
      lastname: '',
      nickname: '',
      password: '',
      nickIsValid: '' as string | boolean,
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
    throwError(errorMessage: string): void {
      this.$emit('error', errorMessage);
    },
    switchState(): void {
      this.isLogin = !this.isLogin;
      this.isLoading = false;
    },
    registerLogin(): void {
      this.isLoading = true;

      if (this.isLogin)
        axios
          .post('auth/login', {
            username: this.nickname,
            password: this.password,
          }) // TODO login
          .then((response) => {
            this.$emit('login', response.data.token);
          })
          .catch((err: any) => {
            // TODO add Type
            if (err.response?.data?.errors?.Password[0])
              this.throwError(err.response?.data?.errors?.Password[0]);
            // TODO add Type
            if (err.response?.data?.errors?.Password[0])
              this.throwError(err.response?.data?.errors?.Username[0]);

            if (typeof err.response?.data === 'string')
              this.throwError(err.response?.data);
          })
          .finally(() => {
            this.isLoading = false;
          });
      else
        axios
          .post('auth/register', {
            firstName: this.firstname,
            lastName: this.lastname,
            username: this.nickname,
            password: this.password,
          })
          .then((response) => {
            this.$emit('login', response.data.token);
          })
          .catch((err: any) => {
            // TODO add Type
            if (err.response?.data?.errors?.Password[0])
              this.throwError(err.response?.data?.errors?.Password[0]);
            // TODO add Type
            if (err.response?.data?.errors?.Password[0])
              this.throwError(err.response?.data?.errors?.Username[0]);

            if (typeof err.response?.data === 'string')
              this.throwError(err.response?.data);

            if (err.response?.data?.PasswordTooShort[0])
              this.throwError(err.response?.data?.PasswordTooShort[0]);
          })
          .finally(() => {
            this.isLoading = false;
          });
    },
    checkNickName(): void {
      if (!this.login && this.nickname !== '')
        axios
          .get(`user/checkuser/${this.nickname}`)
          .then((response) => {
            this.nickIsValid = response.data;
          })
          .catch((err: AxiosError) => {
            this.throwError(err.response?.data as string);
          });
    },
  },
  directives: {
    focus: {
      inserted: function (el) {
        window.setTimeout(() => {
          let childData = el.querySelectorAll('input')[0];
          childData.focus();
        }, 500);
      },
      update: function (el) {
        window.setTimeout(() => {
          let childData = el.querySelectorAll('input')[0];
          if ((childData as HTMLInputElement).value === '') childData.focus();
        }, 500);
      },
    },
  },
});
</script>
