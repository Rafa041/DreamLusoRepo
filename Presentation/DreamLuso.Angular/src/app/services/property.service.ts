import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Property, PropertyImage } from "../models/property";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  private apiUrlCreate = 'https://localhost:7224/api/property/register';

  constructor(private httpClient: HttpClient) {}

  createProperty(propertyData: Property): Observable<any> {
    const formData = new FormData();

    // Adicione cada campo ao FormData
    formData.append('Title', propertyData.title);
    formData.append('Description', propertyData.description);
    formData.append('Street', propertyData.street);
    formData.append('City', propertyData.city);
    formData.append('State', propertyData.state);
    formData.append('PostalCode', propertyData.postalCode);
    formData.append('Country', propertyData.country);
    formData.append('AdditionalInfo', propertyData.additionalInfo || 'N/A');
    formData.append('UserId', propertyData.userId);
    formData.append('Type', propertyData.type);
    formData.append('Size', propertyData.size.toString());
    formData.append('Bedrooms', propertyData.bedrooms.toString());
    formData.append('Bathrooms', propertyData.bathrooms.toString());
    formData.append('Price', propertyData.price.toString());
    formData.append('Amenities', propertyData.amenities || 'N/A');
    formData.append('Status', propertyData.status);
    formData.append('YearBuilt', propertyData.yearBuilt || 'N/A');
    formData.append('OwnerInformation', propertyData.ownerInformation || 'N/A');
    formData.append('HeatingSystem', propertyData.heatingSystem || 'N/A');
    formData.append('CoolingSystem', propertyData.coolingSystem || 'N/A');

    // Adicione as imagens, se houver
    if (propertyData.images && propertyData.images.length > 0) {
      propertyData.images.forEach((image: PropertyImage, index: number) => {
        if (image.file && image.file instanceof File) {
          formData.append(`Images`, image.file, image.fileName);
        } else {
          console.warn(`Image at index ${index} is not a valid File object`);
        }
      });
    }

    console.log('FormData before sending:', formData);

    return this.httpClient.post(`${this.apiUrlCreate}`, formData);
  }

}

