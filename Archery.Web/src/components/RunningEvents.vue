<template>
  <v-card v-if="newEventId > 1">
    <v-card-text>
      <v-container class="grey lighten-5" rounded>
        <v-card v-for="event in events" :key="event.id">
          <v-card-title>{{ event.name }}</v-card-title>
          <v-card-text>
            <v-card v-for="u in user.filter" flat :key="u.id"></v-card>
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
      events: [] as { id: number; name: string }[], // TODO add Type
      user: [] as { eventId: number; nickname: string }[], // TODO add Type
    };
  },
  watch: {
    newEventId() {
      axios
        .get("event/getrunningevents", this.axiosAuthConfig)
        .then((response) => {
          response.data.forEach((e: { id: number; name: string }) => {
            this.events.push({
              id: e.id,
              name: e.name,
            });
          });
        })
        .catch((err) => console.log(err))
        .finally(() => {
          this.events.forEach((e: { id: number; name: string }) => {
            axios
              .get(`user/getuserwithtargets/${e.id}`, this.axiosAuthConfig)
              .then((response) => {
                response.data.forEach((u: { nickname: string }) => {
                  // TODO add Type
                  this.user.push({ eventId: e.id, nickname: u.nickname });
                });
              })
              .catch((err) => console.log(err));
          });
        });
    },
  },
  computed: {
    axiosAuthConfig(): object {
      return { headers: { Authorization: `Bearer ${this.token}` } };
    },
  },
});
</script>
