import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { UserForDetail } from '../_modules/userForDetail';
import { Role } from '../_modules/role';
import { User } from '../_modules/user';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css']
})
export class UserDetailComponent implements OnInit {
  user: UserForDetail;
  userSaved: UserForDetail;
  roles: Role[];
  userRole: string;
  constructor(private userService: UserService, private toastr: ToastrService, private router: Router, private route: ActivatedRoute, private toast: ToastrService) { }

  ngOnInit() {
    this.getUserById();
    this.getAllRoles();
  }
  getAllRoles() {
    this.userService.getAllRoles().subscribe((data: Role[]) => {
      this.roles = data;
    }, error => {
    });
  }
  updateUser() {
    console.log(this.user);
    console.log(this.userRole);
    const roleSelected = this.roles.find(element =>
      element.name === this.userRole
    );
    this.user.role.id = roleSelected.id;
    this.updateUserWithRole(this.user);

  }
  getUserById() {
    this.route.paramMap.subscribe(params => {
      this.userService.getUserByUserId(+params.get('userId')).subscribe((data: UserForDetail) => {
        this.user = data;
        this.userRole = data.role.name;
      },
        error => {
          this.toastr.error(error);
        },
        () => {
        });
    });
  }
  updateUserWithRole(userForDetail: UserForDetail) {
    this.userService.updateUser(userForDetail).subscribe((data: UserForDetail) => {
      this.userSaved = data;
      this.toastr.success('User Updated');
      this.getUserById();
    }, error => {
      this.toastr.error(error.message);
    });
  }
}
