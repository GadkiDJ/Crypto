import type { AuthResponse } from "../types/auth";

export const login = async (
    Email: string,
    Password: string
): Promise<AuthResponse> => {
    const res = await fetch("https://localhost:7072/api/auth/login", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({Email, Password}),
    });
    if (!res.ok){
        throw new Error("Login faild");
    }
    return res.json();
};


export const register = async (
    Email: string,
    Password: string
): Promise<AuthResponse> => {
    const res = await fetch("https://localhost:7072/api/auth/register", {
        method: "POST",
        headers: {
            "Content-type": "application/json",
        },
        body: JSON.stringify({Email, Password}),
    });
    if(!res.ok){
        throw Error("Register faild");
    }
    return res.json();
};
export const authApi = { login, register };