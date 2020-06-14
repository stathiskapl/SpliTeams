import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { UserForList } from '../_modules/userForList';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  constructor(private userService: UserService) { }

  users: UserForList[];
  ngOnInit() {
    this.getAllUsers();
  }
  getAllUsers() {
    this.userService.getAllUsers().subscribe((data: UserForList[]) => {
      this.users = data;
    }, error => {
    });
  }
}
