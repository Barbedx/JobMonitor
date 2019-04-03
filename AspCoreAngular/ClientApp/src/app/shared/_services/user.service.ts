import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models';

//import { User } from 'src/app/shared/models';

//import { User } from '../_models';

@Injectable({ providedIn: 'root' })
export class UserService {
  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<User[]>('/api/users');
  }

  getById(id: number) {
    return this.http.get('/api//users/' + id);
  }

  register(user: User) {
    return this.http.post('/api/Register', user);
  }

  //update(user: User) {
  //  return this.http.put(`/users/` + user.id, user);
  //}

  //delete(id: number) {
  //  return this.http.delete(`/users/` + id);
  //}
}
