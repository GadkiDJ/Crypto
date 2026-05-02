<script setup lang="ts">
import { useAuth } from '../../composables/useAuth';
const {user, logout} = useAuth();
</script>

<template>
    <header class="header">
        <div class="logo">Crypto</div>
        <div class="right">
            <span class="email">{{ user?.email }}</span>
            <button @click="logout">Logout</button>
        </div>
    </header>
</template>

<style scoped>
.header{
    height: 60px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 0 20px;

    border-bottom: 1px solid var(--border);
    background: black;
    backdrop-filter: blur(10px);
}
.logo{
    color: var(--primary);
    font-weight: bold;
    text-shadow: var(--glow);
}
.right{
    display: flex;
    align-items: center;
    gap: 12px;
}
.email{
    color: var(-muted);
}
</style>














