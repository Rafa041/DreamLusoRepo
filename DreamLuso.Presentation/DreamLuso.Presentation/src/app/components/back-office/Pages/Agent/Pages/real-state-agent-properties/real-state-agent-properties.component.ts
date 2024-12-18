import { Component, NgZone, OnInit } from '@angular/core';
import { UserModel } from '../../../../../../models/UserModel';
import { Property, PropertyStatus, PropertyType } from '../../../../../../models/property';
import { PropertyService } from '../../../../../../services/PropertyService/property.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-real-state-agent-properties',
  templateUrl: './real-state-agent-properties.component.html',
  styleUrl: './real-state-agent-properties.component.scss'
})
export class RealStateAgentPropertiesComponent implements OnInit {
  loggedUserDetails: UserModel | null = null;
  properties: Property[] = [];
  filteredProperties: Property[] = [];
  loading: boolean = true;
  errorMessage: string = '';

  PropertyType = PropertyType;
  PropertyStatus = PropertyStatus;
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 6; // Reduced for better visual presentation

  constructor(
    private propertiesService: PropertyService,
    private router: Router,
    private zone: NgZone
  ) {}

  ngOnInit() {
    const loggedUserString = sessionStorage.getItem('loggedUser');
    if (loggedUserString) {
      this.loggedUserDetails = JSON.parse(loggedUserString);
      this.loadAgentProperties();
    }
  }

  loadAgentProperties(): void {
    this.loading = true;
    if (this.loggedUserDetails?.id) {
      this.propertiesService.getPropertiesByAgentId(this.loggedUserDetails.id).subscribe({
        next: (response: Property[]) => {
          this.zone.run(() => {
            this.properties = response;
            this.applyFilters();
            this.loading = false;
          });
        },
        error: (error) => {
          this.errorMessage = "Error loading properties.";
          this.loading = false;
        }
      });
    }
  }

  applyFilters(): void {
    this.filteredProperties = this.properties.filter(property => {
      const matchesSearch = !this.searchTerm ||
        property.city.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        property.title.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        property.postalCode.includes(this.searchTerm);

      return matchesSearch;
    });
  }

  onSearch(event: any): void {
    this.searchTerm = event.target.value;
    this.currentPage = 1; // Reset to first page on search
    this.applyFilters();
  }

  getPaginatedProperties(): Property[] {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.filteredProperties.slice(startIndex, startIndex + this.itemsPerPage);
  }

  get totalPages(): number {
    return Math.ceil(this.filteredProperties.length / this.itemsPerPage);
  }

  getImageUrl(fileName: string | undefined): string {
    return fileName || 'https://images.pexels.com/photos/259588/pexels-photo-259588.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1';
  }

  handleImageError(event: any) {
    event.target.src = 'https://images.pexels.com/photos/259588/pexels-photo-259588.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1';
  }

  onEdit(propertyId: string | undefined): void {
    this.router.navigate(['/back-office/agent/properties/edit', propertyId!]);
}

  onDeactivate(propertyId: string): void {
    // Implement deactivation logic
  }
  onAddNewProperty(): void {
    this.router.navigate(['/back-office/agent/create-property']);
  }
}
