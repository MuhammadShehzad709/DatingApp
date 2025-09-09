import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { RegisterCreds, User } from '../../types/user';
import { tap } from 'rxjs';
import { ApiResponse } from '../../types/ApiResponse';
import { ToastService } from './toast-service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  toastService = inject(ToastService);
  currentUser = signal<User | null>(null);
  baseUrl = 'https://localhost:5001/api/';
  //Register Method is here
  register(creds: RegisterCreds) {
    return this.http.post<ApiResponse<User>>(this.baseUrl + 'account/register', creds).pipe(
      tap(user => {
        if (user.isSuccess && user.data) {
          this.setCurrentUser(user);
        }
        else {
          alert(user.error);
        }
      })
    )
  }
  //Login Method is here
  login(creds: any) {
    return this.http.post<ApiResponse<User>>(this.baseUrl + 'account/login', creds).pipe(
      tap(user => {
        if (user.isSuccess && user.data) {
          this.setCurrentUser(user);
          this.toastService.success('Login Successfully')
        }
        else if (!user.isSuccess) {
          console.log(user)
          this.toastService.error(user.error!)
        }
      })
    )
  }
  //LogoutMethod is Here
  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
  //Set Current Method is here
  setCurrentUser(user: ApiResponse<User>) {
    console.log('SuccessResponse', user);
    this.currentUser.set(user.data);
    localStorage.setItem('user', JSON.stringify(user.data));
  }
}
