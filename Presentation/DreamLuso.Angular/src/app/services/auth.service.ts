import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { LoginModel } from '../models/LoginModel';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7224/api/auth/login';

  constructor(private httpClient: HttpClient, private router: Router) { }

  login(loginObj: LoginModel) {
    return this.httpClient.post<LoginModel>(this.apiUrl, loginObj);
  }
  logout(): Observable<any> {
    // Perform any server-side logout operations here if needed
    // For example, invalidating tokens on the server

    // Return an observable that completes immediately
    return of(null);
  }
}
