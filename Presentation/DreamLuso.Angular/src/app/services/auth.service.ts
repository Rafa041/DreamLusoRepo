import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { LoginModel } from '../models/LoginModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'http://localhost:4200/api/auth/login';

  constructor(private httpClient: HttpClient, private router: Router) { }

  login(loginObj: LoginModel): Observable<any> {
    return this.httpClient.post(this.apiUrl, loginObj);
  }
}
