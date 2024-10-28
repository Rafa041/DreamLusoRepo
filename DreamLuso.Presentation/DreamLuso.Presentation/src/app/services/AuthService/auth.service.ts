import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { LoginModel } from '../../models/LoginModel';
import { Observable } from 'rxjs/internal/Observable';
import { of } from 'rxjs/internal/observable/of';
import { catchError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = 'https://localhost:7224/api/auth/login';

  constructor(private httpClient: HttpClient, private router: Router) { }

  login(loginObj: LoginModel): Observable<any> {
    return this.httpClient.post<any>(this.apiUrl, loginObj).pipe(
      catchError(this.handleError('login', []))
    );
  }

  logout(): Observable<void> {
    sessionStorage.removeItem('loggedUser');
    this.router.navigate(['/login']);
    return of(undefined); // Retorna um Observable que emite undefined
  }

  isLoggedIn(): boolean {
    // Check if a user is logged in based on sessionStorage
    return !!sessionStorage.getItem('loggedUser');
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(`${operation} failed: ${error.message}`); // Log to console instead
      return of(result as T); // Let the app keep running by returning an empty result
    };
  }
}
