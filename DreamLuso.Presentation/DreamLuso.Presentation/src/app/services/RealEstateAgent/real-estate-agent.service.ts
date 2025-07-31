import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { RealEstateAgentModel } from '../../models/RealEstateAgentModel';
import { environment } from '../../../../environment';
import { CreateAgentComponent } from '../../components/back-office/Pages/Admin/Pages/create-agent/create-agent.component';
import { CreateRealEstateAgent } from '../../models/CreateRealEstateAgent';


@Injectable({
  providedIn: 'root'
})
export class RealEstateAgentService {
  private apiUrl = `${environment.apiUrl}/api/realstateagent`;

  constructor(private httpClient: HttpClient) {}

  createAgent(agentRequest: CreateRealEstateAgent): Observable<any> {
    // Send as JSON instead of FormData
    return this.httpClient.post(`${this.apiUrl}/create`, agentRequest, {
      headers: {
        'Content-Type': 'application/json'
      }
    });
  }
  retrieve(id: string): Observable<RealEstateAgentModel> {
    return this.httpClient.get<RealEstateAgentModel>(`${this.apiUrl}/${id}`);
  }

  retrieveByUserId(userId: string): Observable<RealEstateAgentModel> {
    return this.httpClient.get<RealEstateAgentModel>(`${this.apiUrl}/user/${userId}`);
  }
}
