<script setup lang="ts">
import AppHeader from './AppHeader.vue';
import AppSidebar from './AppSidebar.vue';
import AiDrawer from '../ai/AiDrawer.vue';
</script>

<template>
    <div class="layout">
        <AppSidebar />
        <div class="main">
            <AppHeader />
            <div class="content">
                <router-view />
                <AiDrawer />
            </div>
        </div>
    </div>
</template>

<style scoped>
.layout{
    display: flex;
    height: 100vh;
}

.main{
    flex: 1;
    display: flex;
    flex-direction: column;
}

.contetnt{
    padding: 20px;
}

</style>