<template>
  <div class="d-flex justify-center">
    <line-chart
      :data="chartData"
      :options="chartOptions"
      v-if="loaded"
      :style="chartStyle"
      aria-label="Grafik zur Veranschaulichung der Punkte"
    >
      <p>
        Hier sollte eigentlich eine Grafik zur Veranschaulichung der Punkte
        stehen.
      </p>
    </line-chart>
    <!-- TODO v-btn ersetzen -->
    <v-btn disabled loading text v-else> </v-btn>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  PointElement,
  LineElement,
  CategoryScale,
  LinearScale,
} from "chart.js";
import { Line as LineChart } from "vue-chartjs";

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend
);

export default defineComponent({
  props: {
    // TODO add Type
    user: { type: Array, default: [] as { nickName: string; score: number }[] },
  },
  components: {
    LineChart,
  },
  data() {
    return {
      testdata: [
        { year: 2010, count: 10 },
        { year: 2011, count: 20 },
        { year: 2012, count: 15 },
        { year: 2013, count: 25 },
        { year: 2014, count: 22 },
        { year: 2015, count: 30 },
        { year: 2016, count: 28 },
      ],
      loaded: true,
    };
  },
  computed: {
    chartData() {
      let dataSets = [
        {
          label: "User 1",
          borderColor: "#36A2EB",
          backgroundColor: "#9BD0F5",
          data: this.testdata.map((row) => row.count),
        },
      ];

      dataSets.forEach((s) => {
        let randColorString = Math.floor(Math.random() * 16777215).toString(16);
        s.backgroundColor = "#" + randColorString;
        s.borderColor = "#" + randColorString;
      });

      return {
        labels: this.testdata.map((row) => row.year),
        datasets: dataSets,
      };
    },
    chartOptions() {
      return { responsive: true };
    },
    chartStyle() {
      return {
        position: "relative",
      };
    },
  },
});
</script>
