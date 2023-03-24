<template>
  <div>
    <v-card v-for="(target, i) of targets" :key="i">
      <v-card-title> Ziel {{ i + 1 }} </v-card-title>
      <v-card-text class="my-1">
        <span class="ml-2"> Pfeile: {{ target.arrowCount }} </span> <br />
        <span class="ml-2"> Trefferfläche: {{ target.hitArea }} </span> <br />
        <span class="title mt-2 ml-1"> Punkte: {{ calcPunkte(target) }} </span>
      </v-card-text>
    </v-card>
    <v-btn fab large elevation="10" icon fixed bottom right>
      <v-icon>mdi-plus</v-icon>
    </v-btn>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import axios from "@/router/axios";

export default defineComponent({
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
      targets: [], // TODO add Type
    };
  },
  mounted() {
    axios
      .get("target/gettargets")
      .then((response) => {
        // TODO prüfen
        this.targets = response.data;
      })
      .catch((err) => console.log(err));
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
  },
});
</script>
