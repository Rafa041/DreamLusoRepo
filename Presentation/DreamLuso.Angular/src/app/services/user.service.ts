import { HttpClient, HttpXsrfTokenExtractor } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from '../models/UserModel';
import { Observable, of } from 'rxjs';
import { Register } from '../models/Register';


@Injectable({
  providedIn: 'root'
})
export class UserService {


// private apiUrl = 'https://localhost:7224/api/User/RetrieveAll';

  private apiUrl = 'https://localhost:7224/api/user';

  private apiRetrieveAll = '/api/User/RetrieveAll';

  constructor(private httpClient: HttpClient, private router: Router) {}

  retrieveAll() {
    return this.httpClient.get<UserModel[]>(this.apiRetrieveAll);
  }

  retrieve(userId: string): Observable<UserModel> {
    return this.httpClient.get<UserModel>(`${this.apiUrl}/${userId}`);
  }

  register(user: Register, file: File | null): Observable<any> {
    const formData = new FormData();

    formData.append('firstName', user.firstName);
    formData.append('lastName', user.lastName);
    formData.append('dateOfBirth', user.dateOfBirth);
    formData.append('email', user.email);
    formData.append('password', user.password);

    if (file) {
      formData.append('imageFile', file, file.name);
    }

    return this.httpClient.post(`${this.apiUrl}/register`, formData);
  }

}
