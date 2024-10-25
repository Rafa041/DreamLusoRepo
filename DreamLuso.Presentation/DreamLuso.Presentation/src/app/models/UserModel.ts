import { Access } from "./Access";

export interface UserModel {
  id: string;
  access: Access;
  imageUrl: string;
  firstName: string;
  lastName: string;
  phoneNumber?: number;
  email: string;
  upload: string;
}
