export type User = {
    email: string;
    tenantId: string;
};
export type AuthResponse = {
    token: string;
    user: User;
};