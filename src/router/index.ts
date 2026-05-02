import { createRouter, createWebHistory } from "vue-router";
import Login from "../pages/Login.vue";
import Register from "../pages/Register.vue";
import Dashboard from "../pages/Dashboard.vue";
import { authGuard } from "./guards";
import AppLayout from "../components/layout/AppLayout.vue";
import Subscription from "../pages/Subscription.vue";
import TradeHistory from "../pages/TradeHistory.vue";
import Market from "../pages/Market.vue";


const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/",
      component: AppLayout,
      children:[
        {path: "", component: Dashboard},
        {path: "subscription", component: Subscription},
        {path: "history", component: TradeHistory},
        {path: "market", component: Market}
      ]
    },
    {
      path: "/login",
      component: Login,
    },
    {
      path: "/register",
      component: Register,
    },
  ],
});
router.beforeEach(authGuard);




export default router;