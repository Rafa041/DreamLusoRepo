import { VisitStatus } from "./CreatePropertyVisit";
import { Property } from "./property";

export interface PropertyVisitResponse {
  id: string;
  propertyId: string;
  userId: string;
  realStateAgentId: string;
  visitDate: string;
  timeSlot: number;
  createdAt: string;
  status: VisitStatus;
  property: Property;
}
