export interface Property {
  id?: string;
  title: string;
  description: string;
  street: string;
  city: string;
  state: string;
  postalCode: string;
  country: string;
  additionalInfo?: string;
  userId: string;
  realStateAgentId: string
  type: PropertyType;
  size: number;
  bedrooms: number;
  bathrooms: number;
  price: number;
  amenities: string[];
  status: PropertyStatus;
  images?: PropertyImage[];
  yearBuilt?: string;
  isActive: boolean;
  [key: string]: any;
}

export interface PropertyImage {
  id: string;
  propertyId: string;
  fileName: string;
  imagePath?: string;
  file?: File;
}

export enum PropertyType {
  House = 'House',
  Apartment = 'Apartment',
  Condo = 'Condo',
  Townhouse = 'Townhouse',
  Land = 'Land',
  Commercial = 'Commercial',
  Other = 'Other',
}

export enum PropertyStatus {
  Available = 'Available',
  Sold = 'Sold',
  Pending = 'Pending',
  Rented = 'Rented',
  UnderContract = 'UnderContract',
  Unavailable = 'Unavailable',
  ForSale = 'Sale',          // Venda
  ForRent = 'Rent',          // Para Arrendar
  Other = 'Other',
}

export interface PropertyImage {
  id: string;
  propertyId: string;
  fileName: string;
  file?: File;
}
