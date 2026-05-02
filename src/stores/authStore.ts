import { defineStore } from "pinia";
import { authApi } from "../api/auth";
import type { User } from "../types/auth";

export const useAuthStore = defineStore("auth", {
  state: () => ({
    user: null as User | null,
    token: localStorage.getItem("token") || "",
    loading: false,
    error: null as string | null,
  }),

  actions: {
    async register(email: string, password: string) {
      this.loading = true;
      this.error = null;
      try {
        const res = await authApi.register(email, password);
        this.user = res.user;
        this.token = res.token;
        localStorage.setItem("token", res.token);
      } catch (e: any) {
        this.error = e.message;
        throw e; 
      } finally {
        this.loading = false;
      }
    },

    async login(email: string, password: string) {
      this.loading = true;
      this.error = null;
      try {
        const res = await authApi.login(email, password);
        this.user = res.user;
        this.token = res.token;
        localStorage.setItem("token", res.token);
      } catch (e: any) {
        this.error = e.message;
      } finally {
        this.loading = false;
      }
    },

    logout() {
      this.user = null;
      this.token = "";
      localStorage.removeItem("token");
    },
  },
});
