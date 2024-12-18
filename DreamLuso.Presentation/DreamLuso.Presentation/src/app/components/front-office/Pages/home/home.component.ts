import { Component, HostListener, NgZone } from '@angular/core';
import { UserModel } from '../../../../models/UserModel';
import { environment } from '../../../../../../environment';
import { Router } from '@angular/router';
import { AuthService } from '../../../../services/AuthService/auth.service';
import { UserService } from '../../../../services/UserService/user.service';
import { ApiNotification } from '../../../../models/Notification';
import { NotificationService } from '../../../../services/NotificationService/notification.service';
import { Property, PropertyStatus } from '../../../../models/property';
import { PropertyService } from '../../../../services/PropertyService/property.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent  {
  // User related properties
  loggedUserDetails: UserModel | null = null;
  userId: string = "";
  errorMessage: string = "";
  private apiUrl = environment.apiUrl;

  // UI state properties
  isNotificationsOpen = false;
  isDropdownOpen = false;
  loading: boolean = true;
  selectedOption: string = '1';
  searchQuery: string = '';

  // Properties related
  featuredProperties: Property[] = [];
  saleProperties: Property[] = [];
  rentProperties: Property[] = [];
  allProperties: Property[] = [];
  filteredProperties: Property[] = [];
  hasMoreProperties: boolean = false;

  // Notifications
  notifications: ApiNotification[] = [];
  notificationCount = 0;

  // Enums
  PropertyStatus = PropertyStatus;

  constructor(
    private router: Router,
    private authService: AuthService,
    private userService: UserService,
    private notificationService: NotificationService,
    private zone: NgZone,
    private propertyService: PropertyService,
  ) {}

  ngOnInit() {
    this.loadFeaturedProperties();
    this.initializeUserData();
    this.setupNotifications();
    this.filterProperties();
  }

  private initializeUserData() {
    const loggedUserString = sessionStorage.getItem('loggedUser');
    if (loggedUserString) {
      const loggedUser = JSON.parse(loggedUserString);
      this.userId = loggedUser.id;
      this.loadUserDetails();
    }
  }

  private loadUserDetails() {
    this.userService.retrieve(this.userId).subscribe({
      next: (userDetails: UserModel) => {
        this.zone.run(() => {
          this.loggedUserDetails = userDetails;
        });
      },
      error: (error) => {
        console.error('Error fetching user details:', error);
        this.errorMessage = 'Failed to load user details';
      },
    });
  }

  private setupNotifications() {
    if (this.userId) {
      this.loadNotifications();
      setInterval(() => this.loadNotifications(), 30000);
    }
  }

  loadNotifications() {
    if (this.userId) {
      this.notificationService.getUnreadNotifications(this.userId).subscribe(notifications => {
        this.notifications = notifications;
        this.notificationCount = notifications.filter(n => n.status === 'Unread').length;
      });
    }
  }
  loadMoreProperties() {
    // Load more properties based on current filter
    if (this.selectedOption === '1') {
      this.router.navigate(['/propertiesRent']);
    } else {
      this.router.navigate(['/propertiesBuy']);
    }
  }
  loadFeaturedProperties() {
    this.loading = true;
    this.propertyService.retrieveAll().subscribe({
      next: (properties) => {
        this.zone.run(() => {
          this.allProperties = properties.filter(p => p.isActive);
          this.featuredProperties = properties
            .filter(p => p.isActive && p.isFeatured)
            .slice(0, 4);
          this.saleProperties = properties
            .filter(p => p.isActive && p.status === PropertyStatus.ForSale)
            .slice(0, 4);
          this.rentProperties = properties
            .filter(p => p.isActive && p.status === PropertyStatus.ForRent)
            .slice(0, 4);
          this.loading = false;
        });
      },
      error: (error) => {
        console.error('Error loading properties:', error);
        this.loading = false;
      }
    });
  }

  filterProperties() {
    this.loading = true;
    const searchLower = this.searchQuery.toLowerCase();

    this.filteredProperties = this.allProperties.filter(property => {
      const matchesSearch =
        property.title.toLowerCase().includes(searchLower) ||
        property.city.toLowerCase().includes(searchLower) ||
        property.state.toLowerCase().includes(searchLower);

      const matchesType = this.selectedOption === '1' ?
        property.status === PropertyStatus.ForRent :
        property.status === PropertyStatus.ForSale;

      return matchesSearch && matchesType;
    });

    this.hasMoreProperties = this.filteredProperties.length > 8;
    this.filteredProperties = this.filteredProperties.slice(0, 8);
    this.loading = false;
  }
  getPropertyLocation(property: Property): string {
    return `${property.city}, ${property.state}`;
  }


  // Property getters
  getPropertiesForSale(): Property[] {
    return this.saleProperties;
  }

  getPropertiesForRent(): Property[] {
    return this.rentProperties;
  }

  // Navigation methods
  navigateToProperties() {
    this.router.navigate(['/properties']);
  }
  navigateToOption1() {
    this.router.navigate(['/option1']);
  }

  navigateToOption2() {
    this.router.navigate(['/option2']);
  }
  navigateToRent() {
    this.router.navigate(['/propertiesRent']);
  }

  navigateToBuy() {
    this.router.navigate(['/propertiesBuy']);
  }

  navigateToServices() {
    this.router.navigate(['/services']);
  }

  navigateToPropertiesAlgarve() {
    this.router.navigate(['/propertiesAlgarve']);
  }

  navigateToPropertiesPorto() {
    this.router.navigate(['/propertiesPorto']);
  }

  navigateToPropertiesLisbon() {
    this.router.navigate(['/propertiesLisbon']);
  }

  navigateToPropertyDetails(propertyId: string) {
    this.router.navigate(['/propertyId/', propertyId]);
  }

  navitageToLogin() {
    this.router.navigateByUrl('/login');
  }

  navitageToRegister() {
    this.router.navigateByUrl('/register');
  }

  // UI handlers
  handleNotificationClick(notification: ApiNotification) {
    this.notificationService.markAsRead(notification.id).subscribe(() => {
      notification.status = 'Read';
      this.notificationCount = this.notifications.filter(n => n.status === 'Unread').length;
    });
  }


  toggleNotifications(event: Event) {
    event.stopPropagation();
    this.isNotificationsOpen = !this.isNotificationsOpen;
  }

  toggleDropdown(event: Event) {
    event.preventDefault();
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  handleSearchInput(event: any) {
    this.searchQuery = event.target.value;
    this.filterProperties();
  }

  handleSearchSubmit() {
    this.router.navigate(['/search'], {
      queryParams: {
        q: this.searchQuery,
        type: this.selectedOption
      }
    });
  }

  handleOptionChange(event: any) {
    this.selectedOption = event.target.value;
    this.filterProperties();
  }

  // Authentication methods
  isAuthenticated(): boolean {
    return this.loggedUserDetails !== null;
  }

  getDashboardRoute(access: string | undefined): void {
    if (!access) return;
    switch (access) {
      case 'Admin':
        this.router.navigate(['/back-office/admin/dashboard']);
        break;
      case 'RealStateAgent':
        this.router.navigate(['/back-office/agent/dashboard']);
        break;
      case 'None':
        this.router.navigate(['/back-office/client/dashboard']);
        break;
      default:
        this.router.navigate(['/']);
        break;
    }
  }

  logout() {
    this.authService.logout().subscribe(
      () => {
        this.clearCache();
        this.router.navigate(['/']);
        window.location.reload();
      },
      (error) => {
        console.error('Logout failed', error);
      }
    );
  }
  // Utility methods
  getImageUrl(imageUrl: string | undefined): string {
    if (!imageUrl) {
      return 'assets/default-avatar.jpg';
    }
    return `${this.apiUrl}${imageUrl}`;
  }

  handleImageError(event: any) {
    event.target.src = 'assets/default-avatar.jpg';
  }

  private clearCache() {
    localStorage.clear();
    sessionStorage.clear();
    this.clearAllCookies();
    if ('caches' in window) {
      caches.keys().then((names) => {
        names.forEach(name => {
          caches.delete(name);
        });
      });
    }
  }

  private clearAllCookies() {
    const cookies = document.cookie.split(";");
    for (let i = 0; i < cookies.length; i++) {
      const cookie = cookies[i];
      const eqPos = cookie.indexOf("=");
      const name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
      document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT;path=/";
    }
  }

  // Click handlers
  @HostListener('document:click', ['$event'])
  private handleOutsideClick(event: MouseEvent): void {
    const target = event.target as HTMLElement;
    if (!target.closest('.relative')) {
      this.isDropdownOpen = false;
      this.isNotificationsOpen = false;
    }
  }
}
