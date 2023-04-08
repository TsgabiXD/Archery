<template>
  <div class="d-flex justify-center">
    <line-chart
      :data="chartData"
      :options="chartOptions"
      v-if="loaded"
      :style="chartStyle"
    />
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
      return {
        labels: this.testdata.map((row) => row.year),
        datasets: [
          {
            label: "Acquisitions by year",
            data: this.testdata.map((row) => row.count),
          },
        ],
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
