import { Component, OnInit, NgZone } from '@angular/core';
import { UserService } from '../../../services/user.service';
import { UsersModel } from '../../../models/UsersModel';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss'
})
export class UserListComponent implements OnInit {
  users: UsersModel[] = [];
  errorMessage: string = '';

constructor(private zone: NgZone, private http: HttpClient, private userService: UserService, private router: Router) {}

ngOnInit(): void {
  this.userService.RetrieveAll().subscribe({
    next: (response: any) => {
      this.zone.run(() => {
        if (response && response.users) {
          this.users = response.users;
          console.log(this.users);
        } else {
          console.error('Resposta inválida do servidor');
          this.errorMessage = 'Dados de usuários inválidos';
        }
      });
    },
    error: (error) => {
      console.error('Error fetching users:', error);
      this.errorMessage = 'Failed to load users';
    }
  });

}

getAccessString(access: number): string {
  switch (access) {
    case 0: return 'None';
    case 1: return 'Guest';
    case 2: return 'User';
    case 3: return 'Realtor';
    case 4: return 'Admin';
    case 5: return 'Blocked';
    default: return 'Unknown';
  }
}
}
