import { Component, OnInit } from '@angular/core';
import { UserService } from '../Services/user.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  model: any = {};
  name: string;
  constructor(private userService: UserService, private router: Router, private toastr: ToastrService) {
    this.name = localStorage.getItem('username').toString();
  }

  ngOnInit() {
    this.name = localStorage.getItem('username').toString();
  }
  loggedIn() {
    return this.userService.loggedIn();
  }
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    localStorage.removeItem('username');
    localStorage.removeItem('role');
    this.userService.decodedToken = null;
    this.userService.currentUer = null;
    this.router.navigate(['/home']);
  }
  login() {
    this.userService.login(this.model).subscribe(next => {
      this.router.navigate(['/players']);
      this.name = localStorage.getItem('username').toString();
    }, error => {
      this.toastr.error(error.error);
    });
  }

}
