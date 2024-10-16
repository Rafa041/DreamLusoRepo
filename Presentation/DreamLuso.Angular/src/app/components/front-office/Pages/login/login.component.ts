import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../../services/auth.service';
import { LoginModel } from '../../../../models/LoginModel';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  errorMessage: string = '';
  loginStatus: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      rememberMe: [false]
    });
  }

  ngOnInit() {
    // Se o usuário já estiver logado, redireciona para o dashboard
    if (sessionStorage.getItem('loggedUser')) {
      this.router.navigateByUrl('/');
    }
  }

  onLogin() {
    if (this.loginForm.valid) {
      this.loginStatus = true;
      this.errorMessage = '';

      const loginData = this.loginForm.value;

      this.authService.login(loginData).subscribe({
        next: (res) => {
          sessionStorage.setItem('loggedUser', JSON.stringify(res));
          console.log('Login successful:', res);

          setTimeout(() => {
            this.loginStatus = false;
            this.router.navigateByUrl('/');
          }, 2000);
        },
        error: (error) => {
          this.errorMessage = error.message || 'Ocorreu um erro durante o login';
          this.loginStatus = false;
          console.log('Login error:', error);

          setTimeout(() => {
            this.errorMessage = '';
          }, 2000);
        }
      });
    } else {
      this.loginForm.markAllAsTouched();
    }
  }
}
