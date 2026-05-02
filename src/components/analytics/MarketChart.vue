<script setup lang="ts">
import { ref, onMounted, onUnmounted } from "vue";
import VueApexCharts from "vue3-apexcharts";
import { marketWs } from "../../services/marketWs";


type Candle = {
  time: number;
  open: number;
  close: number;
  high: number;
  low: number;
  volume: number;
};

type ChartPoint = {
  x: number;
  y: [number, number, number, number];
};

const symbol = "BTCUSDT";
const interval = "OneMinute";

const candles = ref<ChartPoint[]>([]);
const series = ref([{data: [] as ChartPoint[]}]);
const chartOptions ={
  chart: {
    type: "candlestick" as const,
    animations: {
      enabled: false,
    },
  },
  xaxis: {
    type: "datetime" as const,
  },
  yaxis:{
    tooltip: {
      enabled: true,
    },
  },
};

function upadateSeries(){
  series.value = [{
    data: [...candles.value],
  },];
}

function merge(c: Candle){
  const last =  candles.value[candles.value.length - 1];

  if (!last || c.time > last.x){
    candles.value.push({
      x: c.time,
      y: [c.open, c.high, c.low, c.close], 
    });

    if (candles.value.length > 200){
      candles.value.shift();
    }
    
  }else{
    last.y = [
      last.y[0],
      Math.max(last.y[1], c.high),
      Math.min(last.y[2], c.low),
      c.close,
    ];
  }

  candles.value = [...candles.value];
  upadateSeries();
}

onMounted(async () => {
  await marketWs.connect();
  marketWs.subscribe(symbol, interval, (candle: Candle)=>{
    merge(candle);
  });
});
onUnmounted(async()=>{
  marketWs.unsubscribe(symbol, interval);
});
</script>

<template>
  <div class="chart-container">
    <VueApexCharts
      type="candlestick"
      height="400"
      :options="chartOptions"
      :series="series"
    />
  </div>
</template>
<style scoped>
.chart-container{
  width: 100%;
}
</style>