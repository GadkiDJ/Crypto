<script setup lang="ts">
import {ref} from "vue";
import { useAuthStore } from "../stores/authStore";
import { useRouter } from "vue-router";

const email = ref("");
const password = ref("");

const auth = useAuthStore();
const router = useRouter();

const onSubmit = async () => {
    await auth.login(email.value, password.value);

    if (!auth.error){
        router.push("/");
    }
};
</script>

<template>
    <div class="container">
        <h1>Login</h1>
        <form @submit.prevent="onSubmit">
            <input v-model="email" type="email" placeholder="Email"/>
            <input v-model="password" type="password" placeholder="Password">

            <button type="submit" :disabled="auth.loading">
                {{ auth.loading ? "Loading" : "Login" }}                
            </button>
            <p v-if="auth.error" style="color: red;">
                {{ auth.error }}
            </p>    
        </form>
    </div>    
</template>

<style scoped>
    .auth-wrapper{
        height: 100vh;
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .auth-card{
        width: 320px;
        display: flex;
        flex-direction: column;
        gap: 12px;
    }

    .title{
        text-align: center;
        color: var(--primary);
        text-shadow: var(--glow);
    }

</style>