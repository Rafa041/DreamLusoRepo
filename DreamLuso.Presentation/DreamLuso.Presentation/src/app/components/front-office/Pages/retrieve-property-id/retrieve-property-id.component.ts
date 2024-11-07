import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Property } from '../../../../models/property';
import { ActivatedRoute } from '@angular/router';
import { PropertyService } from '../../../../services/PropertyService/property.service';
import { UserService } from '../../../../services/UserService/user.service';
import { UserModel } from '../../../../models/UserModel';
import { environment } from '../../../../../../environment';
import { RealStateAgentService } from '../../../../services/RealStateAgent/real-state-agent.service';
import { AuthService } from '../../../../services/AuthService/auth.service';
import { Access } from '../../../../models/Access';
import { CreatePropertyVisit, TimeSlot } from '../../../../models/CreatePropertyVisit';
import { PropertyVisitService } from '../../../../services/PropertyVisitService/property-visit.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationService } from '../../../../services/NotificationService/notification.service';

@Component({
  selector: 'app-retrieve-property-id',
  templateUrl: './retrieve-property-id.component.html',
  styleUrl: './retrieve-property-id.component.scss'
})
export class RetrievePropertyIdComponent implements OnInit {
  property: Property | null = null;
  propertyOwner: UserModel | null = null;
  loading: boolean = true;
  currentImageIndex: number = 0;
  amenitiesList: string[] = [];
  private apiUrl = environment.apiUrl;
  isLoggedIn: boolean = false; // Variável para verificar o login do usuário
  Access = Access;

  @ViewChild('notLoggedIn') notLoggedIn!: TemplateRef<any>;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private propertyService: PropertyService,
    private realStateAgentService: RealStateAgentService,
    private authService: AuthService,
  ){ }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadProperty(id);
    }
    this.isLoggedIn = this.authService.isLoggedIn(); // Verifica se o usuário está logado
  }

  loadProperty(id: string): void {
    this.propertyService.retrieve(id).subscribe({
      next: (property) => {
        console.log('Full Property Data:', property);
        console.log('User ID:', property.realStateAgentId);

        this.property = property;
        if (property.amenities && typeof property.amenities === 'string') {
          this.amenitiesList = (property.amenities as string).split(',').map(item => item.trim());
        }

        if (property.realStateAgentId) {
          console.log('Loading user with ID:', property.realStateAgentId);
          this.loadPropertyOwner(property.realStateAgentId);
        } else {
          console.log('No userId found in property data');
        }

        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading property:', error);
        this.loading = false;
      }
    });
  }

  loadPropertyOwner(realStateAgentId: string): void {
    console.log(`API URL being called: ${this.apiUrl}/retrieve/${realStateAgentId}`);
    this.realStateAgentService.retrieve(realStateAgentId).subscribe({
      next: (user) => {
        console.log('Success:', user);
        this.propertyOwner = user;
      },
      error: (error) => {
        console.log('Full error:', error);
      }
    });
  }

  nextImage(): void {
    if (this.property?.images) {
      this.currentImageIndex = (this.currentImageIndex + 1) % this.property.images.length;
    }
  }

  previousImage(): void {
    if (this.property?.images) {
      this.currentImageIndex = this.currentImageIndex === 0
        ? this.property.images.length - 1
        : this.currentImageIndex - 1;
    }
  }

  getImageUrl(imageUrl: string | undefined): string {
    if (!imageUrl) {
      return 'assets/default-avatar.jpg'; // Path to a default image
    }
    // Using the base URL from environment + image path
    return `${this.apiUrl}${imageUrl}`;
  }

  handleImageError(event: any) {
    event.target.src = 'assets/default-avatar.jpg'; // Path to a default image
  }
  getCurrentDate(): string {
    return new Date().toISOString().split('T')[0];
  }


}
