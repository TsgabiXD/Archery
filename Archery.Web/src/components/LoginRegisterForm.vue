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
            <v-text-field label="Vorname" outlined v-model="firstname"> </v-text-field>
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field label="Nachname" outlined v-model="lastname"> </v-text-field>
          </v-col>
        </v-row>
        <v-row dense>
          <v-col>
            <v-text-field label="Nickname" outlined v-model="nickname"> </v-text-field>
          </v-col>
        </v-row>
      </v-container>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="primary" elevation="2">
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
    switchState():void {
      this.isLogin = !this.isLogin;
    },
    registerLogin():void{
      if(this.isLogin)
      axios
        .post("user/getusers")
        .
    }
  },
});
</script>
