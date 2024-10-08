import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { LoginModel } from '../../../models/LoginModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  loginObj: LoginModel = {} as LoginModel;
  errorMessage: string = '';
  loginSuccess: boolean = false;
  loginFailure: boolean = false;


  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit() {
    if (sessionStorage.getItem('loggedUser')) {
      this.router.navigateByUrl('/dashboard');
    }
  }

  onLogin() {
    this.authService.login(this.loginObj).subscribe({
      next: (res) => {
        this.loginSuccess = true;
        sessionStorage.setItem('loggedUser', JSON.stringify(res));
        console.log(res);
        setTimeout(() => {
          this.router.navigateByUrl('/dashboard');
        }, 2000);
      },
      error: (error) => {
        this.errorMessage = error.message || 'Ocorreu um erro durante o login';
        this.loginFailure = true;
        console.log(error);
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
