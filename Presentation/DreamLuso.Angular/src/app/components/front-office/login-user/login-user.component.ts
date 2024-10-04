import { Component, OnInit } from '@angular/core';
import { LoginModel } from '../../../models/LoginModel';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
@Component({
  selector: 'app-login-user',
  templateUrl: './login-user.component.html',
  styleUrl: './login-user.component.scss'
})

export class LoginUserComponent implements OnInit {

  loginObj: LoginModel = {} as LoginModel;
  errorMessage: string = '';
  loginStatus: boolean = false; // Usar uma única propriedade para controlar o status
  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit() {
    if (sessionStorage.getItem('loggedUser')) {
      this.redirectToDashboard();
    }
  }

  onLogin() {
    this.authService.login(this.loginObj).subscribe({
      next: (res) => this.handleLoginSuccess(res),
      error: (error) => this.handleLoginError(error)
    });
  }

  signUp() {
    this.router.navigateByUrl('/register');
  }

  private handleLoginSuccess(res: any) {
    sessionStorage.setItem('loggedUser', JSON.stringify(res));
    this.loginStatus = true;
    setTimeout(() => {
      this.redirectToDashboard();
    }, 2000);
  }

  private handleLoginError(error: any) {
    this.errorMessage = error.message || 'Ocorreu um erro durante o login';
    this.loginStatus = false;
    setTimeout(() => {
      this.errorMessage = '';
    }, 2000);
  }

  private redirectToDashboard() {
    this.router.navigateByUrl('/header');
  }
}
