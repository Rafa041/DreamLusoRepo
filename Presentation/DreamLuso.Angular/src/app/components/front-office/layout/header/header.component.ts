import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../../../../services/user.service';
import { UsersModel } from '../../../../models/UsersModel';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {

  users: UsersModel[] = [];

  constructor(private http: HttpClient, private userService: UserService) {}

  ngOnInit(): void {
    this.RetrieveAll();
  }

  RetrieveAll(): void {
    this.userService.RetrieveAll().subscribe((x) => (this.users = x));

    console.log(this.users);
  }
}
