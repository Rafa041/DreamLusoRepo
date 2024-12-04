import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Property, PropertyImage, PropertyStatus, PropertyType } from '../../../../../../models/property';
import { PropertyService } from '../../../../../../services/PropertyService/property.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StateService } from '../../../../../../services/StateService/state.service';
import { environment } from '../../../../../../../../environment';
import { UserService } from '../../../../../../services/UserService/user.service';
import { UserModel } from '../../../../../../models/UserModel';

@Component({
  selector: 'app-update-property',
  templateUrl: './update-property.component.html',
  styleUrl: './update-property.component.scss'
})
export class UpdatePropertyComponent implements OnInit {
  propertyForm!: FormGroup;
  propertyId: string = '';
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
  isSubmitting: boolean = false;
  userId: string = '';
  loggedUserDetails: UserModel | null = null;

  constructor(
    private fb: FormBuilder,
    private propertyService: PropertyService,
    private router: Router,
    private route: ActivatedRoute,
    private stateService: StateService,
    private userService: UserService  // Add UserService
  ) {}

  ngOnInit() {
    const loggedUserString = sessionStorage.getItem('loggedUser');
    if (loggedUserString) {
      const loggedUser = JSON.parse(loggedUserString);
      this.userId = loggedUser.id;

      this.userService.retrieve(this.userId).subscribe({
        next: (userDetails: UserModel) => {
          this.loggedUserDetails = userDetails;
        },
        error: (error) => {
          console.error('Error fetching user details:', error);
        }
      });
    }

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
      additionalInfo: ['']
    });
  }

  loadCountries() {
    this.stateService.getCountries().subscribe({
      next: (data) => {
        this.countries = data.map((country: any) => country.name.common).sort();
      },
      error: (error) => {
        console.error('Error loading countries:', error);
      }
    });
  }

  loadProperty() {
    this.propertyId = this.route.snapshot.params['id'];
    this.propertyService.retrieve(this.propertyId).subscribe({
      next: (property: Property) => {
        const amenitiesFormArray = this.propertyForm.get('amenities') as FormArray;
        while (amenitiesFormArray.length) {
          amenitiesFormArray.removeAt(0);
        }

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
          additionalInfo: property.additionalInfo
        });

        if (property.amenities) {
          this.selectedAmenities = Array.isArray(property.amenities)
            ? property.amenities
            : (property.amenities as string).split(',').map((item: string) => item.trim());

          this.selectedAmenities.forEach(amenity => {
            amenitiesFormArray.push(this.fb.control(amenity));
          });
        }

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
    if (this.propertyForm.valid && this.userId) {
      this.isSubmitting = true;
      const updatedProperty: Property = {
        ...this.propertyForm.value,
        id: this.propertyId,
        amenities: this.selectedAmenities,
        userId: this.userId
      };

      this.propertyService.updateProperty(this.propertyId, updatedProperty).subscribe({
        next: (response) => {
          console.log('Property updated successfully', response);
          this.router.navigate(['/propertyId', this.propertyId]); // Correct path based on your routing structure
        },
        error: (error) => {
          console.error('Error updating property:', error);
          this.isSubmitting = false;
        },
        complete: () => {
          this.isSubmitting = false;
        }
      });
    } else {
      this.propertyForm.markAllAsTouched();
      alert('Please fill in all required fields.');
    }
  }

  startAutoSlide() {
    this.autoSlideInterval = setInterval(() => {
      if (this.selectedImages.length > 1) {
        this.currentImageIndex = (this.currentImageIndex + 1) % this.selectedImages.length;
      }
    }, this.autoSlideDelay);
  }

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

  nextImage() {
    if (this.selectedImages.length > 1) {
      this.currentImageIndex = (this.currentImageIndex + 1) % this.selectedImages.length;
      this.resetAutoSlide();
    }
  }

  previousImage() {
    if (this.selectedImages.length > 1) {
      this.currentImageIndex = this.currentImageIndex === 0
        ? this.selectedImages.length - 1
        : this.currentImageIndex - 1;
      this.resetAutoSlide();
    }
  }

  getImageUrl(imageUrl: string): string {
    if (!imageUrl) return this.defaultImage;
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
}
