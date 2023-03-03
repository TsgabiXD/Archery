<template>
  <v-card elevation="7" :loading="isLoading">
    <v-card-title>
      {{ isLogin ? "Login" : "Registrieren" }}
    </v-card-title>
    <v-card-actions>

    </v-card-actions>
    <v-card-text>

    </v-card-text>
  </v-card>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  data: () => {
    return {
      isLoading: false,
    };
  },
  computed:{
    isLogin(): boolean{
      return true;
    }
  }
});
</script>
