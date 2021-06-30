import { Component } from "@angular/core";
import { USER_AUTH_TOKEN } from "src/utils/constants";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
})
export class AppComponent {
  public token: string;

  constructor() {
    this.token = localStorage.getItem(USER_AUTH_TOKEN);
  }
}
