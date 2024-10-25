import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Property, PropertyImage, PropertyStatus, PropertyType } from '../../../../models/property';
import { PropertyService } from '../../../../services/property.service';
import { Router } from '@angular/router';
import { StateService } from '../../../../services/state.service';

@Component({
  selector: 'app-add-property',
  templateUrl: './add-property.component.html',
  styleUrl: './add-property.component.scss'
})

  export class AddPropertyComponent implements OnInit {
    propertyForm!: FormGroup;
    propertyTypes = Object.values(PropertyType);
    propertyStatus = Object.values(PropertyStatus);
    selectedAmenities: string[] = [];
    availableAmenities: string[] = [
      'Swimming Pool',
      'Gym',
      'Playground',
      'Parking',
      'Garden',
      'Elevator',
      'Security System',
      'Concierge Service',
      'Balcony',
      'Terrace',
      'Rooftop Deck',
      'Central Air Conditioning',
      'In-unit Laundry',
      'Walk-in Closet',
      'Fireplace',
      'Hardwood Floors',
      'Stainless Steel Appliances',
      'Granite Countertops',
      'High Ceilings',
      'Pet-friendly',
      'Storage Space',
      'Bike Storage',
      'EV Charging Station',
      'Smart Home Features',
      'Fitness Center',
      'Sauna',
      'Spa',
      'Tennis Court',
      'Basketball Court',
      'Clubhouse',
      'Business Center',
      'Conference Room',
      'Movie Theater',
      'Game Room',
      'Library',
      'Wine Cellar',
      'Outdoor Kitchen',
      'BBQ Area',
      'Fire Pit',
      'Jogging Trail',
      'Dog Park',
      'Playroom',
      'On-site Maintenance',
      '24/7 Security',
      'Gated Community',
      'Waterfront View',
      'Mountain View',
      'City View',
    ];
    loggedUserId: string = '';
    selectedFiles: File[] = [];
    countries: string[] = [];
    addPropertySuccess: boolean = false;
    addPropertyFailure: boolean = false;
    isDragging = false;
    imagePreviews: string[] = [];

    constructor(
      private fb: FormBuilder,
      private propertyService: PropertyService,
      private router: Router,
      private stateService: StateService,
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
        this.loggedUserId = loggedUser.id; // Set userId here
      }
    }

    loadCountries() {
      this.stateService.getCountries().subscribe(
        (data) => {
          this.countries = data.map((country: any) => country.name.common).sort();
          console.log('Countries loaded:', this.countries);
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
        userId: [this.loggedUserId, Validators.required], // Use this.userId instead of this.loggedUserId
        amenities: this.fb.array([], Validators.required),
        images: [[], Validators.required]
      });
    }

    onSubmit() {
      console.log('Selected Files:', this.selectedFiles);
      console.log('Form Value:', this.propertyForm.value);

      if (this.propertyForm.valid) {
        const propertyData: Property = {
          ...this.propertyForm.value,
          userId: this.loggedUserId,
          amenities: this.selectedAmenities.join(', '),
        };

        console.log('esta e a propertyData', propertyData);

        const formData = new FormData();
        // Adiciona cada campo do objeto propertyData ao FormData
        for (const key in propertyData) {
          formData.append(key, propertyData[key]);
        }

        // Adiciona os arquivos selecionados ao FormData sob a chave 'images'
        this.selectedFiles.forEach(file => {
          console.log(file.name); // Imprimir os nomes dos arquivos
          formData.append('images', file);
      });

        // Chame createProperty com o FormData
        this.propertyService.createProperty(formData).subscribe({
          next: (response) => {
            console.log('Success!', response);
            this.addPropertySuccess = true;
            setTimeout(() => {
              this.router.navigateByUrl('/dashboard');
            }, 2000);
          },
          error: (error) => {
            console.error('There was an error!', error);
            this.addPropertyFailure = true;
          }
        });
      } else {
        console.log('Form is invalid', this.propertyForm.errors);
        this.propertyForm.markAllAsTouched();
        alert('Please fill in all required fields.');
      }
    }


    onFileSelected(event: any) {
      const files: FileList = event.target.files;
      if (files) {
        this.selectedFiles = [...this.selectedFiles, ...Array.from(files)];
        this.updateImagePreviews();
      }
    }
    updateImagePreviews() {
      const uploadedImagesDiv = document.getElementById('uploadedImages');
      if (uploadedImagesDiv) {
        uploadedImagesDiv.innerHTML = '';
        this.selectedFiles.forEach(file => {
          const reader = new FileReader();
          reader.onload = (e: any) => {
            const imgElement = document.createElement('img');
            imgElement.src = e.target.result;
            imgElement.alt = file.name;
            imgElement.classList.add('w-32', 'h-32', 'object-cover', 'rounded-lg', 'mr-2', 'mb-2');
            uploadedImagesDiv.appendChild(imgElement);
          };
          reader.readAsDataURL(file);
        });
      }
    }
    get amenities(): FormArray {
      return this.propertyForm.get('amenities') as FormArray;
    }

    addAmenity(event: Event): void {
      const selectElement = event.target as HTMLSelectElement;
      const amenity = selectElement.value;

      if (amenity && !this.selectedAmenities.includes(amenity)) {
        this.selectedAmenities.push(amenity);
        this.amenities.push(this.fb.control(amenity));
      }

      selectElement.value = '';
    }

    removeAmenity(amenity: string): void {
      const index = this.selectedAmenities.indexOf(amenity);
      if (index > -1) {
        this.selectedAmenities.splice(index, 1);
        this.amenities.removeAt(index);
      }
    }
    onDragOver(event: DragEvent) {
      event.preventDefault();
      event.stopPropagation();
      this.isDragging = true;
    }

    onDragLeave(event: DragEvent) {
      event.preventDefault();
      event.stopPropagation();
      this.isDragging = false;
    }

    onDrop(event: DragEvent) {
      event.preventDefault();
      event.stopPropagation();
      this.isDragging = false;
      const files = event.dataTransfer?.files;
      if (files) {
        this.selectedFiles = [...this.selectedFiles, ...Array.from(files)];
        this.updateImagePreviews();
      }
    }
    onImageChange(event: Event) {
      const input = event.target as HTMLInputElement; // Assert the type of target
      const files: FileList | null = input.files;

      if (files) {
        this.selectedFiles = [...this.selectedFiles, ...Array.from(files)]; // Update selected files

        this.imagePreviews = []; // Clear previous previews
        for (let file of this.selectedFiles) {
          if (file.type.startsWith('image/')) {
            const reader = new FileReader();
            reader.onload = (e: any) => {
              this.imagePreviews.push(e.target.result);
            };
            reader.readAsDataURL(file);
          }
        }
        // Update the images FormControl with the selected files
        this.propertyForm.patchValue({
          images: this.selectedFiles // This is an array of File objects
        });
    }
  }
}
