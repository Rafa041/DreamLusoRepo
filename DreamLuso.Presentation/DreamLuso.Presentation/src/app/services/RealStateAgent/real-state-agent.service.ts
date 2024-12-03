import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { RealStateAgentModel } from '../../models/RealStateAgentModel';
import { environment } from '../../../../environment';
import { CreateAgentComponent } from '../../components/back-office/Pages/Admin/Pages/create-agent/create-agent.component';
import { CreateRealStateAgent } from '../../models/CreateRealStateAgent';


@Injectable({
  providedIn: 'root'
})
export class RealStateAgentService {
  private apiUrl = `${environment.apiUrl}/api/realstateagent`;

  constructor(private httpClient: HttpClient) {}

  createAgent(agentRequest: CreateRealStateAgent): Observable<any> {
    // Send as JSON instead of FormData
    return this.httpClient.post(`${this.apiUrl}/create`, agentRequest, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
  }
  retrieve(id: string): Observable<RealStateAgentModel> {
    return this.httpClient.get<RealStateAgentModel>(`${this.apiUrl}/${id}`);
  }

  retrieveByUserId(userId: string): Observable<RealStateAgentModel> {
    return this.httpClient.get<RealStateAgentModel>(`${this.apiUrl}/user/${userId}`);
  }
}
