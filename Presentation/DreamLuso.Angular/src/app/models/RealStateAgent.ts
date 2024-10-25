import { UserModel } from "./UserModel";

export interface RealStateAgent {
  id: string;
  access: number;
  user: UserModel;
  userId: string;
  officeEmail: string;
  totalSales: string;
  totalListings: string;
  certifications: string[];
  languagesSpoken: string[];
}
export enum Languages {
  English = 'English',
  Spanish = 'Spanish',
  French = 'French',
  German = 'German',
  Italian = 'Italian',
  Portuguese = 'Portuguese'
}
