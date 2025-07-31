import { Injectable } from '@angular/core';
import { environment } from '../../../../environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CreatePropertyVisit, TimeSlot } from '../../models/CreatePropertyVisit';
import { PropertyVisitResponse } from '../../models/PropertyVisitResponse';
import { Observable } from 'rxjs/internal/Observable';
import { tap } from 'rxjs/internal/operators/tap';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs/internal/observable/throwError';

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
            formData.append('RealEstateAgentId', visitRequest.realEstateAgentId);
    formData.append('VisitDate', visitRequest.visitDate.toString());
    formData.append('TimeSlot', visitRequest.timeSlot.toString());

    // Log the request values directly
    console.log('Form Data Values:', {
      PropertyId: visitRequest.propertyId,
      UserId: visitRequest.userId,
      RealEstateAgentId: visitRequest.realEstateAgentId,
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
    // Add error handling and logging
    return this.http.get<PropertyVisitResponse[]>(`${this.apiUrl}/user/${agentId}`).pipe(
      tap(response => console.log('Agent visits response:', response)),
      catchError(error => {
        console.error('Error fetching agent visits:', error);
        return throwError(() => error);
      })
    );
  }
  cancelVisit(visitId: string): Observable<any> {
    if (!visitId) {
        return throwError(() => new Error('Visit ID is required'));
    }

    const command = { visitId: visitId };
    return this.http.put(`${this.apiUrl}/cancelVisit`, command).pipe(
        tap(response => console.log('Cancel visit response:', response)),
        catchError(error => {
            console.error('Error in cancel visit:', error);
            return throwError(() => error);
        })
    );
  }
  getVisitsByDate(propertyId: string, date: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/visitsByDate`, {
      params: { propertyId, date }
    });
  }
  confirmVisit(confirmationToken: string): Observable<any> {
    return this.http.put(`${this.apiUrl}/confirmVisit`, { confirmationToken });
  }
}
