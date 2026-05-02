<script setup lang="ts">
import { useAppStore } from "../../stores/appStore";
import {ref} from "vue";

const app = useAppStore();
type Message= {
    role: "user" | "ai";
    text:  string;
};

const message = ref<Message[]>([]);
const input = ref("");
const loading = ref(false);

const sendMessage = async () => {
    if (!input.value.trim()) return;

    message.value.push({
        role: "user",
        text: input.value,
    });

    const userText = input.value;
    input.value = "";

    loading.value = true;
    setTimeout(() =>{
        message.value.push({
            role: "ai",
            text: `Response for: "${userText}"`,
        });
        loading.value = false;
    }, 600);
};

</script>

<template>
    <div v-if="app.isAiOpen" class="overlay" @click="app.closeAi">
    <div class="drawer" @click.stop>
      
      <div class="header">
        <h3>Ask AI</h3>
        <button @click="app.closeAi">X</button>
      </div>
        <div class="chat">
            <div
                v-for="(msg, index) in message"
                :key="index"
                :class="['msg', msg.role]">           
                {{ msg.text }}
            </div>
            <div v-if="loading" class="msg ai">Thinking...</div>
        </div>
        <div class="input">
        <input v-model="input"
        placeholder="Ask somthing"
        @keyup.enter="sendMessage"
        />
        <button @click="sendMessage">🌐</button>            
        </div>
    </div>
    </div>
</template>

<style scoped>
.drawer {
    position: fixed;
    right: 0;
    top: 0;
    width: 380px;
    height: 100%;
    background: black;
    border-left: 1px solid var(--border);
    box-shadow: var(--glow);
    display: flex;
    flex-direction: column;
}
.header{
    padding: 16px;
    color: var(--primary);
    border-bottom: 1px solid var(--border);
}
.chat{
    flex: 1;
    padding: 12px;
    overflow-y: auto;
    display: flex;
    flex-direction: column;
    gap: 10px;
}
.msg{
    padding: 8px 10px;
    border-radius: 8px;
    max-width: 80%;
    font-size: 14px;
}
.user{
    align-self: flex-end;
    background: var(--glow);
    border: 1px solid var(--border);
}
.ai{
    align-self: flex-start;
    background: black;
    border: 1px solid var(--border);
}
.input{
    display: felx;
    gap: 8px;
    padding: 12px;
    border-top: 1px solid var(--border);
}
input{
    flex: 1;
}

</style>