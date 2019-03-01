import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {ReactiveFormsModule} from '@angular/forms'
import { HttpClientModule , HTTP_INTERCEPTORS, HttpClient} from '@angular/common/http';

 
//import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';



import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
//import { GrowlModule } from 'primeng/primeng'
import { routing } from './app.rounting';
import { AlertComponent } from './shared/_components/alert';
import { LoginComponent } from './account/login-form';
import { RegisterComponent } from './account/registration-form';
import { JwtInterceptor, ErrorInterceptor } from './shared/_helpers';

@NgModule({
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    //GrowlModule,
    ReactiveFormsModule,
    HttpClientModule,
    routing
  ],

  declarations: [
    AppComponent,
    AlertComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    RegisterComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
