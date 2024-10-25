import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Property, PropertyImage } from "../models/property";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class PropertyService {
private apiUrl = 'https://localhost:7224/api/property';

constructor(private httpClient: HttpClient) {}

createProperty(formData: FormData): Observable<any> {
  return this.httpClient.post(`${this.apiUrl}/register`, formData);
}
}
