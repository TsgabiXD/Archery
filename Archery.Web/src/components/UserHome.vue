<template>
  <div>
    <div v-if="events.length === 0">
      <v-card>
        <v-card-title class="subtitle-1 ma-12">
          Zur Zeit spielst du in keinem Event!
        </v-card-title>
      </v-card>
    </div>
    <div v-else>
      <v-row no-gutters>
        <v-col cols="12" :md="targets.length > 1 ? 6 : 12">
          <v-card v-for="(target, i) of targets" :key="i" class="ma-1">
            <v-card-title> Ziel {{ i + 1 }} </v-card-title>
            <v-card-text class="my-1">
              <span class="ml-2"> Pfeile: {{ target.arrowCount }} </span> <br />
              <span class="ml-2"> Trefferfläche: {{ target.hitArea }} </span>
              <br />
              <span class="title mt-2 ml-1">
                Punkte: {{ calcPunkte(target) }}
              </span>
            </v-card-text>
          </v-card>
        </v-col>
        <v-col cols="12" md="6" v-if="targets.length > 1">
          <v-card class="ma-1">
            <v-card-text>
              <user-chart :data="eventChartData" :bottomLabels="bottomLabels">
              </user-chart>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
      <v-btn
        fab
        large
        elevation="10"
        fixed
        bottom
        right
        @click="addingTarget = !addingTarget"
        v-if="!addingTarget && !isEventFinished"
      >
        <v-icon>mdi-plus</v-icon>
      </v-btn>
      <new-target
        :show="addingTarget"
        :token="token"
        :eventId="events[0]"
        @save="loadTargets"
        @close="hideNewTarget"
        @error="throwError"
      >
      </new-target>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import axios from '@/router/axios';
import { AxiosError } from 'axios';

import NewTarget from '@/components/NewTarget.vue';
import UserChart from '@/components/UserChart.vue';

export default defineComponent({
  components: {
    NewTarget,
    UserChart,
  },
  props: {
    token: { type: String, required: true },
  },
  data: () => {
    return {
      countingResults: [
        [20, 18, 16],
        [14, 12, 10],
        [8, 6, 4],
      ],
      targets: [] as { id: number; arrowCount: number; hitArea: number }[], // TODO add Type
      addingTarget: false,
      events: [] as number[],
      checkIntervalId: 0,
      isEventFinished: false,
    };
  },
  mounted() {
    this.checkUserInEvent();
    this.loadTargets();

    if (this.tokenData.userId && this.events.length === 0)
      this.checkIntervalId = setInterval(() => {
        if (this.tokenData.userId && this.events.length === 0) {
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
    tokenData() {
      if (this.token === '') return {};

      var base64Url = this.token.split('.')[1];
      var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
      var jsonPayload = decodeURIComponent(
        window
          .atob(base64)
          .split('')
          .map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
          })
          .join('')
      );

      let result = JSON.parse(jsonPayload);

      result.username =
        result['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];

      return result;
    },
    eventChartData(): {
      userName: string;
      targets: { id: number; arrowCount: number; hitArea: number }[];
    }[] {
      let result = [];

      if (!this.tokenData.username) return [];

      result.push({
        userName: this.tokenData.username,
        targets: this.targets,
      });

      return result;
    },
    bottomLabels(): number[] {
      let result = [] as number[];
      this.targets.forEach((_e, i) => {
        result.push(i + 1);
      });
      return result;
    },
  },
  methods: {
    // TODO remove
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
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
      axios
        .get('target/getmytargets', this.axiosAuthConfig)
        .then((response) => {
          this.targets = [];

          if (
            (
              response.data as {
                id: number;
                arrowCount: number;
                hitArea: number;
              }[]
            ).findIndex((d) => d === null) === -1
          )
            this.isEventFinished = true;

          response.data.forEach(
            (t: { id: number; arrowCount: number; hitArea: number }) => {
              if (t) this.targets.push(t);
            }
          );
        })
        .catch((err: AxiosError) =>
          this.throwError(err.response?.data as string)
        );
    },
    checkUserInEvent(): void {
      axios
        .get('user/getusersrunningevents', this.axiosAuthConfig)
        .then((response) => {
          if (response.data.length !== 0) this.events = response.data;
        })
        .catch((err: AxiosError) =>
          this.throwError(err.response?.data as string)
        );
    },
    throwError(errorMessage: string): void {
      this.$emit('error', errorMessage);
    },
  },
});
</script>
