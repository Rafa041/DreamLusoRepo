import { MainComponent } from './main.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared.module';
import { FrontOfficeRoutingModule } from './front-office-routing.module';
import { HeaderComponent } from './layout/header/header.component';

@NgModule({
  declarations: [
    MainComponent,
    HeaderComponent,
  ],
  imports: [FrontOfficeRoutingModule, CommonModule, SharedModule],
})
export class FrontOfficeModule {}
