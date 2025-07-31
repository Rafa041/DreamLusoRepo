import { Access } from "./Access";

export interface RealEstateAgentModel {
  id: string;
  officeEmail?: string; // Supondo que este seja opcional
  totalSales: number;
  totalListings: number;
  certifications?: string[];
  languagesSpoken?: string[];

  // Propriedades do Usuário
  firstName: string;
  lastName: string;
  email: string;
  access: Access;
  imageUrl: string;
  phoneNumber?: number; // Opcional
  upload: string;
}
