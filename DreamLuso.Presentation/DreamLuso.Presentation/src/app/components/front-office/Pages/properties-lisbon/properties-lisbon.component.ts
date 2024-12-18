import { Component, OnInit } from '@angular/core';
import { Property } from '../../../../models/property';
import { Router } from '@angular/router';
import { PropertyService } from '../../../../services/PropertyService/property.service';

@Component({
  selector: 'app-properties-lisbon',
  templateUrl: './properties-lisbon.component.html',
  styleUrl: './properties-lisbon.component.scss'
})
export class PropertiesLisbonComponent implements OnInit {
  lisbonProperties: Property[] = [];
  loading: boolean = true;
  errorMessage: string = '';

  constructor(
    private propertyService: PropertyService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadLisbonProperties();
  }

  loadLisbonProperties(): void {
    this.loading = true;
    this.propertyService.retrieveAll().subscribe({
      next: (properties: Property[]) => {
        this.lisbonProperties = properties.filter(property =>
          property.isActive === true &&
          property.city?.toLowerCase() === 'lisboa'
        );
        this.loading = false;
      },
      error: (error) => {
        this.errorMessage = 'Error loading Lisboa properties';
        this.loading = false;
      }
    });
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
