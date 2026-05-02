<script setup lang="ts">
</script>


<template>
    <div class="card">
        <h2>Trade history</h2>
        <div class="empty">
            <p>No trades yet</p>
            <span>Your trades will appear here</span>
        </div>
    </div>
</template>
<style scoped>
.empty{
    margin-top: 30px;
    padding: 40px;
    text-align: center;
    border: 1px dashed var(--border);
    color: var(--muted);
}
</style>