import { IsDevelopment } from "./environment";

export const USER_AUTH_TOKEN = "USER_AUTH_TOKEN";

export const API_URL = IsDevelopment ? "http://localhost:5000/api/" : "/api/";
