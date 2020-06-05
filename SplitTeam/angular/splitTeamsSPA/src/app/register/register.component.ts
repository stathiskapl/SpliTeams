import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { User } from '../_modules/user';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../Services/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  user: User;
  registerForm: FormGroup;
  constructor(private fb: FormBuilder, private router: Router, private userService: UserService, private toastr: ToastrService) { }

  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', Validators.required]
    }, { validators: [this.passwordMatchValidator] });
  }
  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : { mismatch: true };
  }
  register() {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.userService.createUser(this.user).subscribe(() => {
        this.toastr.success('User Created!', 'successfully!');
        this.cancelRegister.emit(false);
      },
        error => {
          this.toastr.error(error.error);
        },
        () => {
          this.router.navigate(['/home']);
        }
      );
    }
  }
  cancel() {
    this.cancelRegister.emit(false);
  }

}
