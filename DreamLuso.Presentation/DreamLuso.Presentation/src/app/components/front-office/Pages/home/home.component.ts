import { Component, HostListener, NgZone } from '@angular/core';
import { UserModel } from '../../../../models/UserModel';
import { environment } from '../../../../../../environment';
import { Router } from '@angular/router';
import { AuthService } from '../../../../services/AuthService/auth.service';
import { UserService } from '../../../../services/UserService/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent  {
  loggedUserDetails: UserModel | null = null;
  userId: string = "";
  errorMessage: string = "";
  private apiUrl = environment.apiUrl;

   // Add these properties
   isNotificationsOpen = false;
   notificationCount = 3; // Example count
   notifications = [
     {
       message: 'New property listed in Porto',
       time: '5 minutes ago'
     },
     {
       message: 'Your saved property price has been updated',
       time: '1 hour ago'
     },
     {
       message: 'Welcome to DreamLuso!',
       time: '1 day ago'
     }
   ];

  constructor(
    private router: Router,
    private authService: AuthService,
    private userService: UserService,
    private zone: NgZone
  ) {}

  ngOnInit() {
    const loggedUserString = sessionStorage.getItem('loggedUser');

    if (loggedUserString) {
      const loggedUser = JSON.parse(loggedUserString);
      this.userId = loggedUser.id;

      this.userService.retrieve(this.userId).subscribe({
        next: (userDetails: UserModel) => {
          this.zone.run(() => {
            this.loggedUserDetails = userDetails;
            console.log('Logged user details:', this.loggedUserDetails);
          });
        },
        error: (error) => {
          console.error('Error fetching user details:', error);
          this.errorMessage = 'Failed to load user details';
        },
      });
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

  isAuthenticated(): boolean {
    return this.loggedUserDetails !== null;
  }

  navitageToLogin() {
    this.router.navigateByUrl('/login');
  }

  navitageToRegister() {
    this.router.navigateByUrl('/register');
  }

  isDropdownOpen = false;

  toggleDropdown(event: Event) {
    event.preventDefault();
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    if (!(event.target as HTMLElement).closest('.relative')) {
      this.isDropdownOpen = false;
    }
  }

  logout() {
    this.authService.logout().subscribe(
      () => {
        this.clearCache();
        // Navigate to home page or login page after successful logout
        this.router.navigate(['/']);
        window.location.reload();
      },
      (error) => {
        console.error('Logout failed', error);
        // Handle logout error (optional)
      }
    );
  }

  private clearCache() {
    // Clear localStorage
    localStorage.clear();

    // Clear sessionStorage
    sessionStorage.clear();

    // Clear cookies
    this.clearAllCookies();

    // Clear application cache if supported by the browser
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
  // Add this method
  toggleNotifications(event: Event) {
    event.stopPropagation();
    this.isNotificationsOpen = !this.isNotificationsOpen;
    // Close other dropdowns when notifications is opened
    if (this.isNotificationsOpen) {
      this.isDropdownOpen = false;
    }
  }

  // Modify existing document click handler
  @HostListener('document:click', ['$event'])
private handleOutsideClick(event: MouseEvent): void {
  const target = event.target as HTMLElement;
  if (!target.closest('.relative')) {
    this.isDropdownOpen = false;
    this.isNotificationsOpen = false;
  }
}
}
