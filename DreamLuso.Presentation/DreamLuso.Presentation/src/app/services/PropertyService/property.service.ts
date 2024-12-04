import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Property, PropertyImage } from '../../models/property';
import { environment } from '../../../../environment';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {
  private apiUrl = `${environment.apiUrl}/api/Property`;

  constructor(private httpClient: HttpClient) {}

  createProperty(propertyData: Property): Observable<any> {
    const formData = new FormData();

    // Add property data to FormData
    formData.append('Title', propertyData.title);
    formData.append('Description', propertyData.description);
    formData.append('Street', propertyData.street);
    formData.append('City', propertyData.city);
    formData.append('State', propertyData.state);
    formData.append('PostalCode', propertyData.postalCode);
    formData.append('Amenities', propertyData.amenities.toString());
    formData.append('Country', propertyData.country);
    formData.append('AdditionalInfo', propertyData.additionalInfo || 'N/A');
    formData.append('UserId', propertyData.userId);
    formData.append('Type', propertyData.type);
    formData.append('Size', propertyData.size.toString());
    formData.append('Bedrooms', propertyData.bedrooms.toString());
    formData.append('Bathrooms', propertyData.bathrooms.toString());
    formData.append('Price', propertyData.price.toString());
    formData.append('Status', propertyData.status);

    // Handle amenities
    if (Array.isArray(propertyData.amenities)) {
      propertyData.amenities.forEach((amenity, index) => {
        formData.append(`Amenities[${index}]`, amenity);
      });
    }

    // Add images to FormData
    if (propertyData.images && propertyData.images.length > 0) {
      propertyData.images.forEach((image: PropertyImage, index: number) => {
        if (image.file && image.file instanceof File) {
          formData.append(`Images`, image.file, image.fileName);
        } else {
          console.warn(`Image at index ${index} is not a valid File object`);
        }
      });
    }

    console.log(formData)
    return this.httpClient.post(`${this.apiUrl}/Register`, formData);
  }

  retrieve(id: string): Observable<Property> {
    return this.httpClient.get<Property>(`${this.apiUrl}/retrieve/${id}`);
  }

  retrieveAll(): Observable<Property[]> {
    return this.httpClient.get<Property[]>(`${this.apiUrl}/retrieveall`);
  }
  getPropertiesByAgentId(agentId: string): Observable<Property[]> {
    return this.httpClient.get<Property[]>(`${this.apiUrl}/agent/${agentId}`);
  }
  updateProperty(id: string, propertyData: Property): Observable<any> {
    const formData = new FormData();

    // Handle amenities properly
    if (typeof propertyData.amenities === 'string') {
        formData.append('Amenities', propertyData.amenities);
    } else if (Array.isArray(propertyData.amenities)) {
        formData.append('Amenities', propertyData.amenities.join(','));
    }

    // Handle other form fields
    Object.keys(propertyData).forEach(key => {
        if (key === 'images') {
            if (propertyData.images && propertyData.images.length > 0) {
                propertyData.images.forEach((image: PropertyImage, index: number) => {
                    if (image.file && image.file instanceof File) {
                        formData.append('Images', image.file, image.fileName);
                    }
                });
            }
        } else if (key !== 'amenities') {  // Skip amenities as we handled it above
            formData.append(key, propertyData[key]?.toString() || '');
        }
    });

    return this.httpClient.put(`${this.apiUrl}/${id}`, formData);
}
  updatePropertyIsActive(id: string, isActive: boolean): Observable<any> {
    return this.httpClient.patch(`${this.apiUrl}/${id}/active`, { id, isActive });
}
}
