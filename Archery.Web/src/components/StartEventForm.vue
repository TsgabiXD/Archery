<template>
  <v-card elevation="7" :loading="isLoading" v-if="!isAddParcour">
    <v-card-title> Neues Parcour-Event </v-card-title>
    <v-card-text>
      <v-container class="grey lighten-5" rounded>
        <v-row dense>
          <v-col>
            <v-combobox
              label="Parcour"
              clearable
              outlined
              :items="parcours"
              item-text="name"
              v-model="selectedParcour"
              hint="Alle angmeldeten Benutzer können hier mitspielen."
            >
            </v-combobox>
          </v-col>
        </v-row>
      </v-container>
    </v-card-text>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="primary" elevation="2" @click="startEvent">
        Event Starten
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
      isAddParcour: false,
      parcours: [], // TODO add Type
      selectedParcour: {},
    };
  },
  mounted() {
    axios
      .get("parcour/getparcours")
      .then((response) => {
        // TODO prüfen
        this.parcours = response.data;
      })
      .catch((err) => console.log(err));
  },
  methods:{
    startEvent(){
      axios
      .post("parcour/addparcour")
      .then((response) => {
        // TODO implement
        this.parcours = response.data;
      })
      .catch((err) => console.log(err));
    }
  }
});
</script>
