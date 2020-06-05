import { Role } from './role';

export interface User {
    id: number;
    username: string;
    password: string;
    token: string;
    role: Role;
}