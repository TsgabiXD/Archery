<template>
  <v-card v-if="newEventId > 1">
    <v-card-text>
      <v-container class="grey lighten-5" rounded>
        <v-card
          v-for="event in events"
          :key="event.id"
          :title="event.name"
          class="mb-2"
        >
          <v-card-text>
            <v-card v-for="u in user" flat :key="'u' + u.id">
              <v-card-title>{{ u.nickname }}</v-card-title>
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
        user: { nickName: string; score: number };
      }[], // TODO add Type
    };
  },
  watch: {
    newEventId() {
      axios
        .get("event/getadminviewelements", this.axiosAuthConfig)
        .then((response) => {
          response.data.forEach(
            (e: {
              eventName: string;
              user: { nickName: string; score: number };
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
  computed: {
    axiosAuthConfig(): object {
      return { headers: { Authorization: `Bearer ${this.token}` } };
    },
  },
});
</script>
