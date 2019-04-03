import { Component } from '@angular/core';
import { UserService } from '../shared/_services';
import { first } from 'rxjs/operators';
import { User } from '../shared/models';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  users: User[] = [];

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.getAll()

      .pipe(first())
      .subscribe(users => { this.users = users })
  }
}
