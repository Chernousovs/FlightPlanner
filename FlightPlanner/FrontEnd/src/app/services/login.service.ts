import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { LoginQuery } from "../shared/models/login.model";

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  constructor(private router: Router) {
  }

  async login(query: LoginQuery): Promise<void> {
    if (query.username === 'codelex-admin' && query.password === 'Password123') {
      const adminDetails = query.username + ':' + query.password;
      localStorage.setItem('admin',adminDetails);
      await this.router.navigate(['flights/find']).then();
    }

  }

  async logout(): Promise<void> {
    localStorage.removeItem('admin');
    await this.router.navigate(['flights/search'])
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('admin');
  }
}
