import { Injectable } from "@angular/core";
import { Subject } from "rxjs/internal/Subject";
import { environment } from "../../../../environment";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { ApiNotification, UINotification } from "../../models/Notification";


@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private notificationSubject = new Subject<UINotification>();
  notifications$ = this.notificationSubject.asObservable();
  private apiUrl = `${environment.apiUrl}/api/notifications`;

  constructor(private http: HttpClient) {}

  showSuccess(message: string) {
    this.notificationSubject.next({ message, type: 'success' });
  }

  showError(message: string) {
    this.notificationSubject.next({ message, type: 'error' });
  }

  getUnreadNotifications(userId: string): Observable<ApiNotification[]> {
    return this.http.get<ApiNotification[]>(`${this.apiUrl}/${userId}/unread`);
  }

  markAsRead(notificationId: string): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${notificationId}/markAsRead`, {});
  }
  getAllNotifications(userId: string): Observable<ApiNotification[]> {
    return this.http.get<ApiNotification[]>(`${this.apiUrl}/${userId}/all`);
}
}
