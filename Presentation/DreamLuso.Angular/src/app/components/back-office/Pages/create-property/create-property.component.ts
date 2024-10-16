import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Property, PropertyImage, PropertyStatus, PropertyType } from '../../../../models/property';
import { PropertyService } from '../../../../services/property.service';
import { Router } from '@angular/router';
import { StateService } from '../../../../services/state.service';

@Component({
  selector: 'app-create-property',
  templateUrl: './create-property.component.html',
  styleUrl: './create-property.component.scss'
})
export class CreatePropertyComponent implements OnInit {
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
  loggedUserId: string = '';
  uploadedImages: string[] = [];
  selectedFiles: File[] = [];
  imageMetadatas: PropertyImage[] = [];
  countries: string[] = [];
  addpropertySuccess: boolean = false;
  addpropertyFailure: boolean = false;
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
      yearBuilt: ['', Validators.nullValidator],
      ownerInformation: ['', Validators.nullValidator],
      heatingSystem: ['', Validators.nullValidator],
      coolingSystem: ['', Validators.nullValidator],
    });
  }
  onSubmit() {
    if (this.propertyForm.valid) {
      const propertyData: Property = {
        title: this.propertyForm.get('title')?.value,
        description: this.propertyForm.get('description')?.value,
        street: this.propertyForm.get('street')?.value,
        city: this.propertyForm.get('city')?.value,
        state: this.propertyForm.get('state')?.value,
        postalCode: this.propertyForm.get('postalCode')?.value,
        country: this.propertyForm.get('country')?.value,
        additionalInfo: this.propertyForm.get('additionalInfo')?.value || 'N/A',
        userId: this.loggedUserId,
        type: this.propertyForm.get('type')?.value,
        size: this.propertyForm.get('size')?.value,
        bedrooms: this.propertyForm.get('bedrooms')?.value,
        bathrooms: this.propertyForm.get('bathrooms')?.value,
        price: this.propertyForm.get('price')?.value,
        amenities: this.selectedAmenities.join(', '),
        status: this.propertyForm.get('status')?.value,
        images: this.imageMetadatas.filter(img => img.file instanceof File),
        yearBuilt: this.propertyForm.get('yearBuilt')?.value,
        ownerInformation: this.propertyForm.get('ownerInformation')?.value,
        heatingSystem: this.propertyForm.get('heatingSystem')?.value|| 'N/A',
        coolingSystem: this.propertyForm.get('coolingSystem')?.value|| 'N/A',
      };

      console.log('Property data before submission:', propertyData);

      this.propertyService.createProperty(propertyData).subscribe({
        next: (response) => {
          console.log('Success!', response);
          this.addpropertySuccess = true;
          setTimeout(() => {
            this.router.navigateByUrl('/dashboard');
          }, 2000);
        },
        error: (error) => {
          console.error('There was an error!', error);
          this.addpropertyFailure = true;
        }
      });
    } else {
      console.log('Form is invalid', this.propertyForm.errors);
      this.propertyForm.markAllAsTouched();
      alert('Please fill in all required fields.');
    }
  }

  onFileSelected(event: Event) {
    const element = event.target as HTMLInputElement;
    const fileList: FileList | null = element.files;

    if (fileList && fileList.length > 0) {
        this.selectedFiles = [...this.selectedFiles, ...Array.from(fileList)];

        // Mapeando os arquivos para a estrutura PropertyImage
        this.imageMetadatas = this.selectedFiles.map((file, index) => ({
            id: '',  // Se o backend não retorna um ID no upload, pode ficar vazio
            propertyId: '', // Isso pode ser preenchido após o upload
            fileName: file.name,
            imagePath: '', // Se o caminho for gerado no backend, pode ficar vazio
            file: file, // O próprio arquivo
            isMain: index === 0 // Marca o primeiro arquivo como principal
        }));

        console.log('Selected Files:', this.imageMetadatas);

        // Atualizando o controle do formulário
        this.propertyForm.patchValue({ images: this.imageMetadatas });
        this.propertyForm.get('images')?.updateValueAndValidity();

        // Limpar a pré-visualização antiga
        this.previewImages();
    } else {
        console.log('No files selected');
    }
}

previewImages() {
    const uploadedImagesDiv = document.getElementById('uploadedImages');
    if (uploadedImagesDiv) {
        uploadedImagesDiv.innerHTML = ''; // Limpa as imagens anteriores

        // Criar a pré-visualização para todas as imagens selecionadas
        this.selectedFiles.forEach(file => {
            const reader = new FileReader();
            reader.onload = (e: any) => {
                const imgElement = document.createElement('img');
                imgElement.src = e.target.result; // URL da imagem
                imgElement.alt = file.name;
                imgElement.classList.add('w-32', 'h-32', 'object-cover', 'rounded-lg', 'mr-2', 'mb-2'); // Classes CSS
                uploadedImagesDiv.appendChild(imgElement);
            };
            reader.readAsDataURL(file); // Ler o arquivo como URL
        });
    }
  }
  removeImage(index: number) {
    this.uploadedImages.splice(index, 1);
    this.imageMetadatas.splice(index, 1);

    // Se a imagem principal for removida, a próxima será a principal
    if (this.imageMetadatas.length > 0 && !this.imageMetadatas.some(img => img.isMain)) {
      this.imageMetadatas[0].isMain = true;
    }

    // Atualiza o formulário
    this.propertyForm.patchValue({ images: this.imageMetadatas });
    this.propertyForm.get('images')?.updateValueAndValidity();
  }

  setMainImage(index: number) {
    this.imageMetadatas.forEach((img, i) => {
      img.isMain = i === index;
    });

    // Atualiza o formulário
    this.propertyForm.patchValue({ images: this.imageMetadatas });
    this.propertyForm.get('images')?.updateValueAndValidity();
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
