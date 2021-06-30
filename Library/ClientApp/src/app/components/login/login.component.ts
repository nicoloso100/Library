import { Component, OnInit } from "@angular/core";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { LoginService } from "src/app/services/login.service";
import { USER_AUTH_TOKEN } from "src/utils/constants";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private loginService: LoginService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: new FormControl("", Validators.required),
      password: new FormControl("", Validators.required),
    });
  }

  get username() {
    return this.loginForm.get("username");
  }

  get password() {
    return this.loginForm.get("password");
  }

  get invalidForm() {
    return this.loginForm.invalid;
  }

  onSubmit(): void {
    if (!this.invalidForm) {
      const auth: IAuhLogin = {
        username: this.username.value,
        password: this.password.value,
      };
      this.loginService
        .login(auth)
        .then((token) => {
          localStorage.setItem(USER_AUTH_TOKEN, token);
          window.location.href = "/";
        })
        .catch((err) => this.toastr.error(err));
    } else {
      this.loginForm.setErrors({ ...this.loginForm.errors });
    }
  }
}
