import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs/internal/Observable";


@Injectable({
  providedIn: 'root'
})
export class RealStateAgentService {

  private apiUrl = 'https://localhost:7224/api/realstateagent';

  constructor(private httpClient: HttpClient, private router: Router) {}

  createAgent(agentData: FormData): Observable<any> {
    return this.httpClient.post<any>(`${this.apiUrl}/create`, agentData);
  }

}
