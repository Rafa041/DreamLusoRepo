import { Component, NgZone, OnInit } from '@angular/core';
import { PropertyService } from '../../../../services/PropertyService/property.service';
import { Property, PropertyStatus, PropertyType } from '../../../../models/property';
import { Router } from '@angular/router';
import { environment } from '../../../../../../environment';

@Component({
  selector: 'app-retrieve-all-properties',
  templateUrl: './retrieve-all-properties.component.html',
  styleUrl: './retrieve-all-properties.component.scss'
})
export class RetrieveAllPropertiesComponent implements OnInit{
  properties: Property[] = [];
  filteredProperties: Property[] = [];
  activeFilter: string = 'rent';
  errorMessage: string = '';
  loading: boolean = true;

  // New properties
  PropertyType = PropertyType;
  PropertyStatus = PropertyStatus;
  searchTerm: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 8;

  filters = {
    type: '',
    status: '',
    minPrice: null as number | null,
    maxPrice: null as number | null,
    bedrooms: null as number | null,
    city: '',
    amenities: [] as string[]
  };

  constructor(
    private propertiesService: PropertyService,
    private router: Router,
    private zone: NgZone
  ) {}

  ngOnInit(): void {
    this.loadProperties();
  }

  loadProperties(): void {
    this.loading = true;
    this.propertiesService.retrieveAll().subscribe({
      next: (response: Property[]) => {
        this.zone.run(() => {
          if (response) {
            this.properties = response;
            this.applyFilters();
            this.loading = false;
          } else {
            this.errorMessage = "Invalid data received.";
            this.loading = false;
          }
        });
      },
      error: (error) => {
        this.errorMessage = "Error loading properties.";
        this.loading = false;
      }
    });
  }

  applyFilters(): void {
    this.filteredProperties = this.properties.filter(property => {
      const matchesSearch = !this.searchTerm ||
        property.city.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        property.postalCode.includes(this.searchTerm);

      const matchesType = !this.filters.type || property.type === this.filters.type;
      const matchesStatus = !this.filters.status || property.status === this.filters.status;
      const matchesMinPrice = !this.filters.minPrice || property.price >= this.filters.minPrice;
      const matchesMaxPrice = !this.filters.maxPrice || property.price <= this.filters.maxPrice;

      return matchesSearch && matchesType && matchesStatus &&
             matchesMinPrice && matchesMaxPrice;
    });
  }

  onSearch(event: any): void {
    this.searchTerm = event.target.value;
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
    if (!fileName) {
      return 'https://images.pexels.com/photos/259588/pexels-photo-259588.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1';
    }
    return `${fileName}`;
  }

  handleImageError(event: any) {
    event.target.src = 'https://images.pexels.com/photos/259588/pexels-photo-259588.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1';
  }
}
