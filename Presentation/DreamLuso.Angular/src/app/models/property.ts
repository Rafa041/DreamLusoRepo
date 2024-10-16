export interface Property {
  id?: string;
  title: string;
  description: string;
  street: string;
  city: string;
  state: string;
  postalCode: string;
  country: string;
  additionalInfo?: string;  // Adicionando AdditionalInfo
  userId: string;  // Este agora representa o UserId
  type: PropertyType;
  size: number;
  bedrooms: number;
  bathrooms: number;
  price: number;
  amenities?: string;  // Tornado opcional para corresponder ao backend
  status: PropertyStatus;
  yearBuilt?: string;  // Adicionando YearBuilt
  ownerInformation?: string;  // Adicionando OwnerInformation
  heatingSystem?: string;  // Adicionando HeatingSystem
  coolingSystem?: string;  // Adicionando CoolingSystem
  images?: PropertyImage[];
}

export interface PropertyImage {
  id: string;
  propertyId: string;
  fileName: string;
  imagePath?: string;
  file?: File;
  isMain: boolean;  // Adicionando isMain para controlar a imagem principal
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
