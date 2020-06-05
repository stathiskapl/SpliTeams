import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { User } from '../_modules/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  decodedToken: any;
  currentUer: User;
  jwtHelper = new JwtHelperService();
  username: string;
  constructor(private http: HttpClient) { }

  createUser(user: User) {
    return this.http.post(this.baseUrl + 'user/Create', user);
  }
  login(model: any) {
    return this.http.post(this.baseUrl + 'user/Login', model)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
            localStorage.setItem('userId', user.id);
            localStorage.setItem('username', user.userName);
            localStorage.setItem('role', user.role.name);
            this.decodedToken = this.jwtHelper.decodeToken(user.token);
            this.currentUer = user.user;
            this.username = JSON.stringify(user.userName);
          }
        })
      );
  }
  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);

  }
}
