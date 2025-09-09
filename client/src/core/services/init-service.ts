import { inject, Injectable, OnInit } from '@angular/core';
import { AccountService } from './account-service';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class InitService {
  private accountService = inject(AccountService);
  init() {
    const userstring = localStorage.getItem('user');
    if (!userstring) return of(null);
    const user = JSON.parse(userstring);
    this.accountService.currentUser.set(user);
    return of(null);
  }

}
