<template>
  <v-dialog
    :value="show"
    fullscreen
    hide-overlay
    transition="dialog-bottom-transition"
  >
    <v-stepper v-model="step" flat>
      <v-stepper-header>
        <v-spacer></v-spacer>
        <v-stepper-step :complete="step > 1" step="1"> </v-stepper-step>
        <v-stepper-step :complete="step > 2" step="2"> </v-stepper-step>
        <v-spacer></v-spacer>
      </v-stepper-header>
      <v-stepper-items>
        <v-stepper-content step="1">
          <v-card class="mb-6 mx-3 mt-2" height="65vh" elevation="4">
            <v-card-title>Pfeil</v-card-title>
          </v-card>
          <v-btn color="error" class="mx-2" @click="cancel"> Abbrechen </v-btn>
          <v-btn color="primary" @click="step = 2"> Weiter </v-btn>
        </v-stepper-content>
        <v-stepper-content step="2">
          <v-card class="mb-6 mx-3 mt-2" height="65vh" elevation="4">
            <v-card-title>Trefferfläche</v-card-title>
          </v-card>
          <v-btn color="secondary" class="mx-2" @click="step = 1">
            Zurück
          </v-btn>
          <v-btn color="error" class="mx-2" @click="cancel"> Abbrechen </v-btn>
          <v-btn color="primary" class="mx-2" @click="save"> Speichern </v-btn>
        </v-stepper-content>
      </v-stepper-items>
    </v-stepper>
  </v-dialog>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import axios from "@/router/axios";

export default defineComponent({
  props: {
    show: { type: Boolean, required: true },
    token: { type: String, required: true },
  },
  data() {
    return {
      step: 1,
      target: {
        arrowCount: 0,
        hitArea: 0,
        eventId: 0,
        userId: 0,
      },
    };
  },
  methods: {
    save(): void {
      axios
        .post("target/addtarget", this.target, this.axiosAuthConfig)
        .then(() => {
          this.clearData();
          this.$emit("save");
        })
        .catch((err) => console.log(err));
    },
    cancel(): void {
      this.clearData();
      this.$emit("cancel");
    },
    clearData(): void {
      this.step = 1;
    },
  },
  computed: {
    axiosAuthConfig(): object {
      return { headers: { Authorization: `Bearer ${this.token}` } };
    },
  },
});
</script>
