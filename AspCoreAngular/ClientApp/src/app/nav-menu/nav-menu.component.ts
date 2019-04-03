import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../shared/_services';
import { first, map } from 'rxjs/operators';
import { User } from '../shared/models';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  public currentUser: User;
  constructor(
    private router: Router,
    private authenticationService: AuthenticationService) {
    //this.authenticationService.currentUser.subscribe(x => this.currentUser = x);
  }
  ngOnInit() {
     
      this.authenticationService.currentUser.subscribe(u => this.currentUser = u) 
      //this.currentUser = this.authenticationService.currentUserValue;
    
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/loginForm']);
  }
}
