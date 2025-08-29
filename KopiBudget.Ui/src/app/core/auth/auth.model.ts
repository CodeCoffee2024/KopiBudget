export interface LoginRequest {
    usernameEmail: string;
    password: string;
}

export interface LoginResponse {
    token: string;
    refreshToken?: string;
    expiresIn?: number;
}

export interface AuthUser {
    id: string;
    username: string;
    roles: string[];
}
