import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModel } from '../models/UserModel';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

private apiUrl = '/api/User';

  constructor(private httpClient: HttpClient, private router: Router) {}

  getAll () {
    return this.httpClient.get<UserModel[]>(this.apiUrl);
  }
}
