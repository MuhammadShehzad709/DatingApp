import { Component, inject, signal } from '@angular/core';
import { AccountService } from '../../core/services/account-service';
import { FormsModule } from '@angular/forms';
import {Router, RouterLink, RouterLinkActive } from '@angular/router';
@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink,RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
  protected accountService = inject(AccountService);
  private router=inject(Router);
  protected creds: any= {};
  login() {
    this.accountService.login(this.creds).subscribe({
      next:Response=>{
        if(Response.isSuccess){
          this.creds={};
        }
      }
    });
  }
  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
