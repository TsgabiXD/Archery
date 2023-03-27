<template>
  <v-dialog
    :value="show"
    fullscreen
    hide-overlay
    transition="dialog-bottom-transition"
  >
    <v-stepper v-model="step">
      <v-stepper-header>
        <v-stepper-step :complete="step > 1" step="1">
          Name of step 1
        </v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step :complete="step > 2" step="2">
          Name of step 2
        </v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="3"> Name of step 3 </v-stepper-step>
      </v-stepper-header>
      <v-stepper-items>
        <v-stepper-content step="1">
          <v-card class="mb-12" color="grey lighten-1" height="200px"></v-card>
          <v-btn color="primary" @click="step = 2"> Weiter </v-btn>
          <v-btn text @click="cancel"> Abbrechen </v-btn>
        </v-stepper-content>
        <v-stepper-content step="2">
          <v-card class="mb-12" color="grey lighten-1" height="200px"></v-card>
          <v-btn color="primary" @click="step = 3"> Weiter </v-btn>
          <v-btn text @click="cancel"> Abbrechen </v-btn>
        </v-stepper-content>
        <v-stepper-content step="3">
          <v-card class="mb-12" color="grey lighten-1" height="200px"></v-card>
          <v-btn color="primary"> Speichern </v-btn>
          <v-btn text @click="cancel"> Abbrechen </v-btn>
        </v-stepper-content>
      </v-stepper-items>
    </v-stepper>
  </v-dialog>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  props: {
    show: { type: Boolean, required: true },
  },
  data() {
    return {
      step: 1,
    };
  },
  methods: {
    cancel() {
      this.$emit("cancel");
    },
  },
});
</script>
