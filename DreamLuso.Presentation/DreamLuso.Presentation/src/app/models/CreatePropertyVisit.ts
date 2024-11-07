export interface CreatePropertyVisit {
  propertyId: string;
  userId: string;
  realStateAgentId: string;
  visitDate: string;
  timeSlot: number;
}
export enum TimeSlot {
  Morning_8AM_10AM = 0,
  Morning_10AM_12AM = 1,
  Afternoon_2PM_4PM = 2,
  Afternoon_4PM_6PM = 3
}
export enum VisitStatus {
  Scheduled = 'Scheduled',
  Completed = 'Completed',
  Pending = 'Pending',
  Canceled = 'Canceled',
  Confirmed = 'Confirmed'
}


