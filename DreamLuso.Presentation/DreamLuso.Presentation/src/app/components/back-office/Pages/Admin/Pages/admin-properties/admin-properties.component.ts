import { Component, NgZone, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Property, PropertyType, PropertyStatus } from '../../../../../../models/property';
import { UserModel } from '../../../../../../models/UserModel';
import { PropertyService } from '../../../../../../services/PropertyService/property.service';

@Component({
  selector: 'app-admin-properties',
  templateUrl: './admin-properties.component.html',
  styleUrl: './admin-properties.component.scss'
})
export class AdminPropertiesComponent implements OnInit {
  loggedUserDetails: UserModel | null = null;
  properties: Property[] = [];
  loading: boolean = true;
  errorMessage: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 8;

  constructor(
    private propertiesService: PropertyService,
    private zone: NgZone
  ) {}

  ngOnInit() {
    const loggedUserString = sessionStorage.getItem('loggedUser');
    if (loggedUserString) {
      this.loggedUserDetails = JSON.parse(loggedUserString);
      this.loadAllProperties();
    }
  }

  loadAllProperties(): void {
    this.loading = true;
    this.propertiesService.retrieveAll().subscribe({
      next: (response: Property[]) => {
        this.zone.run(() => {
          this.properties = response;
          this.loading = false;
        });
      },
      error: (error) => {
        this.errorMessage = "Error loading properties.";
        this.loading = false;
      }
    });
  }

  getPaginatedProperties(): Property[] {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.properties.slice(startIndex, startIndex + this.itemsPerPage);
  }

  get totalPages(): number {
    return Math.ceil(this.properties.length / this.itemsPerPage);
  }

  getImageUrl(fileName: string | undefined): string {
    return fileName || 'path/to/default/image.jpg';
  }

  handleImageError(event: any) {
    event.target.src = 'path/to/default/image.jpg';
  }
}
