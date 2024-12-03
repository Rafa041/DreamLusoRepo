import { Component, OnInit } from '@angular/core';
import { UserModel } from '../../../../../../models/UserModel';
import { environment } from '../../../../../../../../environment';
import { UserService } from '../../../../../../services/UserService/user.service';
import { RetrieveAllUsersResponse, RetrieveUserResponse } from '../../../../../../models/RetrieveAllUsers';
import { Access } from '../../../../../../models/Access';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrl: './users.component.scss'
})
export class UsersComponent implements OnInit {
  users: RetrieveUserResponse[] = [];
  loggedUserDetails: UserModel | null = null;
  private apiUrl = environment.apiUrl;
  Access = Access;
  constructor(private userService: UserService) {}

  ngOnInit() {
    this.loadUsers();
    this.loadLoggedUserDetails();
  }

  confirmBlock(user: RetrieveUserResponse) {
    if (confirm('Are you sure you want to block this user?')) {
      this.blockUser(user);
    }
  }
  blockUser(user: RetrieveUserResponse) {
    const updatedUser = { ...user, access: Access.Blocked };

    this.userService.updateAccess(user.id, updatedUser).subscribe({
      next: () => {
        // Update the local user list to reflect the change
        this.users = this.users.map(u =>
          u.id === user.id ? { ...u, access: Access.Blocked } : u
        );
      },
      error: (error) => {
        console.error('Error blocking user:', error);
      }
    });
  }
  loadUsers() {
    this.userService.getAllUsers().subscribe({
      next: (response) => {
        console.log('API Response:', response);
        this.users = response;
      },
      error: (error) => {
        console.error('Error loading users:', error);
      }
    });
  }
  getDisplayAccess(access: string): string {
    switch (access) {
      case 'Real State Agent':
        return 'Agent';
      case 'RealStateAgent':
        return 'Agent';
      default:
        return access;
    }
  }
  loadLoggedUserDetails() {
    const loggedUserString = sessionStorage.getItem('loggedUser');
    if (loggedUserString) {
      const loggedUser = JSON.parse(loggedUserString);
      this.userService.retrieve(loggedUser.id).subscribe({
        next: (userDetails) => {
          this.loggedUserDetails = userDetails;
        },
        error: (error) => {
          console.error('Error loading logged user details:', error);
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
}
