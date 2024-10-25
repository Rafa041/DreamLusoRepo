import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { RealStateAgentModel } from '../../models/RealStateAgentModel';


@Injectable({
  providedIn: 'root'
})
export class RealStateAgentService {

  private apiUrl = 'https://localhost:7224/api/realstateagent';

  constructor(private httpClient: HttpClient, private router: Router) {}

  createAgent(agentData: FormData): Observable<any> {
    return this.httpClient.post<any>(`${this.apiUrl}/create`, agentData);
  }
  retrieve(id: string): Observable<RealStateAgentModel> {
    // Add error handling and logging
    console.log(`Calling API: ${this.apiUrl}/retrieve/${id}`);
    return this.httpClient.get<RealStateAgentModel>(`${this.apiUrl}/${id}`);
  }
}
