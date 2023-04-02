<template>
  <v-card v-if="newEventId > 0">
    <v-card-title>Laufende Events</v-card-title>
    <v-card-text>
      <v-container class="grey lighten-5" rounded>
        <v-card v-for="event in events" :key="event.id" class="mb-2">
          <v-card-title>{{ event.name }}</v-card-title>
          <v-card-text>
            <v-card v-for="u in event.user" flat :key="u.nickName">
              <v-card-title class="subtitle-1">{{ u.nickName }}</v-card-title>
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
    newEventId: { Type: Number, required: true },
  },
  data() {
    return {
      events: [] as {
        name: string;
        user: { nickName: string; score: number }[];
      }[], // TODO add Type
      checkIntervalId: 0,
    };
  },
  mounted() {
    this.checkIntervalId = setInterval(() => {
      if (this.events.length > 0) {
        this.getAdminViewElements();
      }
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
              eventName: string;
              user: { nickName: string; score: number }[];
            }) => {
              this.events.push({
                name: e.eventName,
                user: e.user,
              });
            }
          );
        })
        .catch((err) => console.log(err));
    },
  },
  watch: {
    newEventId() {
      this.getAdminViewElements();
    },
  },
  computed: {
    axiosAuthConfig(): object {
      return { headers: { Authorization: `Bearer ${this.token}` } };
    },
  },
});
</script>
