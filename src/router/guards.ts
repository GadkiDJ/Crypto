import { useAuthStore } from "../stores/authStore";
import type { RouteLocationNormalized } from "vue-router";
export const authGuard = (to: RouteLocationNormalized) => {
  const auth = useAuthStore();
  const token = auth.token || localStorage.getItem("token");

  const publicPages = ["/login", "/register"];
  const authRequired = !publicPages.includes(to.path);
  if (authRequired && !token) {
    return "/login";
  }

  if (!authRequired && token) {
    return "/";
  }
  return true;
};