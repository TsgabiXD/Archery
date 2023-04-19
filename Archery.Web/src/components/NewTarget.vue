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
          <v-card class="mb-6 mx-3 mt-2 stepper-card" elevation="4">
            <v-card-title>Pfeil</v-card-title>
            <v-card-text>
              <v-radio-group v-model="arrowCount">
                <v-radio label="Pfeil 1" :value="1"></v-radio>
                <v-radio label="Pfeil 2" :value="2"></v-radio>
                <v-radio label="Pfeil 3" :value="3"></v-radio>
                <v-radio label="Verschossen" :value="0"></v-radio>
              </v-radio-group>
            </v-card-text>
            <v-card-actions class="stepper-card-actions">
              <v-btn
                color="error"
                class="mx-2 d-none d-sm-inline-flex"
                @click="close"
              >
                Abbrechen
              </v-btn>
              <v-btn color="error" class="mx-2 d-sm-none" @click="close">
                <v-icon>mdi-close</v-icon>
              </v-btn>
              <v-btn
                color="primary"
                class="d-none d-sm-inline-flex"
                @click="step = 2"
                :disabled="arrowCount === -1"
              >
                Weiter
              </v-btn>
              <v-btn
                color="primary"
                class="d-sm-none"
                @click="step = 2"
                :disabled="arrowCount === -1"
              >
                <v-icon>mdi-arrow-right-thin</v-icon>
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-stepper-content>
        <v-stepper-content step="2">
          <v-card class="mb-6 mx-3 mt-2 stepper-card" elevation="4">
            <v-card-title>Trefferfläche</v-card-title>
            <v-card-text>
              <div class="target-container">
                <v-btn
                  :disabled="arrowCount === 0"
                  elevation="3"
                  class="target"
                  :color="!(hitArea === 1 || hitArea === 2) ? 'blue' : 'white'"
                  @click.stop="hitArea === 3 ? (hitArea = -1) : (hitArea = 3)"
                >
                  <div class="target-mid-container">
                    <v-btn
                      :disabled="arrowCount === 0"
                      elevation="0"
                      class="target-mid d-block"
                      :color="
                        !(hitArea === 1 || hitArea === 3) ? 'red' : 'white'
                      "
                      @click.stop="
                        hitArea === 2 ? (hitArea = -1) : (hitArea = 2)
                      "
                    >
                      <div class="target-center-container">
                        <v-btn
                          :disabled="arrowCount === 0"
                          elevation="0"
                          class="target-center"
                          :color="
                            !(hitArea === 3 || hitArea === 2)
                              ? 'yellow'
                              : 'white'
                          "
                          @click.stop="
                            hitArea === 1 ? (hitArea = -1) : (hitArea = 1)
                          "
                        >
                        </v-btn>
                      </div>
                    </v-btn>
                  </div>
                </v-btn>
              </div>
            </v-card-text>
            <v-card-actions class="stepper-card-actions">
              <v-btn
                color="secondary"
                class="mx-2 d-none d-sm-inline-flex"
                @click="step = 1"
              >
                Zurück
              </v-btn>
              <v-btn color="secondary" class="mx-2 d-sm-none" @click="step = 1">
                <v-icon>mdi-arrow-left-thin</v-icon>
              </v-btn>
              <v-btn
                color="error"
                class="mx-2 d-none d-sm-inline-flex"
                @click="close"
              >
                Abbrechen
              </v-btn>
              <v-btn color="error" class="mx-2 d-sm-none" @click="close">
                <v-icon>mdi-close</v-icon>
              </v-btn>
              <v-btn
                color="primary"
                class="mx-2 d-none d-sm-inline-flex"
                @click="save"
                :disabled="!isSaveAble"
              >
                Speichern
              </v-btn>
              <v-btn
                color="primary"
                class="mx-2 d-sm-none"
                @click="save"
                :disabled="!isSaveAble"
              >
                <v-icon>mdi-content-save</v-icon>
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-stepper-content>
      </v-stepper-items>
    </v-stepper>
  </v-dialog>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import axios from '@/router/axios';
import { AxiosError } from 'axios';

export default defineComponent({
  props: {
    show: { type: Boolean, required: true },
    token: { type: String, required: true },
    eventId: { type: Number, required: true },
  },
  data() {
    return {
      step: 1,
      arrowCount: -1,
      hitArea: -1,
    };
  },
  methods: {
    save(): void {
      let newTarget = {
        arrowCount: this.arrowCount,
        hitArea: this.hitArea,
        eventId: this.eventId,
      };

      if (newTarget.arrowCount == 0) {
        newTarget.hitArea = 0;
      }

      axios
        .post('target/addtarget', newTarget, this.axiosAuthConfig)
        .then(() => {
          this.$emit('save');
          this.close();
        })
        .catch((err: any) => this.throwError(err.response.data));
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
    throwError(errorMessage: string): void {
      this.$emit('error', errorMessage);
    },
  },
  computed: {
    axiosAuthConfig(): object {
      return { headers: { Authorization: `Bearer ${this.token}` } };
    },
    isSaveAble(): boolean {
      if (this.arrowCount === 0) return true;
      return [0, 1, 2, 3].indexOf(this.hitArea) !== -1;
    },
  },
});
</script>

<style scoped>
.stepper-card {
  position: relative;
  max-height: 910px;
}

.stepper-card:after {
  content: '';
  display: block;
  padding-bottom: 50px;
}

.stepper-card-actions {
  position: absolute;
  bottom: 0;
  width: 100%;
  padding-bottom: 12px;
}

.target-container {
  position: relative;
  max-height: 745px;
  max-width: 745px;
}

.target-container:after {
  content: '';
  display: block;
  padding-bottom: 100%;
}

.target {
  border-radius: 100%;
  position: absolute;
  width: 100%;
  height: 100% !important;
}

.target-mid-container {
  position: relative;
  width: 100%;
  height: 100%;
}

.target-mid-container:after {
  content: '';
  display: block;
  padding-bottom: 100%;
}

.target-mid {
  border-radius: 100%;
  position: absolute;
  top: calc(25% / 2);
  left: calc(25% / 2);
  width: 75%;
  height: 75% !important;
}

.target-center-container {
  position: relative;
  width: 100%;
  height: 100%;
}

.target-center-container:after {
  content: '';
  display: block;
  padding-bottom: 100%;
}

.target-center {
  border-radius: 100%;
  position: absolute;
  top: calc(30% / 2);
  left: calc(30% / 2);
  width: 70%;
  height: 70% !important;
}
</style>
