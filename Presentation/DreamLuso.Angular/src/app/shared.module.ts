import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@NgModule({
  imports: [CommonModule, HttpClientModule, ReactiveFormsModule, FormsModule],
  declarations: [],
  exports: [HttpClientModule, ReactiveFormsModule, FormsModule],
})
export class SharedModule {}
