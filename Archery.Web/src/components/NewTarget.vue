<template>
  <v-dialog :value="show" fullscreen transition="dialog-bottom-transition">
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
            <v-card-text>
              <v-radio-group v-model="arrowCount">
                <v-radio label="Pfeil 1" value="1"></v-radio>
                <v-radio label="Pfeil 2" value="2"></v-radio>
                <v-radio label="Pfeil 3" value="3"></v-radio>
                <v-radio label="Verschossen" value="0"></v-radio>
              </v-radio-group>
            </v-card-text>
          </v-card>
          <v-btn color="error" class="mx-2" @click="close"> Abbrechen </v-btn>
          <v-btn
            color="primary"
            @click="step = 2"
            :disabled="arrowCount === -1"
          >
            Weiter
          </v-btn>
        </v-stepper-content>
        <v-stepper-content step="2">
          <v-card class="mb-6 mx-3 mt-2" height="65vh" elevation="4">
            <v-card-title>Trefferfläche</v-card-title>
            <v-card-text>
              <v-container style="height: 100%">
                <v-row>
                  <v-col>
                    <v-btn
                      elevation="2"
                      outlined
                      rounded
                      x-large
                      style="border-radius: 100%"
                    >
                    </v-btn>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>
          </v-card>
          <v-btn color="secondary" class="mx-2" @click="step = 1">
            Zurück
          </v-btn>
          <v-btn color="error" class="mx-2" @click="close"> Abbrechen </v-btn>
          <v-btn color="primary" class="mx-2" @click="save"> Speichern </v-btn>
        </v-stepper-content>
      </v-stepper-items>
    </v-stepper>
  </v-dialog>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import axios from '@/router/axios';

export default defineComponent({
  props: {
    show: { type: Boolean, required: true },
    token: { type: String, required: true },
    eventId: { type: Number, required: true },
  },
  data() {
    return {
      count: 0,
      step: 1,
      arrowCount: -1,
      hitArea: -1,
    };
  },
  methods: {
    save(): void {
      axios
        .post(
          'target/addtarget',
          {
            arrowCount: this.arrowCount,
            hitArea: this.hitArea,
            eventId: this.eventId,
          },
          this.axiosAuthConfig
        )
        .then(() => {
          this.$emit('save');
          this.close();
        })
        .catch((err) => console.log(err));
    },
    close(): void {
      this.clearData();
      this.$emit('close');
    },
    clearData(): void {
      this.step = 1;
      this.arrowCount = -1;
      this.hitArea = -1;
    },
  },
  computed: {
    axiosAuthConfig(): object {
      return { headers: { Authorization: `Bearer ${this.token}` } };
    },
  },
});
</script>
