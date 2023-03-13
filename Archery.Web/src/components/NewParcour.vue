<template>
  <v-card elevation="7" :loading="isLoading">
    <v-card-title> Neuer Parcour </v-card-title>
    <v-card-text>
      <v-container class="grey lighten-5" rounded>
        <v-row dense>
          <v-col cols="12" md="6">
            <v-text-field label="Parcourname" outlined v-model="parcourName">
            </v-text-field>
          </v-col>
          <v-col cols="12" md="6">
            <v-text-field label="Ort" outlined v-model="location">
            </v-text-field>
          </v-col>
        </v-row>
        <v-row dense>
          <v-col>
            <v-text-field
              label="Anzahl der 3D-Tiere"
              outlined
              v-model="animalCount"
            >
            </v-text-field>
          </v-col>
        </v-row>
      </v-container>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="primary" elevation="2" @click="addParcour">
        Hinzufügen
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
      isLoading: false,
      parcourName: "",
      location: "",
      animalCount: 0,
    };
  },
  computed: {
    isLogin(): boolean {
      return true;
    },
  },
  methods: {
    addParcour(): void {
      axios
        .post("parcour/addparcour")
        .then((response) => {
          // TODO implement
          response.data;
        })
        .catch((err) => console.log(err));
    },
  },
});
</script>
