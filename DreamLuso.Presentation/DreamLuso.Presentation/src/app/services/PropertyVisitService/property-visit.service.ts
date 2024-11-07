import { Injectable } from '@angular/core';
import { environment } from '../../../../environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CreatePropertyVisit, TimeSlot } from '../../models/CreatePropertyVisit';
import { PropertyVisitResponse } from '../../models/PropertyVisitResponse';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class PropertyVisitService {
  private apiUrl = `${environment.apiUrl}/api/PropertyVisit`;

  constructor(private http: HttpClient) { }

  createVisit(visitRequest: CreatePropertyVisit): Observable<any> {
    const formData = new FormData();

    formData.append('PropertyId', visitRequest.propertyId);
    formData.append('UserId', visitRequest.userId);
    formData.append('RealStateAgentId', visitRequest.realStateAgentId);
    formData.append('VisitDate', visitRequest.visitDate.toString());
    formData.append('TimeSlot', visitRequest.timeSlot.toString());

    // Log the request values directly
    console.log('Form Data Values:', {
      PropertyId: visitRequest.propertyId,
      UserId: visitRequest.userId,
      RealStateAgentId: visitRequest.realStateAgentId,
      VisitDate: visitRequest.visitDate,
      TimeSlot: visitRequest.timeSlot
    });

    return this.http.post(`${this.apiUrl}/Create`, formData);
  }
  getAvailableTimeSlots(propertyId: string, date: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/availableSlots`, {
      params: {
        propertyId,
        date
      }
    });
  }
  getVisitById(visitId: string): Observable<PropertyVisitResponse> {
    return this.http.get<PropertyVisitResponse>(`${this.apiUrl}/${visitId}`);
  }

  getUserVisits(userId: string): Observable<PropertyVisitResponse[]> {
    return this.http.get<PropertyVisitResponse[]>(`${this.apiUrl}/user/${userId}`);
  }

  getAgentVisits(agentId: string): Observable<PropertyVisitResponse[]> {
    return this.http.get<PropertyVisitResponse[]>(`${this.apiUrl}/agent/${agentId}`);
  }
  cancelVisit(visitId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${visitId}`);
  }
  getVisitsByDate(propertyId: string, date: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/visitsByDate`, {
      params: { propertyId, date }
    });
  }
}
