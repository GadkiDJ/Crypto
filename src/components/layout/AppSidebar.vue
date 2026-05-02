<script setup lang="ts">
import { useAppStore } from '../../stores/appStore';
const app = useAppStore();
</script>

<template>
    <aside class="sidebar">
        <div class="title">Menu</div>
        <nav>
            <a href="/">Profile</a>
            <a href="/market">Market</a>
            <a href="/trade">Trade</a>
            <a href="/subscription">Open demo-score</a>
            <a href="/subscription">Bot-settings</a>
            <a href="/subscription">Trading tips</a>
            <a href="/subscription">Subscription</a>
            <a href="/history">Trade history</a>
            <button class="ai-btn" @click="app.openAi()">Ask AI</button>
        </nav>
    </aside>
</template>

<style scoped>
.sidebar{
    width: 220px;
    padding: 20px;
    border-right: 1px solid  var(--border);
    background: black;
}
.title{
    color: var(--primary);
    margin:-bottom 20px;
    text-shadow: var(--glow);
}

nav{
    display: flex;
    flex-direction: column;
    gap: 10px;
}
a{
    color: var(--text);
    text-decoration: none;
    padding: 8px;
    border-radius: 6px;
}

a:hover{
    background: var(--glow);
}

</style>