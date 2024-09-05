import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { UsersModel } from '../models/UsersModel';


@Injectable({
  providedIn: 'root'
})
export class UserService {


// private apiUrl = 'https://localhost:7224/api/User/RetrieveAll';

  private apiUrl = '/api/User';

  private apiRetrieveAll = '/api/User/RetrieveAll';

  constructor(private httpClient: HttpClient, private router: Router) {}

  RetrieveAll() {
    return this.httpClient.get<UsersModel[]>(this.apiRetrieveAll);
  }
}
