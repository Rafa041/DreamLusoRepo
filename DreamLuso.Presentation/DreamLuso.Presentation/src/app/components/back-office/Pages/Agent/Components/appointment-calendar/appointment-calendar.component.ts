import { Component, Input, OnInit } from '@angular/core';
import { CalendarEvent, CalendarView } from 'angular-calendar';
import { MonthViewDay } from 'calendar-utils';
import { addDays } from 'date-fns';
import { PropertyVisitResponse } from '../../../../../../models/PropertyVisitResponse';
import { VisitStatus } from '../../../../../../models/CreatePropertyVisit';

interface EventColor {
  primary: string;
  secondary: string;
}
@Component({
  selector: 'app-appointment-calendar',
  templateUrl: './appointment-calendar.component.html',
  styleUrl: './appointment-calendar.component.scss'
})
export class AppointmentCalendarComponent implements OnInit {
  @Input() appointments: PropertyVisitResponse[] = [];

  view: CalendarView = CalendarView.Month;
  viewDate: Date = new Date();
  CalendarView = CalendarView;
  events: CalendarEvent[] = [];

  private readonly statusColors: Record<VisitStatus, EventColor> = {
    [VisitStatus.Pending]: { primary: '#eab308', secondary: '#fef3c7' },
    [VisitStatus.Confirmed]: { primary: '#22c55e', secondary: '#dcfce7' },
    [VisitStatus.Canceled]: { primary: '#ef4444', secondary: '#fee2e2' },
    [VisitStatus.Scheduled]: { primary: '#3b82f6', secondary: '#dbeafe' },
    [VisitStatus.Completed]: { primary: '#8b5cf6', secondary: '#f3e8ff' }
};

  ngOnInit() {
    this.loadAppointmentsToCalendar();
  }

  loadAppointmentsToCalendar() {
    this.events = this.appointments.map(appointment => ({
      start: new Date(appointment.visitDate),
      title: `${appointment.property.title} - ${appointment.status}`,
      color: this.getStatusColor(appointment.status as VisitStatus),
      meta: appointment
    }));
  }

  getStatusColor(status: VisitStatus): EventColor {
    return this.statusColors[status] || { primary: '#3b82f6', secondary: '#dbeafe' };
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  dayClicked(event: { day: MonthViewDay<any>; sourceEvent: MouseEvent | KeyboardEvent }): void {
    if (event.sourceEvent instanceof MouseEvent) {
      const clickedDate = event.day.date;
      const appointmentsOnDay = this.appointments.filter(appointment => {
        const appointmentDate = new Date(appointment.visitDate);
        return appointmentDate.toDateString() === clickedDate.toDateString();
      });

      if (appointmentsOnDay.length > 0) {
        console.log('Appointments on this day:', appointmentsOnDay);
      }
    }
  }
}
