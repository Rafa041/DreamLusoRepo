import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { LoginModel } from '../models/LoginModel';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = '/api/Auth';

  constructor(private httpClient: HttpClient, private router: Router) { }

  login(loginObj: LoginModel) {
    return this.httpClient.post<LoginModel>(this.apiUrl + '/login', loginObj);
  }
}
