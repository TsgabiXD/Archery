import Vue from 'vue';
import axios from '@/router/axios';
import { AxiosStatic } from 'axios';

Vue.use({
  install() {
    Vue.prototype.axios = axios;
  },
});

declare module 'vue/types/vue' {
  interface Vue {
    axios: AxiosStatic;
  }
}