<template>
  <div class="d-flex justify-center">
    <line-chart
      :data="chartData"
      :options="chartOptions"
      :style="chartStyle"
      aria-label="Grafik zur Veranschaulichung der Punkte"
    >
      <p>
        Hier sollte eigentlich eine Grafik zur Veranschaulichung der Punkte
        stehen.
      </p>
    </line-chart>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import {
  Chart as ChartJS,
  Title,
  Tooltip,
  Legend,
  PointElement,
  LineElement,
  CategoryScale,
  LinearScale,
  ChartData,
  ChartDataset,
} from 'chart.js';
import { Line as LineChart } from 'vue-chartjs';

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
    data: Array as PropType<
      {
        userName: string;
        targets: { id: number; arrowCount: number; hitArea: number }[];
      }[]
    >,
    bottomLabels: Array as PropType<number[] | string[]>,
  },
  components: {
    LineChart,
  },
  data() {
    return {
      countingResults: [
        [20, 18, 16],
        [14, 12, 10],
        [8, 6, 4],
      ],
    };
  },
  computed: {
    chartData(): ChartData {
      let dataSets = [] as ChartDataset[];

      this.data?.forEach(
        (d: {
          userName: string;
          targets: {
            id: number;
            arrowCount: number;
            hitArea: number;
          }[];
        }) => {
          dataSets.push({
            label: 'Pfeile',
            backgroundColor: '#aa7b24',
            borderColor: '#aa7b24',
            data: d.targets.map((t) => t.arrowCount),
          });
          dataSets.push({
            label: 'Genauigkeit',
            backgroundColor: '#84aa24',
            borderColor: '#84aa24',
            data: d.targets.map((t) => t.hitArea),
          });
          dataSets.push({
            label: 'Punkte',
            backgroundColor: '#8e24aa',
            borderColor: '#8e24aa',
            data: d.targets.map(
              (t) => this.countingResults[t.hitArea - 1][t.arrowCount - 1]
            ),
          });
        }
      );

      return {
        labels: this.bottomLabels,
        datasets: dataSets,
      };
    },
    chartOptions() {
      return { responsive: true };
    },
    chartStyle() {
      return {
        position: 'relative',
      };
    },
  },
});
</script>
