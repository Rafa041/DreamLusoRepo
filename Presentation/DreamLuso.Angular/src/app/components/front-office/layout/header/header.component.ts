import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../../services/user.service';
import { UserModel } from '../../../../models/UserModel';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {

  users: UserModel[] = [];

  constructor(private http: HttpClient, private userService: UserService) {}

  ngOnInit(): void {
    this.getAll();
  }

  getAll() {
    this.userService.getAll().subscribe((x) => (this.users = x));

    console.log(this.users);
  }
}
