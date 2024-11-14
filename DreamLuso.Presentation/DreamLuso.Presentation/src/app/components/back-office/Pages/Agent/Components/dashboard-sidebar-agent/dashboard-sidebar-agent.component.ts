import { Component, Input, OnInit } from '@angular/core';
import { UserModel } from '../../../../../../models/UserModel';
import { environment } from '../../../../../../../../environment';
import { AuthService } from '../../../../../../services/AuthService/auth.service';
import { Router } from '@angular/router';
import { UserService } from '../../../../../../services/UserService/user.service';

@Component({
  selector: 'app-dashboard-sidebar-agent',
  templateUrl: './dashboard-sidebar-agent.component.html',
  styleUrl: './dashboard-sidebar-agent.component.scss'
})
export class DashboardSidebarAgentComponent implements OnInit {
  @Input() loggedUserDetails: UserModel | null = null;
  private apiUrl = environment.apiUrl;
  userId: string = "";

  constructor(
    private router: Router,
    private authService: AuthService,
    private userService: UserService
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
  }

  getImageUrl(imageUrl: string | undefined): string {
    if (!imageUrl) {
      return 'assets/default-avatar.jpg';
    }
    return `${this.apiUrl}${imageUrl}`;
  }

  handleImageError(event: any) {
    event.target.src = 'assets/default-avatar.jpg';
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
  navigateToProperties() {
    this.router.navigate(['/back-office/agent/properties']);

  }
  navigateToAppointments() {
    this.router.navigate(['/back-office/appointmentsAgent']);

  }
}
