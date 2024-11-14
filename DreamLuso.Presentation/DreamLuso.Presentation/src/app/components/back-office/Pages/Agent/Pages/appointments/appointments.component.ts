import { Component, Input, OnInit } from '@angular/core';
import { UserModel } from '../../../../../../models/UserModel';
import { UserService } from '../../../../../../services/UserService/user.service';
import { Router } from '@angular/router';
import { AuthService } from '../../../../../../services/AuthService/auth.service';
import { environment } from '../../../../../../../../environment';
import { NotificationService } from '../../../../../../services/NotificationService/notification.service';
import { ApiNotification } from '../../../../../../models/Notification';
import { TimeSlot, VisitStatus } from '../../../../../../models/CreatePropertyVisit';
import { PropertyVisitService } from '../../../../../../services/PropertyVisitService/property-visit.service';
import { PropertyVisitResponse } from '../../../../../../models/PropertyVisitResponse';
import { PropertyService } from '../../../../../../services/PropertyService/property.service';
import { Property, PropertyStatus, PropertyType } from '../../../../../../models/property';
import { Observable } from 'rxjs';
import { forkJoin } from 'rxjs';
import { RealStateAgentService } from '../../../../../../services/RealStateAgent/real-state-agent.service';
import { RealStateAgentModel } from '../../../../../../models/RealStateAgentModel';

@Component({
  selector: 'app-appointments',
  templateUrl: './appointments.component.html',
  styleUrl: './appointments.component.scss'
})
export class AppointmentsComponent implements OnInit {
  @Input() loggedUserDetails: UserModel | null = null;
  private apiUrl = environment.apiUrl;
  userId: string = '';
  notifications: ApiNotification[] = [];
  appointments: PropertyVisitResponse[] = [];
  realStateAgent: RealStateAgentModel | null = null;
  VisitStatus = VisitStatus;
  TimeSlot = TimeSlot;
  showCancelSuccessAlert: boolean = false;


  constructor(
    private router: Router,
    private userService: UserService,
    private authService: AuthService,
    private notificationService: NotificationService,
    private propertyVisitService: PropertyVisitService,
    private propertyService: PropertyService,
    private realstateAgentService: RealStateAgentService,
  ) {}


  ngOnInit() {
    const loggedUserString = sessionStorage.getItem('loggedUser');
    if (loggedUserString) {
        const loggedUser = JSON.parse(loggedUserString);
        this.userId = loggedUser.id;

        this.userService.retrieve(this.userId).subscribe({
            next: (userDetails: UserModel) => {
                this.loggedUserDetails = userDetails;

                this.realstateAgentService.retrieveByUserId(this.userId).subscribe({
                    next: (agentDetails) => {
                        this.realStateAgent = agentDetails;
                        console.log('Real State Agent details:', this.realStateAgent);
                        this.loadAppointments();
                    },
                    error: (error) => {
                        console.error('Error fetching real state agent details:', error);
                    }
                });
            },
            error: (error) => {
                console.error('Error fetching user details:', error);
            }
        });

        this.loadNotifications();
    }
}

getTimeSlotLabel(slot: number): string {
  switch (slot) {
    case TimeSlot.Morning_8AM_10AM:
      return '8:00 AM - 10:00 AM';
    case TimeSlot.Morning_10AM_12AM:
      return '10:00 AM - 12:00 PM';
    case TimeSlot.Afternoon_2PM_4PM:
      return '2:00 PM - 4:00 PM';
    case TimeSlot.Afternoon_4PM_6PM:
      return '4:00 PM - 6:00 PM';
    default:
      return 'Unknown Time Slot';
  }
}
  getTotalAppointments(): number {
    return this.appointments.length;
  }

  getPendingAppointments(): number {
    return this.appointments.filter(a => {
      console.log('Checking status:', a.status, typeof a.status);
      return a.status === VisitStatus.Pending;
    }).length;
  }

  loadNotifications() {
    this.notificationService.getAllNotifications(this.userId).subscribe({
      next: (notifications) => {
        this.notifications = notifications;
      },
      error: (error) => {
        console.error('Error fetching notifications:', error);
      }
    });
  }

  markAsRead(notificationId: string) {
    this.notificationService.markAsRead(notificationId).subscribe({
      next: () => {
        this.notifications = this.notifications.filter(notification =>
          notification.id !== notificationId
        );
      },
      error: (error) => {
        console.error('Error marking notification as read:', error);
      }
    });
  }
  ngAfterViewInit() {
    console.log('Current appointments:', this.appointments);
    this.appointments.forEach(appointment => {
      console.log(`Appointment ID: ${appointment.id}, Status: ${appointment.status}`);
    });
  }

  loadAppointments() {
    if (!this.realStateAgent) {
      console.log('No real state agent found');
      return;
    }

    this.propertyVisitService.getAgentVisits(this.realStateAgent.id).subscribe({
      next: (appointments) => {
        console.log('Status values from API:', appointments.map(a => a.status));// Debug log

        if (appointments && appointments.length > 0) {
          const propertyRequests = appointments.map(appointment =>
            this.propertyService.retrieve(appointment.propertyId)
          );

          forkJoin(propertyRequests).subscribe({
            next: (properties) => {
              this.appointments = appointments.map((appointment, index) => {
                const mappedAppointment = {
                  ...appointment,
                  property: properties[index],
                  status: appointment.status || VisitStatus.Pending // Default to Pending if status is undefined
                };
                console.log('Mapped appointment:', mappedAppointment); // Debug log
                return mappedAppointment;
              });
              console.log('Final appointments array:', this.appointments); // Debug log
            },
            error: (error) => {
              console.error('Error fetching property details:', error);
            }
          });
        } else {
          this.appointments = [];
        }
      },
      error: (error) => {
        console.error('Error loading appointments:', error);
      }
    });
  }

  confirmVisit(visitId: string) {
    this.propertyVisitService.confirmVisit(visitId).subscribe({
      next: () => {
        this.appointments = this.appointments.map(appointment => {
          if (appointment.id === visitId) {
            return { ...appointment, status: VisitStatus.Confirmed };
          }
          return appointment;
        });
      }
    });
  }

  cancelVisit(visitId: string) {
    console.log('Attempting to cancel visit with ID:', visitId); // Debug log

    if (!visitId) {
        console.error('Visit ID is null or undefined');
        return;
    }

    this.propertyVisitService.cancelVisit(visitId).subscribe({
        next: () => {
            console.log('Successfully canceled visit:', visitId); // Success log

            this.appointments = this.appointments.map(appointment => {
                if (appointment.id === visitId) {
                    return { ...appointment, status: VisitStatus.Canceled };
                }
                return appointment;
            });

            this.showCancelSuccessAlert = true;
            setTimeout(() => {
                this.showCancelSuccessAlert = false;
            }, 9000);

            this.loadAppointments();
        },
        error: (error) => {
            console.error('Error canceling visit:', error);
        }
    });
}

  getImageUrl(imageUrl: string | undefined): string {
    if (!imageUrl) {
      return 'assets/default-avatar.jpg';
    }
    return `${this.apiUrl}${imageUrl}`;
  }

  logout() {
    this.authService.logout().subscribe(
      () => {
        this.clearCache();
        this.router.navigate(['/']);
        window.location.reload();
      },
      (error) => {
        console.error('Logout failed', error);
      }
    );
  }

  private clearCache() {
    localStorage.clear();
    sessionStorage.clear();
    this.clearAllCookies();

    if ('caches' in window) {
      caches.keys().then((names) => {
        names.forEach(name => {
          caches.delete(name);
        });
      });
    }
  }

  private clearAllCookies() {
    const cookies = document.cookie.split(";");
    for (let i = 0; i < cookies.length; i++) {
      const cookie = cookies[i];
      const eqPos = cookie.indexOf("=");
      const name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
      document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT;path=/";
    }
  }

  navigateToProperties() {

    this.router.navigate(['/back-office/agent/properties']);
  }
}
