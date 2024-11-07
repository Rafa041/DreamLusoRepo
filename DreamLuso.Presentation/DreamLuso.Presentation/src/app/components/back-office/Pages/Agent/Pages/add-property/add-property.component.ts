import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PropertyService } from '../../../../../../services/PropertyService/property.service';
import { StateService } from '../../../../../../services/StateService/state.service';
import { Property, PropertyImage, PropertyStatus, PropertyType } from '../../../../../../models/property';

@Component({
  selector: 'app-add-property',
  templateUrl: './add-property.component.html',
  styleUrl: './add-property.component.scss'
})
export class AddPropertyComponent implements OnInit {
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
  addPropertySuccess: boolean = false;
  addPropertyFailure: boolean = false;
  userId: string = '';

  selectedImages: string[] = [];
  defaultImage: string = 'https://images.pexels.com/photos/1732414/pexels-photo-1732414.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2';
  currentImageIndex: number = 0;
  private autoSlideInterval: any;
  autoSlideDelay: number = 5000; // 5 seconds

  constructor(
    private fb: FormBuilder,
    private propertyService: PropertyService,
    private router: Router,
    private stateService: StateService
  ) {}

  ngOnInit() {
    this.getLoggedUserId();
    this.initForm();
    this.loadCountries();
    this.startAutoSlide(); // Start auto-sliding
  }

  getLoggedUserId() {
    const loggedUserString = sessionStorage.getItem('loggedUser');
    if (loggedUserString) {
      const loggedUser = JSON.parse(loggedUserString);
      this.userId = loggedUser.id;
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
      userId: [this.userId, Validators.required],
      amenities: this.fb.array([], Validators.required),
      images: [[], Validators.required],
      additionalInfo: ['', Validators.nullValidator],
      yearBuilt: ['', [Validators.required, this.yearBuiltValidator]]
    });
  }

  onSubmit() {
    if (this.propertyForm.valid) {
      const propertyData: Property = this.propertyForm.value;

      propertyData.yearBuilt = new Date(propertyData.yearBuilt).toISOString();
      console.log(this.propertyForm)
      this.propertyService.createProperty(propertyData).subscribe({
        next: (response) => {
          console.log('Success!', response);
          this.addPropertySuccess = true;
          setTimeout(() => {
            this.router.navigateByUrl('/');
          }, 2000);
        },
        error: (error) => {
          console.error('There was an error!', error);
          this.addPropertyFailure = true;
        }
      });
    }
     else {
      Object.keys(this.propertyForm.controls).forEach(key => {
        const controlErrors = this.propertyForm.get(key)?.errors;
        if (controlErrors) {
          console.log('Error in control: ' + key, controlErrors);
        }
      });
      console.log('Form is invalid', this.propertyForm.errors);
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

    // Clear existing selected images
    this.selectedImages = [];

    // Convert each uploaded file to base64 string
    images.forEach(image => {
      if (image.file) {
        const reader = new FileReader();
        reader.onload = (e: any) => {
          this.selectedImages.push(e.target.result);
          // Set first image as current when images are first loaded
          if (this.selectedImages.length === 1) {
            this.currentImageIndex = 0;
          }
        };
        reader.readAsDataURL(image.file as Blob);
      }
    });
  }

  nextImage() {
    if (this.currentImageIndex < this.selectedImages.length - 1) {
      this.currentImageIndex++;
      this.resetAutoSlide();
    }
  }

  previousImage() {
    if (this.currentImageIndex > 0) {
      this.currentImageIndex--;
      this.resetAutoSlide();
    }
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
