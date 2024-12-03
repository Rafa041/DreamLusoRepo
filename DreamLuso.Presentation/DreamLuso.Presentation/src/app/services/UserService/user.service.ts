import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RetrieveAllUsersResponse, RetrieveUserResponse } from '../../models/RetrieveAllUsers';
import { Observable } from 'rxjs/internal/Observable';
import { UserModel } from '../../models/UserModel';
import { Register } from '../../models/Register';
import { Router } from '@angular/router';
import { environment } from '../../../../environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private apiUrl = `${environment.apiUrl}/api/user`;


  constructor(private httpClient: HttpClient, private router: Router) {}


  getAllUsers(): Observable<RetrieveUserResponse[]> {
    return this.httpClient.get<RetrieveUserResponse[]>(`${this.apiUrl}/retrieveall`);
  }
  retrieve(userId: string): Observable<UserModel> {
    return this.httpClient.get<UserModel>(`${this.apiUrl}/${userId}`);
  }
  updateAccess(userId: string, userData: any): Observable<any> {
    return this.httpClient.put(`${this.apiUrl}/users/${userId}`, userData);
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
