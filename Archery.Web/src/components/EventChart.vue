<template>
  <div class="d-flex justify-center pa-2">
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
    user: Array as PropType<
      {
        nickName: string;
        targets: { id: number; arrowCount: number; hitArea: number }[];
      }[]
    >,
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
      delayed: false,
    };
  },
  computed: {
    chartData(): ChartData {
      let dataSets = [] as ChartDataset[];

      this.user?.forEach((u) => {
        let data = u.targets.map((t) => {
          if (t) {
            if (t.arrowCount !== 0 && t.hitArea !== 0)
              return this.countingResults[t.arrowCount - 1][t.hitArea - 1];
            else return 0;
          } else return t;
        });

        data.forEach((d, i) => {
          if (i - 1 >= 0 && d >= 0) data[i] = d + data[i - 1];
        });

        dataSets.push({
          label: u.nickName,
          backgroundColor: '',
          borderColor: '',
          data: data,
        });
      });

      dataSets.forEach((s) => {
        let randColorString = Math.floor(Math.random() * 16777215).toString(16);
        s.backgroundColor = '#' + randColorString;
        s.borderColor = '#' + randColorString;
      });

      let bottomLabels = [] as number[];

      this.user?.forEach((e) => {
        if (bottomLabels.length < e.targets.length)
          for (let i = 0; i < e.targets.length; i++) bottomLabels.push(i + 1);
      });

      return {
        labels: bottomLabels,
        datasets: dataSets,
      };
    },
    chartOptions() {
      return {
        responsive: true,
        animation: {
          onComplete: () => {
            this.delayed = true;
          },
          // TODO add type
          // eslint-disable-next-line @typescript-eslint/no-explicit-any
          delay: (context: any) => {
            let delay = 0;
            if (
              context.type === 'data' &&
              context.mode === 'default' &&
              !this.delayed
            ) {
              delay = context.dataIndex * 300 + context.datasetIndex * 100;
            }
            return delay;
          },
        },
      };
    },
    chartStyle() {
      return {
        position: 'relative',
      };
    },
  },
});
</script>
