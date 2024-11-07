import { Component, Input, OnInit } from '@angular/core';
import { Property } from '../../../../models/property';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PropertyVisitService } from '../../../../services/PropertyVisitService/property-visit.service';
import { NotificationService } from '../../../../services/NotificationService/notification.service';
import { AuthService } from '../../../../services/AuthService/auth.service';
import { CreatePropertyVisit } from '../../../../models/CreatePropertyVisit';

@Component({
  selector: 'app-schedule-visit',
  templateUrl: './schedule-visit.component.html',
  styleUrl: './schedule-visit.component.scss'
})
export class ScheduleVisitComponent implements OnInit {
  @Input() property!: Property;
  visitForm: FormGroup;
  isLoggedIn: boolean = false;
  showSundayAlert: boolean = false;
  showSuccessAlert: boolean = false;
  availableTimeSlots: any[] = [];
  isLoadingSlots: boolean = false;
  showTimeSlotAlert: boolean = false;
  showTimeSlotBookedAlert: boolean = false;

  readonly timeSlotOptions = [
    { value: 0, label: '8:00 - 10:00' },
    { value: 1, label: '10:00 - 12:00' },
    { value: 2, label: '14:00 - 16:00' },
    { value: 3, label: '16:00 - 18:00' }
  ];

  constructor(
    private fb: FormBuilder,
    private propertyVisitService: PropertyVisitService,
    private notificationService: NotificationService,
    private authService: AuthService
  ) {
    this.visitForm = this.fb.group({
      date: ['', Validators.required],
      timeSlot: ['', Validators.required]
    });

    this.visitForm.get('date')?.valueChanges.subscribe(date => {
      if (date && this.isDateValid(date)) {
        this.loadAvailableTimeSlots(date);
      }
    });
  }

  ngOnInit() {
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  getCurrentDate(): string {
    return new Date().toISOString().split('T')[0];
  }

  loadAvailableTimeSlots(date: string) {
    if (!this.property?.id) return;

    this.isLoadingSlots = true;
    this.visitForm.get('timeSlot')?.disable();

    this.propertyVisitService.getVisitsByDate(this.property?.id.toString(), date)
        .subscribe({
            next: (visits) => {
                const bookedSlots = visits.map((visit: any) => visit.timeSlot);
                this.availableTimeSlots = this.timeSlotOptions.filter(slot =>
                    !bookedSlots.includes(slot.value)
                );
                this.showTimeSlotAlert = this.availableTimeSlots.length === 0;
                this.visitForm.get('timeSlot')?.enable();
            },
            error: (error) => {
                this.notificationService.showError('Failed to load available time slots');
                console.error('Error:', error);
            },
            complete: () => {
                this.isLoadingSlots = false;
            }
        });
}

  getTimeSlotLabel(slot: string): string {
    const slots: { [key: string]: string } = {
      '0': '8:00 - 10:00',
      '1': '10:00 - 12:00',
      '2': '14:00 - 16:00',
      '3': '16:00 - 18:00'
    };
    return slots[slot] || '';
  }

  isDateValid(date: string): boolean {
    const selectedDate = new Date(date);
    if (selectedDate.getDay() === 0) {
      this.showSundayAlert = true;
      setTimeout(() => {
        this.showSundayAlert = false;
      }, 9000);
      return false;
    }
    this.showSundayAlert = false;
    return true;
  }

  scheduleVisit(): void {
    if (!this.property?.id) {
        this.notificationService.showError('Property information is missing.');
        return;
    }

    const propertyId = this.property.id;
    const realStateAgentId = this.property.realStateAgentId;
    const selectedDate = this.visitForm.get('date')?.value;
    const selectedTimeSlot = Number(this.visitForm.get('timeSlot')?.value);

    if (!realStateAgentId || !selectedDate || this.visitForm.invalid) {
        this.notificationService.showError('Please fill in all required fields.');
        return;
    }

    this.propertyVisitService.getVisitsByDate(this.property.id.toString(), selectedDate)
        .subscribe({
            next: (visits) => {
                const bookedSlots = visits.map((visit: any) => visit.timeSlot);

                if (bookedSlots.includes(selectedTimeSlot)) {
                    this.showTimeSlotBookedAlert = true;
                    setTimeout(() => {
                        this.showTimeSlotBookedAlert = false;
                    }, 9000);
                    this.loadAvailableTimeSlots(selectedDate);
                    return;
                }

                const visitRequest: CreatePropertyVisit = {
                    propertyId: propertyId.toString(),
                    userId: this.authService.getCurrentUserId().toString(),
                    realStateAgentId: realStateAgentId.toString(),
                    visitDate: selectedDate,
                    timeSlot: selectedTimeSlot
                };

                this.propertyVisitService.createVisit(visitRequest).subscribe({
                    next: () => {
                        this.showSuccessAlert = true;
                        setTimeout(() => {
                            this.showSuccessAlert = false;
                        }, 9000);
                        this.visitForm.reset();
                        this.loadAvailableTimeSlots(selectedDate);
                    },
                    error: () => {
                        this.notificationService.showError('Unable to schedule visit. Please try again.');
                    }
                });
            },
            error: () => {
                this.notificationService.showError('Unable to verify time slot availability. Please try again.');
            }
        });
}
}
