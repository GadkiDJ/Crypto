<script setup lang="ts">
import { useAuth } from '../composables/useAuth';

const auth = useAuth();
</script>

<template>
    <div>
        <h1>Dashboard</h1>
        <p>You are logged in</p>
        <p>Email: {{ auth.user?.email }}</p>
        <p>Tenant: {{ auth.user?.tenantId }}</p>
        <button @click="auth.logout()">Logout</button>
    </div>
</template>