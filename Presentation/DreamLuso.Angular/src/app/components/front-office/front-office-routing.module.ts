import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main.component';
import { HeaderComponent } from './layout/header/header.component';
import { UserComponent } from './user/user.component';
import { UserListComponent } from './user-list/user-list.component';
import { LoginUserComponent } from './login-user/login-user.component';

const routes: Routes = [
  {
    path: '',
    component: HeaderComponent,
    children: [
      { path: 'user', component: UserComponent },
      { path: 'user/:id', component: UserComponent },

    ],

  },
  { path: 'userlist', component: UserListComponent },
  { path: 'login', component: LoginUserComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class FrontOfficeRoutingModule {}



