import { Component, OnInit } from '@angular/core';
import { UserModel } from '../../../../../../models/UserModel';
import { UserService } from '../../../../../../services/UserService/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardClientComponent implements OnInit {
  loggedUserDetails: UserModel | null = null;

  constructor(
    private userService: UserService
  ) {}

  ngOnInit() {
    const loggedUserString = sessionStorage.getItem('loggedUser');
    if (loggedUserString) {
      const loggedUser = JSON.parse(loggedUserString);
      this.userService.retrieve(loggedUser.id).subscribe({
        next: (userDetails: UserModel) => {
          this.loggedUserDetails = userDetails;
        },
        error: (error) => {
          console.error('Error fetching user details:', error);
        }
      });
    }
  }
}
