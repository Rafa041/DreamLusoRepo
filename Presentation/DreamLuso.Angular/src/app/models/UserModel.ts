export interface UserModel {
  id: string;
  access: number;
  imageUrl: string;
  firstName: string;
  lastName: string;
  phoneNumber?: number;
  email: string;
  upload: string;
}
