import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { Nav } from "../layout/nav/nav";
import { AccountService } from '../core/services/account-service';
import { Home } from "../features/home/home";
import { User } from '../types/user';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [Nav, Home],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private accountService = inject(AccountService);
  private http = inject(HttpClient);
  protected readonly title = "Dating App";
  public members = signal<any>([]);
  async ngOnInit() {
    this.setcurrentUser();
    this.members.set(await this.getmembers());

    // this.http.get('https://localhost:5001/WeatherForecast').subscribe({
    //   next: response => this.members.set(response),
    //   error: error => console.log('Error', error),
    //   complete: () => console.log('Completed the httprequest>')
    //})
  }
  setcurrentUser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountService.currentUser.set(user);
  }
  async getmembers() {
    try {
      return lastValueFrom(this.http.get('https://localhost:5001/WeatherForecast'));
    } catch (error) {
      console.log(error);
      throw error;
    }
  }
}