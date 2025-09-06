import { PermissionDto } from "../../domain/models/permission";

export interface LoginRequest {
    usernameEmail: string;
    password: string;
}
export interface RegisterRequest {
    firstName: string;
    middleName: string;
    lastName: string;
    email: string;
    username: string;
    password: string;
}

export interface LoginResponse {
    token: string;
    refreshToken?: string;
    expiresIn?: number;
}

export interface AuthUser {
    firstName: string;
    middleName: string;
    lastName: string;
    email: string;
    username: string;
    roles: [];
    permissions: PermissionDto[];
}
