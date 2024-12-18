import { Component, OnInit } from '@angular/core';
import { Property, PropertyStatus, PropertyType } from '../../../../models/property';
import { PropertyService } from '../../../../services/PropertyService/property.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-properties-rent',
  templateUrl: './properties-rent.component.html',
  styleUrl: './properties-rent.component.scss'
})
export class PropertiesRentComponent implements OnInit {
  properties: Property[] = [];
  filteredProperties: Property[] = [];
  PropertyType = PropertyType;
  loading: boolean = true;
  errorMessage: string = '';
  searchTerm: string = '';

  filters = {
    type: '',
    minPrice: null as number | null,
    maxPrice: null as number | null,
    bedrooms: null as number | null,
    city: ''
  };

  constructor(
    private propertyService: PropertyService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadRentalProperties();
  }

  loadRentalProperties(): void {
    this.loading = true;
    this.propertyService.retrieveAll().subscribe({
      next: (properties: Property[]) => {
        this.properties = properties.filter(property =>
          property.status === PropertyStatus.ForRent &&
          property.isActive === true
        );
        this.filteredProperties = this.properties;
        this.loading = false;
      },
      error: (error) => {
        this.errorMessage = 'Error loading rental properties';
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
      const matchesMinPrice = !this.filters.minPrice || property.price >= this.filters.minPrice;
      const matchesMaxPrice = !this.filters.maxPrice || property.price <= this.filters.maxPrice;
      const matchesBedrooms = !this.filters.bedrooms || property.bedrooms >= this.filters.bedrooms;
      const matchesCity = !this.filters.city || property.city.toLowerCase().includes(this.filters.city.toLowerCase());

      return matchesSearch && matchesType && matchesMinPrice &&
             matchesMaxPrice && matchesBedrooms && matchesCity;
    });
  }

  onSearch(event: any): void {
    this.searchTerm = event.target.value;
    this.applyFilters();
  }

  getImageUrl(property: Property): string {
    if (property.images && property.images.length > 0 && property.images[0].imagePath) {
      return property.images[0].imagePath;
    }
    return 'assets/default-property-image.jpg';
  }

  formatPrice(price: number): string {
    return new Intl.NumberFormat('pt-PT', {
      style: 'currency',
      currency: 'EUR'
    }).format(price);
  }

  handleImageError(event: any): void {
    event.target.src = 'assets/default-property-image.jpg';
  }
}
