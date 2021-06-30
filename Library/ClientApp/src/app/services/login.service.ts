import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { API_URL } from "src/utils/constants";
import { handleHttpErrors } from "src/utils/services";

@Injectable({
  providedIn: "root",
})
export class LoginService {
  constructor(private http: HttpClient) {}

  login(auth: IAuhLogin): Promise<string> {
    return new Promise((resolve, reject) => {
      this.http.post(`${API_URL}auth/login`, auth).subscribe(
        (res) => {
          resolve(res["response"]);
        },
        (err) => {
          reject(handleHttpErrors(err));
        }
      );
    });
  }
}
