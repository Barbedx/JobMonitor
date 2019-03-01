import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './registration-form';
import { LoginComponent } from './login-form'; 

@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  imports: [
    CommonModule
  ]
})
export class AccountModule { }
