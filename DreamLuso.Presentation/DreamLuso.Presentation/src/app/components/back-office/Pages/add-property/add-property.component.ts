import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PropertyService } from '../../../../services/PropertyService/property.service';
import { StateService } from '../../../../services/StateService/state.service';
import { Property, PropertyImage, PropertyStatus, PropertyType } from '../../../../models/property';

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
  onImagesChanged(images: PropertyImage[]) {
    this.propertyForm.patchValue({ images });
    this.propertyForm.get('images')?.updateValueAndValidity();
  }
  /*
  onFileSelected(event: Event) {
    const element = event.target as HTMLInputElement;
    const fileList: FileList | null = element.files;

    if (fileList) {
      const newImages: PropertyImage[] = Array.from(fileList).map((file, index) => ({
        id: '',
        propertyId: '',
        fileName: file.name,
        file: file
      }));

      const currentImages = this.propertyForm.get('images')?.value || [];
      this.propertyForm.patchValue({
        images: [...currentImages, ...newImages]
      });
      this.propertyForm.get('images')?.updateValueAndValidity();
      this.previewImages();
    }
  }

  previewImages() {
    const uploadedImagesDiv = document.getElementById('uploadedImages');
    if (uploadedImagesDiv) {
      uploadedImagesDiv.innerHTML = '';

      const images = this.propertyForm.get('images')?.value || [];
      images.forEach((image: PropertyImage, index: number) => {
        const reader = new FileReader();
        reader.onload = (e: any) => {
          const imgElement = document.createElement('img');
          imgElement.src = e.target.result;
          imgElement.alt = image.fileName;
          imgElement.classList.add('w-32', 'h-32', 'object-cover', 'rounded-lg', 'mr-2', 'mb-2');

          const removeButton = document.createElement('button');
          removeButton.textContent = 'Remove';
          removeButton.onclick = () => this.removeImage(index);
          removeButton.classList.add('mt-1', 'px-2', 'py-1', 'bg-red-500', 'text-white', 'rounded');

          const imageContainer = document.createElement('div');
          imageContainer.classList.add('inline-block', 'text-center');
          imageContainer.appendChild(imgElement);
          imageContainer.appendChild(removeButton);

          uploadedImagesDiv.appendChild(imageContainer);
        };
        reader.readAsDataURL(image.file as Blob);
      });
    }
  }

  removeImage(index: number) {
    const images = this.propertyForm.get('images')?.value || [];
    images.splice(index, 1);
    this.propertyForm.patchValue({ images });
    this.propertyForm.get('images')?.updateValueAndValidity();
    this.previewImages();
  }*/

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
