import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  isAdmin = false;
  adminMenuItems = [
    {
      path: '/flights/find',
      title: 'Find Flight'
    },

    {
      path: '/flights/add',
      title: 'Add Flight'
    }
  ]
  loginForm = new FormGroup({});

  constructor(private fb: FormBuilder, private loginService: LoginService) { }

  ngOnInit(): void {
    this.isAdmin = this.loginService.isLoggedIn();
    this.buildForm();
  }

  buildForm(): void {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
  }

  login(): void {
    if (this.loginForm.valid) {
      this.loginService.login(this.loginForm.value).then();
      this.isAdmin = this.loginService.isLoggedIn();
    }
  }

  logout(): void {
    this.loginService.logout().then();
    this.isAdmin = this.loginService.isLoggedIn();
  }

}
