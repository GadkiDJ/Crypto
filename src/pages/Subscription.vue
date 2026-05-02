<script setup lang="ts">
import { useAppStore } from "../stores/appStore";

const app = useAppStore();

const upgrade = (plan: "Pro" | "Expert") => {
  app.plan = plan;
};
</script>

<template>
  <div class="subscriptions">
    <h1 class="title">Upgrade access</h1>
    <div class="plans">
      <div class="card plan">
        <h2>Pro</h2>
        <p class="price">5$/month</p>
        <ul>
          <li>AI chat access</li>
          <li>Basic analytics</li>
        </ul>
        <button @click="upgrade('Pro')">Upgrade</button>

        <h2>Expert</h2>
        <p class="price">50$/month</p>
        <ul>
          <li>Highest priority queue</li>
          <li>AI chat access</li>
          <li>Automatic trading available</li>
        </ul>
        <button @click="upgrade('Expert')">Upgrade</button>
      </div>
    </div>
  </div>
</template>

<style scoped>
.subscriptions {
  text-align: center;
}

.title {
  color: var(--primary);
  text-shadow: var(--glow);
  margin-bottom: 30px;
}

.plans {
  display: flex;
  justify-content: center;
  gap: 20px;
}

.plan {
  width: 260px;
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.price {
  color: var(--primary);
  font-size: 20px;
}

.featured {
  border: 1px solid var(--primary);
  box-shadow: 0 0 20px var(--glow);
}
</style>
