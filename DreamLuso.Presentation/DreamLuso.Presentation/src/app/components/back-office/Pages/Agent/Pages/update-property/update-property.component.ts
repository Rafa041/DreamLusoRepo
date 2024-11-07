import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Property, PropertyImage, PropertyStatus, PropertyType } from '../../../../../../models/property';
import { PropertyService } from '../../../../../../services/PropertyService/property.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StateService } from '../../../../../../services/StateService/state.service';
import { environment } from '../../../../../../../../environment';

@Component({
  selector: 'app-update-property',
  templateUrl: './update-property.component.html',
  styleUrl: './update-property.component.scss'
})
export class UpdatePropertyComponent implements OnInit {
  propertyId: string = '';
  propertyForm!: FormGroup;
  propertyTypes = Object.values(PropertyType);
  propertyStatuses = Object.values(PropertyStatus);
  selectedAmenities: string[] = [];
  availableAmenities: string[] = [
    'Swimming Pool', 'Gym', 'Playground', 'Parking', 'Garden', 'Elevator',
    'Security System', 'Concierge Service', 'Balcony', 'Terrace', 'Rooftop Deck',
    'Central Air Conditioning', 'In-unit Laundry', 'Walk-in Closet', 'Fireplace',
    'Hardwood Floors', 'Stainless Steel Appliances', 'Granite Countertops',
    'High Ceilings', 'Pet-friendly', 'Storage Space', 'Bike Storage',
    'EV Charging Station', 'Smart Home Features', 'Fitness Center', 'Sauna',
    'Spa', 'Tennis Court', 'Basketball Court', 'Clubhouse', 'Business Center',
    'Conference Room', 'Movie Theater', 'Game Room', 'Library', 'Wine Cellar',
    'Outdoor Kitchen', 'BBQ Area', 'Fire Pit', 'Jogging Trail', 'Dog Park',
    'Playroom', 'On-site Maintenance', '24/7 Security', 'Gated Community',
    'Waterfront View', 'Mountain View', 'City View'
  ];
  countries: string[] = [];
  selectedImages: string[] = [];
  currentImageIndex: number = 0;
  private autoSlideInterval: any;
  autoSlideDelay: number = 5000;
  defaultImage: string = 'https://images.pexels.com/photos/1732414/pexels-photo-1732414.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2';
  apiUrl = environment.apiUrl;

  get amenitiesFormArray() {
    return this.propertyForm.get('amenities') as FormArray;
  }

  constructor(
    private fb: FormBuilder,
    private propertyService: PropertyService,
    private router: Router,
    private route: ActivatedRoute,
    private stateService: StateService
  ) {}

  ngOnInit() {
    this.initForm();
    this.loadCountries();
    this.loadProperty();
    this.startAutoSlide();
  }

  initForm() {
    this.propertyForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      street: ['', Validators.required],
      city: ['', Validators.required],
      state: ['', Validators.required],
      postalCode: ['', Validators.required],
      country: ['', Validators.required],
      type: ['', Validators.required],
      size: [null, [Validators.required, Validators.min(0)]],
      bedrooms: [null, [Validators.required, Validators.min(0)]],
      bathrooms: [null, [Validators.required, Validators.min(0)]],
      price: [null, [Validators.required, Validators.min(0)]],
      status: ['', Validators.required],
      amenities: this.fb.array([]),
      images: [[]],
      additionalInfo: [''],
      yearBuilt: ['', [Validators.required, this.yearBuiltValidator]]
    });
  }
  loadProperty() {
    this.propertyId = this.route.snapshot.params['id'];

    this.propertyService.retrieve(this.propertyId).subscribe({
      next: (property: Property) => {
        console.log('Full Property Data:', property);

        // Clear existing amenities
        const amenitiesFormArray = this.propertyForm.get('amenities') as FormArray;
        while (amenitiesFormArray.length) {
          amenitiesFormArray.removeAt(0);
        }

        // Load property data
        this.propertyForm.patchValue({
          title: property.title,
          description: property.description,
          street: property.street,
          city: property.city,
          state: property.state,
          postalCode: property.postalCode,
          country: property.country,
          type: property.type,
          size: property.size,
          bedrooms: property.bedrooms,
          bathrooms: property.bathrooms,
          price: property.price,
          status: property.status,
          additionalInfo: property.additionalInfo,
          yearBuilt: property.yearBuilt
        });

        // Handle amenities with type checking
        if (property.amenities) {
          if (typeof property.amenities === 'string') {
            this.selectedAmenities = (property.amenities as string).split(',').map((item: string) => item.trim());
          } else if (Array.isArray(property.amenities)) {
            this.selectedAmenities = property.amenities;
          }

          // Add amenities to form array
          this.selectedAmenities.forEach(amenity => {
            amenitiesFormArray.push(this.fb.control(amenity));
          });
        }

        // Load images
        if (property.images) {
          this.selectedImages = property.images.map(img => img.fileName);
          this.propertyForm.patchValue({ images: property.images });
        }
      },
      error: (error) => {
        console.error('Error loading property:', error);
      }
    });
  }

  onSubmit() {
    if (this.propertyForm.valid) {
      const updatedProperty: Property = this.propertyForm.value;

      this.propertyService.updateProperty(this.propertyId, updatedProperty).subscribe({
        next: (response) => {
          console.log('Property updated successfully', response);
          this.router.navigate(['/property', this.propertyId]);
        },
        error: (error) => {
          console.error('Error updating property:', error);
        }
      });
    } else {
      this.propertyForm.markAllAsTouched();
      alert('Please fill in all required fields.');
    }
  }


  yearBuiltValidator(control: AbstractControl): {[key: string]: any} | null {
    const year = new Date(control.value).getFullYear();
    const currentYear = new Date().getFullYear();
    if (isNaN(year) || year < 1800 || year > currentYear) {
      return { 'invalidYear': true };
    }
    return null;
  }


  startAutoSlide() {
    this.autoSlideInterval = setInterval(() => {
      if (this.selectedImages.length > 1) {
        if (this.currentImageIndex < this.selectedImages.length - 1) {
          this.currentImageIndex++;
        } else {
          this.currentImageIndex = 0;
        }
      }
    }, this.autoSlideDelay);
  }

  // Add this to clean up when component is destroyed
  ngOnDestroy() {
    if (this.autoSlideInterval) {
      clearInterval(this.autoSlideInterval);
    }
  }
  onImagesChanged(images: PropertyImage[]) {
    this.propertyForm.patchValue({ images });
    this.propertyForm.get('images')?.updateValueAndValidity();

    this.selectedImages = [];

    images.forEach(image => {
      if (image.file) {
        const reader = new FileReader();
        reader.onload = (e: any) => {
          this.selectedImages.push(e.target.result);
          if (this.selectedImages.length === 1) {
            this.currentImageIndex = 0;
          }
        };
        reader.readAsDataURL(image.file as Blob);
      }
    });
  }

  nextImage(): void {
    if (this.selectedImages?.length) {
      this.currentImageIndex = (this.currentImageIndex + 1) % this.selectedImages.length;
      this.resetAutoSlide();
    }
  }

  previousImage(): void {
    if (this.selectedImages?.length) {
      this.currentImageIndex = this.currentImageIndex === 0
        ? this.selectedImages.length - 1
        : this.currentImageIndex - 1;
      this.resetAutoSlide();
    }
  }

  getImageUrl(imageUrl: string | undefined): string {
    if (!imageUrl) {
      return this.defaultImage;
    }
    return `${this.apiUrl}${imageUrl}`;
  }

  handleImageError(event: any) {
    event.target.src = this.defaultImage;
  }

  resetAutoSlide() {
    if (this.autoSlideInterval) {
      clearInterval(this.autoSlideInterval);
      this.startAutoSlide();
    }
  }

  addAmenity(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    const amenity = selectElement.value;

    if (amenity && !this.selectedAmenities.includes(amenity)) {
      this.selectedAmenities.push(amenity);
      (this.propertyForm.get('amenities') as FormArray).push(this.fb.control(amenity));
    }

    selectElement.value = '';
  }

  removeAmenity(amenity: string) {
    const index = this.selectedAmenities.indexOf(amenity);
    if (index > -1) {
      this.selectedAmenities.splice(index, 1);
      (this.propertyForm.get('amenities') as FormArray).removeAt(index);
    }
  }
  loadCountries() {
    this.stateService.getCountries().subscribe(
      (data) => {
        this.countries = data.map((country: any) => country.name.common).sort();
      },
      (error) => {
        console.error('Error loading countries:', error);
      }
    );
  }
}
