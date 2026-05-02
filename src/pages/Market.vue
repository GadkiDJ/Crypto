<script setup lang="ts">
import MarketChart from "../components/analytics/MarketChart.vue";
</script>

<template>
  <div class="market-page">
    <h1>Market</h1>

    <p class="subtitle">
      Live crypto market data
    </p>

    <MarketChart />
  </div>
</template>

<style scoped>
.market-page {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.subtitle {
  color: var(--muted);
}
</style>