import { Access } from "./Access";

export interface RetrieveUserResponse {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
  access: Access;
  imageUrl?: string;
  phoneNumber?: string;
  dateOfBirth?: Date;
}

export interface RetrieveAllUsersResponse {
  users: RetrieveUserResponse[];
}
