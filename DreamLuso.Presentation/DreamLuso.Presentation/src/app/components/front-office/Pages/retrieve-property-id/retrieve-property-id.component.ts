import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Property } from '../../../../models/property';
import { ActivatedRoute, Router } from '@angular/router';
import { PropertyService } from '../../../../services/PropertyService/property.service';
import { UserService } from '../../../../services/UserService/user.service';
import { UserModel } from '../../../../models/UserModel';
import { environment } from '../../../../../../environment';
import { RealStateAgentService } from '../../../../services/RealStateAgent/real-state-agent.service';
import { AuthService } from '../../../../services/AuthService/auth.service';
import { Access } from '../../../../models/Access';
import { ChatService } from '../../../../services/ChatService/chat.service';
interface CreateChatDto {
  propertyId: string;
  userId: string;
  realStateAgentId: string;
}

@Component({
  selector: 'app-retrieve-property-id',
  templateUrl: './retrieve-property-id.component.html',
  styleUrl: './retrieve-property-id.component.scss'
})
export class RetrievePropertyIdComponent implements OnInit {
  property!: Property;
  currentImageIndex = 0;

  propertyOwner: UserModel | null = null;
  loading = true;
  amenitiesList: string[] = [];
  private apiUrl = environment.apiUrl;
  isLoggedIn = false;
  Access = Access;
  isRealStateAgent = false;
  currentUser: UserModel | null = null;

  // Image Modal Properties
  isModalOpen = false;
  scale = 1;
  position = { x: 0, y: 0 };
  isDragging = false;
  startPosition = { x: 0, y: 0 };
  maxScale = 3;
  minScale = 1;
  scaleStep = 0.25;

  @ViewChild('notLoggedIn') notLoggedIn!: TemplateRef<any>;

  constructor(
    private route: ActivatedRoute,
    private propertyService: PropertyService,
    private realStateAgentService: RealStateAgentService,
    private authService: AuthService,
    private router: Router,
    private chatService: ChatService,
  ) { }

  ngOnInit(): void {
    this.initializeComponent();
  }

  private initializeComponent(): void {
    this.currentUser = this.authService.getCurrentUser();
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loadProperty(id);
    }
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  // Image Modal Methods
  zoomIn(): void {
    if (this.scale < this.maxScale) {
      this.scale = Math.min(this.scale + this.scaleStep, this.maxScale);
    }
  }

  zoomOut(): void {
    if (this.scale > this.minScale) {
      this.scale = Math.max(this.scale - this.scaleStep, this.minScale);
    }
  }

  startDrag(event: MouseEvent): void {
    if (this.scale > 1) {
      this.isDragging = true;
      this.startPosition = {
        x: event.clientX - this.position.x,
        y: event.clientY - this.position.y
      };
    }
  }

  onDrag(event: MouseEvent): void {
    if (this.isDragging) {
      this.position = {
        x: event.clientX - this.startPosition.x,
        y: event.clientY - this.startPosition.y
      };
    }
  }

  stopDrag(): void {
    this.isDragging = false;
  }

  resetZoom(): void {
    this.scale = 1;
    this.position = { x: 0, y: 0 };
  }

  openImageModal(): void {
    if (this.property?.images && this.property.images.length > 0) {
      this.isModalOpen = true;
      this.resetZoom();
    }
  }
  closeImageModal(): void {
    this.isModalOpen = false;
    this.resetZoom();
  }

  get showChatButton(): boolean {
    return this.isLoggedIn && this.currentUser?.access !== Access.RealStateAgent;
  }

  checkUserRole(): void {
    if (this.isLoggedIn) {
      const user = this.authService.getCurrentUser();
      this.isRealStateAgent = user?.access === Access.RealStateAgent;
    }
  }

  loadProperty(id: string): void {
    this.propertyService.retrieve(id).subscribe({
      next: (property) => {
        this.property = property;
        this.processPropertyData(property);
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading property:', error);
        this.loading = false;
      }
    });
  }

  private processPropertyData(property: Property): void {
    if (property.amenities && typeof property.amenities === 'string') {
      this.amenitiesList = (property.amenities as string).split(',').map(item => item.trim());
    }
    if (property.realStateAgentId) {
      this.loadPropertyOwner(property.realStateAgentId);
    }
  }

  loadPropertyOwner(realStateAgentId: string): void {
    this.realStateAgentService.retrieve(realStateAgentId).subscribe({
      next: (user) => this.propertyOwner = user,
      error: (error) => console.log('Error:', error)
    });
  }

  nextImage(): void {
    if (this.property?.images) {
      this.currentImageIndex = (this.currentImageIndex + 1) % this.property.images.length;
    }
  }

  previousImage(): void {
    if (this.property?.images && this.property.images.length > 0) {
      this.currentImageIndex = this.currentImageIndex === 0
        ? this.property.images.length - 1
        : this.currentImageIndex - 1;
    }
  }

// Add these methods to your component class
getImagePath(): string {
  const imagePath = this.property?.images?.[this.currentImageIndex]?.imagePath;
  return imagePath || 'assets/default-property-image.jpg';
}

getImageUrl(imageUrl: string | undefined): string {
  return imageUrl ? `${this.apiUrl}${imageUrl}` : 'assets/default-avatar.jpg';
}

  handleImageError(event: any): void {
    event.target.src = 'assets/default-avatar.jpg';
  }

  getCurrentDate(): string {
    return new Date().toISOString().split('T')[0];
  }

  openChat(): void {
    const loggedUserString = sessionStorage.getItem('loggedUser');

    if (loggedUserString) {
      const loggedUser = JSON.parse(loggedUserString);

      if (loggedUser && this.property?.id && this.propertyOwner?.id) {
        const chatData: CreateChatDto = {
          propertyId: this.property.id,
          userId: loggedUser.id,
          realStateAgentId: this.propertyOwner.id
        };

        console.log('Initiating chat with data:', chatData);

        this.chatService.initiateChat(chatData).subscribe({
          next: (chat) => {
            this.router.navigate(['/back-office/client/chatClient'], {
              state: { chat }
            });
          },
          error: (error) => {
            console.error('Error creating chat:', error);
          }
        });
      }
    }
  }
}
