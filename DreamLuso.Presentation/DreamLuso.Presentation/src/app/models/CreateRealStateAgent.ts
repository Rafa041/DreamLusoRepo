
export interface CreateRealStateAgent {
  userId: string;
  officeEmail: string;
  totalSales: number;
  totalListings: number;
  certifications: string[];
  languagesSpoken: Languages[];
}

export enum Languages {
  English = 'English',
  Spanish = 'Spanish',
  French = 'French',
  German = 'German',
  Italian = 'Italian',
  Portuguese = 'Portuguese'
}
