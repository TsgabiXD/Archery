<template>
  <v-card v-if="lastNewEventId > 0 || events.length !== 0">
    <v-card-title>Laufende Events</v-card-title>
    <v-card-text>
      <v-container class="grey lighten-5" rounded>
        <v-card v-for="event in events" :key="event.id" class="mb-2">
          <v-card-title>
            {{ event.name }}
            <v-spacer></v-spacer>
            <v-chip
              class="ma-2"
              close
              color="red"
              @click="endEvent(event.id)"
              outlined
              small
            >
              Event Beenden
            </v-chip>
          </v-card-title>
          <v-card-text>
            <v-card
              v-for="u in event.user"
              flat
              :key="u.nickName"
              outlined
              class="mt-1"
            >
              <v-card-title class="subtitle-1">
                {{ u.nickName }}
              </v-card-title>
              <v-card-text>{{ u.score }}</v-card-text>
            </v-card>
          </v-card-text>
        </v-card>
      </v-container>
    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import axios from "@/router/axios";

export default defineComponent({
  props: {
    token: { type: String, required: true },
    lastNewEventId: { Type: Number, required: true },
  },
  data() {
    return {
      events: [] as {
        id: number;
        name: string;
        user: { nickName: string; score: number }[];
      }[], // TODO add Type
      checkIntervalId: 0,
    };
  },
  mounted() {
    this.getAdminViewElements();

    if (this.events.length === 0)
      this.checkIntervalId = setInterval(() => {
        this.getAdminViewElements();
      }, 10000);
  },
  beforeUnmount() {
    clearInterval(this.checkIntervalId); // TODO fix this
  },
  methods: {
    getAdminViewElements(): void {
      axios
        .get("event/getadminviewelements", this.axiosAuthConfig)
        .then((response) => {
          this.events = [];

          response.data.forEach(
            (e: {
              id: number;
              eventName: string;
              user: { nickName: string; score: number }[];
            }) => {
              this.events.push({
                id: e.id,
                name: e.eventName,
                user: e.user,
              });
            }
          );
        })
        .catch((err) => console.log(err));
    },
    endEvent(event: number): void {
      axios
        .post(
          // TODO data richtig mitgeben
          `event/endevent?stopEvent=${event}`,
          undefined,
          this.axiosAuthConfig
        )
        .then((response) => {
          this.getAdminViewElements();
          this.$emit("ended-event", response.data);
        })
        .catch((err) => console.log(err));
    },
  },
  watch: {
    lastNewEventId(newVal: number) {
      this.getAdminViewElements();

      if (newVal <= 0) clearInterval(this.checkIntervalId);
    },
  },
  computed: {
    axiosAuthConfig(): object {
      return { headers: { Authorization: `Bearer ${this.token}` } };
    },
  },
});
</script>
