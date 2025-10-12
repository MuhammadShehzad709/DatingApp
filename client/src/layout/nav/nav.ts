import { Component, inject, OnInit, signal } from '@angular/core';
import { AccountService } from '../../core/services/account-service';
import { FormsModule, TouchedChangeEvent } from '@angular/forms';
import {Router, RouterLink, RouterLinkActive } from '@angular/router';
import { themes } from '../../types/theme';
import { BusyService } from '../../core/services/busy-service';
@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink,RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav implements OnInit {

  protected accountService = inject(AccountService);
  protected busyService=inject(BusyService);
  private router=inject(Router);
  protected selectedTheme=signal<string>(localStorage.getItem('theme')||'light')
  protected themes=themes;
    ngOnInit(): void {
    document.documentElement.setAttribute('data-theme',this.selectedTheme())
  }
  handleSelectedTheme(theme:string){
    this.selectedTheme.set(theme);
    localStorage.setItem('theme',theme)
    document.documentElement.setAttribute('data-theme',theme)
    const elem=document.activeElement as HTMLDivElement;
    if(elem) elem.blur();
  }
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
