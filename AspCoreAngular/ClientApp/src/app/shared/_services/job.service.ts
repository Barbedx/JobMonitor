import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { Job } from '../models'; 

//import { User } from 'src/app/shared/models';

//import { User } from '../_models';

@Injectable({ providedIn: 'root' })
export class JobService {
  constructor(private http: HttpClient) { }

  //getAll() {
  //  return this.http.get<User[]>(`/users`);
  //}

  //getById(id: number) {
  //  return this.http.get(`/users/` + id);
  //}

  //register(user: User) {
  //  return this.http.post(`/Register`, user);
  //}

  //update(user: User) {
  //  return this.http.put(`/users/` + user.id, user);
  //}

  //delete(id: number) {
  //  return this.http.delete(`/users/` + id);
  //}
}
