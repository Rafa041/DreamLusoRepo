export interface Property {
  [key: string]: any;  // This allows string s
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
  type: PropertyType;
  size: number;
  bedrooms: number;
  bathrooms: number;
  price: number;
  amenities: string;
  status: PropertyStatus;
  yearBuilt?: string;
  ownerInformation?: string;
  heatingSystem?: string;
  coolingSystem?: string;
  images?: PropertyImage[];
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
  Other = 'Other',
}
