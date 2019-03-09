 import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './shared/_guards';
import { LoginComponent } from './account/login-form';
import { RegisterComponent } from './account/registration-form';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

//import { HomeComponent } from './home';
//import { LoginComponent } from './login';
//import { RegisterComponent } from './register';
//import { AuthGuard } from './_guards';

const appRoutes: Routes = [
  { path: '', component: HomeComponent  ,  canActivate: [AuthGuard] },
    { path: 'loginForm', component: LoginComponent },
    { path: 'registerForm', component: RegisterComponent },
  { path: 'counter', component: CounterComponent, canActivate: [AuthGuard]  },
  { path: 'fetch-data', component: FetchDataComponent, canActivate: [AuthGuard] },

    //RouterModule.forRoot([
  //  { path: '', component: HomeComponent, pathMatch: 'full' },
  //])

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);
