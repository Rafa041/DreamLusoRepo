import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { PropertyImage } from '../../../../models/property';

@Component({
  selector: 'app-image-upload',
  templateUrl: './image-upload.component.html',
  styleUrl: './image-upload.component.scss'
})
export class imageUploadComponent implements OnInit {
  @Output() imagesChanged = new EventEmitter<PropertyImage[]>();
  images: PropertyImage[] = [];

  constructor() { }

  ngOnInit(): void {
  }

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
      this.images = [...this.images, ...newImages];
      this.imagesChanged.emit(this.images);
      this.previewImages();
    }
  }

  previewImages() {
    const uploadedImagesDiv = document.getElementById('uploadedImages');
    if (uploadedImagesDiv) {
      uploadedImagesDiv.innerHTML = '';
      this.images.forEach((image: PropertyImage, index: number) => {
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
    this.images.splice(index, 1);
    this.imagesChanged.emit(this.images);
    this.previewImages();
  }
}
