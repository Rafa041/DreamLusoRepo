import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RetrieveUserResponse } from '../../models/RetrieveAllUsers';
import { Observable } from 'rxjs/internal/Observable';
import { UserModel } from '../../models/UserModel';
import { Register } from '../../models/Register';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = 'https://localhost:7224/api/user';

  private apiRetrieveAll = '/api/User/RetrieveAll';

  constructor(private httpClient: HttpClient, private router: Router) {}


  getAllUsers(): Observable<{ users: RetrieveUserResponse[] }> {
    return this.httpClient.get<{ users: RetrieveUserResponse[] }>(`${this.apiUrl}/retrieveall`);
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
