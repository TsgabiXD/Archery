<template>
  <div>
    <div v-if="!isUserInEvent">
      <v-card>
        <v-card-title class="display-1 ma-12">
          Zur Zeit in keinem Event eingetragen
        </v-card-title>
      </v-card>
    </div>
    <div v-else>
      <v-card v-for="(target, i) of targets" :key="i">
        <v-card-title> Ziel {{ i + 1 }} </v-card-title>
        <v-card-text class="my-1">
          <span class="ml-2"> Pfeile: {{ target.arrowCount }} </span> <br />
          <span class="ml-2"> Trefferfläche: {{ target.hitArea }} </span> <br />
          <span class="title mt-2 ml-1">
            Punkte: {{ calcPunkte(target) }}
          </span>
        </v-card-text>
      </v-card>
      <v-btn
        fab
        large
        elevation="10"
        icon
        fixed
        bottom
        right
        @click="addingTarget = !addingTarget"
        v-if="!addingTarget"
      >
        <v-icon>mdi-plus</v-icon>
      </v-btn>
      <new-target
        :show="addingTarget"
        @save="loadTargets"
        @cancel="hideNewTarget"
        :token="token"
      >
      </new-target>
    </div>
    <error-message> </error-message>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import axios from "@/router/axios";
import NewTarget from "@/components/NewTarget.vue";
import ErrorMessage from "@/components/ErrorMessage.vue";
import { AxiosError } from "axios";

export default defineComponent({
  components: {
    NewTarget,
    ErrorMessage,
  },
  props: {
    token: { type: String, required: true },
    userId: { type: Number, required: true },
  },
  data: () => {
    return {
      countingResults: [
        [20, 18, 16],
        [14, 12, 10],
        [8, 6, 4],
      ],
      targets: [], // TODO add Type
      addingTarget: false,
      isUserInEvent: false,
      events: [] as number[],
      checkIntervalId: 0,
      errorMessages:[] as string []
    };
  },
  mounted() {
    this.checkIntervalId = setInterval(() => {
      if (this.userId && this.events.length === 0) {
        this.loadTargets();
        this.checkUserInEvent();
      }
    }, 10000);
  },
  beforeUnmount() {
    clearInterval(this.checkIntervalId); // TODO fix this
  },
  computed: {
    axiosAuthConfig(): object {
      return { headers: { Authorization: `Bearer ${this.token}` } };
    },
  },
  methods: {
    calcPunkte(target: any): number {
      // TODO add Type
      if (
        target.arrowCount - 1 < 0 ||
        target.arrowCount - 1 > 2 ||
        target.hitArea - 1 < 0 ||
        target.hitArea - 1 > 2
      )
        return 0;
      return this.countingResults[target.arrowCount - 1][target.hitArea - 1];
    },
    hideNewTarget(): void {
      this.addingTarget = false;
    },
    loadTargets(): void {
      this.hideNewTarget();

      axios
        .get("target/gettargets", this.axiosAuthConfig)
        .then((response) => {
          // TODO prüfen
          this.targets = response.data;
        })
        .catch((err: AxiosError ) => this.errorMessages.push(err.response?.data as string));
    },
    checkUserInEvent(): void {
      if (this.userId !== -1)
        axios
          .get(
            `user/getusersrunningevents/${this.userId}`,
            this.axiosAuthConfig
          )
          .then((response) => {
            this.isUserInEvent = response.data.length !== 0;
            if (this.isUserInEvent) this.events = response.data;
          })
          .catch((err: AxiosError ) => this.errorMessages.push(err.response?.data as string));
    },
  },
});
</script>
