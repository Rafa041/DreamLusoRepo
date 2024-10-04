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
  loginSuccess: boolean = false;
  loginFailure: boolean = false;
  constructor(private router: Router, private authService: AuthService) {}


  ngOnInit() {
    if (sessionStorage.getItem('loggedUser')) {
      this.router.navigateByUrl('');
    }
  }
  onLogin() {
    this.authService.login(this.loginObj).subscribe({
      next: (res) => {
        this.loginSuccess = true;
        sessionStorage.setItem('loggedUser', JSON.stringify(res));
        setTimeout(() => {
          this.router.navigateByUrl('/dashboard');
        }, 2000);
      },
      error: (error) => {
        this.errorMessage = error.message || 'Ocorreu um erro durante o login';
        this.loginFailure = true;
        setTimeout(() => {
          this.loginFailure = false;
        }, 2000);
      }
    });
  }
  signUp() {
    this.router.navigateByUrl('/register');
  }
}
