<template>
  <div>
    <v-card elevation="7" :loading="isLoading" v-if="!isAddParcour">
      <v-card-title> Neues Parcour-Event </v-card-title>
      <v-card-text>
        <v-container class="grey lighten-5" rounded>
          <v-row dense>
            <v-col cols="12" md="6">
              <v-text-field label="Parcourname" outlined v-model="eventName">
              </v-text-field>
            </v-col>
            <v-col cols="12" md="6">
              <v-combobox
                label="Parcour"
                clearable
                outlined
                :items="parcours"
                item-text="name"
                v-model="selectedParcour"
                hint="Alle angmeldeten Benutzer können hier mitspielen."
                persistent-hint
              >
                <template v-slot:prepend-item>
                  <v-btn tile depressed style="width: 100%" @click="addParcour">
                    +
                  </v-btn>
                </template>
              </v-combobox>
            </v-col>
          </v-row>
          <v-row dense>
            <v-col>
              <v-simple-table dense>
                <tbody>
                  <tr v-for="user in users" :key="user.name">
                    <td>{{ user.nickName }}</td>
                    <td class="d-flex">
                      <v-spacer></v-spacer>
                      <v-switch
                        v-model="eventUser"
                        color="success"
                        :value="user"
                        dense
                        class="ma-0"
                      >
                      </v-switch>
                    </td>
                  </tr>
                </tbody>
              </v-simple-table>
            </v-col>
          </v-row>
        </v-container>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" elevation="2" @click="startEvent">
          Event Starten
        </v-btn>
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
    <new-parcour v-if="isAddParcour" @parcour-added="isAddParcour = false" />
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import NewParcour from "./NewParcour.vue";
import axios from "@/router/axios";

export default defineComponent({
  components: {
    NewParcour,
  },
  data: () => {
    return {
      isLoading: false,
      isAddParcour: false,
      parcours: [], // TODO add Type
      users: [], // TODO add Type
      selectedParcour: "" as string, // TODO add Type
      eventName: "",
      eventUser: [], // TODO add Type
    };
  },
  mounted() {
    this.getParcours();

    axios
      .get("user/getusers")
      .then((response) => {
        // TODO prüfen
        this.users = response.data;
        this.eventUser = response.data;
      })
      .catch((err) => console.log(err));
  },
  methods: {
    startEvent(): void {
      this.isLoading = true;

      axios
        .post("event/startevent", {
          name: this.eventName,
          parcour: this.selectedParcour,
          user: this.eventUser,
          isRunning: true,
        }) // TODO add Type
        .then(() => {
          // TODO implement
        })
        .catch((err) => console.log(err))
        .finally(() => {
          this.isLoading = false;
        });
    },
    addParcour(): void {
      this.isAddParcour = true;
    },
    getParcours(): void {
      axios
        .get("parcour/getparcours")
        .then((response) => {
          // TODO prüfen
          this.parcours = response.data;
        })
        .catch((err) => console.log(err));
    },
  },
  watch: {
    isAddParcour(newValue: boolean) {
      if (newValue !== true) this.getParcours();
    },
  },
});
</script>
