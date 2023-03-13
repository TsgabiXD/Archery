<template>
  <div>
    <v-card elevation="7" :loading="isLoading" v-if="!isAddParcour">
      <v-card-title> Neues Parcour-Event </v-card-title>
      <v-card-text>
        <v-container class="grey lighten-5" rounded>
          <v-row dense>
            <v-col cols="12" md="6">
              <v-text-field label="Parcourname" outlined v-model="eventName">
              </v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-combobox
                label="Parcour"
                clearable
                outlined
                :items="parcours"
                item-text="name"
                v-model="selectedParcour"
                hint="Alle angmeldeten Benutzer können hier mitspielen."
                persistent-hint
              >
                <template v-slot:prepend-item>
                  <v-btn tile depressed style="width: 100%" @click="addParcour"
                    >+</v-btn
                  >
                </template>
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
    <new-parcour v-if="isAddParcour" @parcour-added="isAddParcour = false" />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import NewParcour from "./NewParcour.vue";
import axios from "@/router/axios";

export default defineComponent({
  components: {
    NewParcour,
  },
  data: () => {
    return {
      isLoading: false,
      isAddParcour: false,
      parcours: [], // TODO add Type
      selectedParcour: "" as string, // TODO add Type
      eventName: "",
    };
  },
  mounted() {
    this.getParcours();
  },
  methods: {
    startEvent(): void {
      axios
        .post("event/startevent")
        .then((response) => {
          // TODO implement
          this.parcours = response.data;
        })
        .catch((err) => console.log(err));
    },
    addParcour(): void {
      this.isAddParcour = true;
    },
    getParcours(): void {
      axios
        .get("parcour/getparcours")
        .then((response) => {
          // TODO prüfen
          this.parcours = response.data;
        })
        .catch((err) => console.log(err));
    },
  },
  watch: {
    isAddParcour(newValue: boolean) {
      if (newValue !== true) this.getParcours();
    },
  },
});
</script>
