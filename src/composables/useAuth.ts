import { useAuthStore } from "../stores/authStore";
import { useRouter } from "vue-router";

export const useAuth = ()=>{
    const auth = useAuthStore();
    const router = useRouter();

    const logout = () =>{
        auth.logout();
        router.push("/login");
    };
    return {
        ...auth,
        logout,
    };
};