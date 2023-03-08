<template>
  <div>
    <!-- <v-card v-for="(tier, i) of tiere" key="i">
      <v-card-title> Tier {{ i }} </v-card-title>
      <v-card-text> </v-card-text>
    </v-card> -->
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import axios from "@/router/axios";

export default defineComponent({
  data: () => {
    return {
      tiere: [],
    };
  },
  mounted() {
    axios
      .get('story/gettiere')
      .then((response)=>{
        // TODO prüfen
        this.tiere = response.data;
      })
      .catch((err) => console.log(err));
  },
});
</script>
