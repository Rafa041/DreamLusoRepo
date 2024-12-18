import { Component, OnInit } from '@angular/core';
import { Property } from '../../../../models/property';
import { PropertyService } from '../../../../services/PropertyService/property.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-properties-algarve',
  templateUrl: './properties-algarve.component.html',
  styleUrl: './properties-algarve.component.scss'
})
export class PropertiesAlgarveComponent implements OnInit {
  algarveProperties: Property[] = [];
  loading: boolean = true;
  errorMessage: string = '';

  constructor(
    private propertyService: PropertyService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadAlgarveProperties();
  }

  loadAlgarveProperties(): void {
    this.loading = true;
    this.propertyService.retrieveAll().subscribe({
      next: (properties: Property[]) => {
        this.algarveProperties = properties.filter(property =>
          property.isActive === true &&
          property.city?.toLowerCase() === 'algarve'
        );
        this.loading = false;
      },
      error: (error) => {
        this.errorMessage = 'Error loading Algarve properties';
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
