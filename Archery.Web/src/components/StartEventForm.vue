<template>
  <div>
    <v-card
      elevation="7"
      :loading="isLoading"
      v-if="!isAddParcour"
      class="mb-5"
      v-focus
    >
      <v-card-title> Neues Event </v-card-title>
      <v-card-text>
        <v-container class="grey lighten-5" rounded>
          <v-row dense>
            <v-col cols="12" md="6">
              <v-text-field
                label="Eventname"
                outlined
                v-model="eventName"
                @keypress.native.enter="startEvent"
              >
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
    <new-parcour
      v-if="isAddParcour"
      :token="token"
      @parcour-added="isAddParcour = false"
      @canceled="isAddParcour = false"
    />
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
  props: {
    token: { type: String, required: true },
  },
  data: () => {
    return {
      isLoading: false,
      isAddParcour: false,
      parcours: [], // TODO add Type
      users: [], // TODO add Type
      selectedParcour: "" as
        | string
        | { animalNumber: number; id: number; location: string; name: string }, // TODO add Type
      eventName: "",
      eventUser: [], // TODO add Type
    };
  },
  mounted() {
    this.getParcours();

    axios
      .get("user/getusers", this.axiosAuthConfig)
      .then((response) => {
        // TODO prüfen
        this.users = response.data;

        response.data.forEach((e: never) => {
          // TODO add Type
          // TODO implementieren if(e.EventId)
          this.eventUser.push(e);
        });
      })
      .catch((err) => console.log(err));
  },
  methods: {
    startEvent(): void {
      this.isLoading = true;

      if (typeof this.selectedParcour === "object")
        // TODO add Type
        axios
          .post(
            "event/startevent",
            {
              name: this.eventName,
              parcourId: this.selectedParcour.id,
              userIds: this.eventUserIds,
            },
            this.axiosAuthConfig
          ) // TODO add Type
          .then((response) => {
            this.$emit("new-event", response.data);

            this.selectedParcour = "";
            this.eventUser = [];
            this.eventName = "";
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
        .get("parcour/getparcours", this.axiosAuthConfig)
        .then((response) => {
          // TODO prüfen
          this.parcours = response.data;
        })
        .catch((err) => console.log(err));
    },
  },
  computed: {
    axiosAuthConfig(): object {
      return { headers: { Authorization: `Bearer ${this.token}` } };
    },
    eventUserIds(): number[] {
      let result = [] as number[];

      this.eventUser.forEach(
        (e: {
          animalNumber: number;
          id: number;
          location: string;
          name: string;
        }) => result.push(e.id)
      );

      return result;
    },
  },
  watch: {
    isAddParcour(newValue: boolean) {
      if (newValue !== true) this.getParcours();
    },
  },
  directives: {
    focus: {
      inserted: function (el) {
        window.setTimeout(() => {
          let childData = el.querySelectorAll("input")[0];
          childData.focus();
        }, 500);
      },
      update: function (el) {
        window.setTimeout(() => {
          let childData = el.querySelectorAll("input")[0];
          if ((childData as HTMLInputElement).value === "") childData.focus();
        }, 500);
      },
    },
  },
});
</script>
