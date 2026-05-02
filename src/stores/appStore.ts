import {defineStore} from "pinia"

export const useAppStore = defineStore("app",{
    state: () => ({
        isAiOpen: false,
        plan: "Free" as "Free" | "Pro" | "Expert",
    }),

    actions: {
        openAi(){
            this.isAiOpen = true;
        },
        closeAi(){
            this.isAiOpen = false;
        },
        setPlan(plan: "Free" | "Pro" | "Expert"){
            this.plan = plan;
        },
    },
});