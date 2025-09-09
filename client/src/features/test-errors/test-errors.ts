import { HttpClient } from '@angular/common/http';
import { Component, inject, signal } from '@angular/core';

@Component({
  selector: 'app-test-errors',
  imports: [],
  templateUrl: './test-errors.html',
  styleUrl: './test-errors.css'
})
export class TestErrors {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:5001/api/';
  validationErrors = signal<string[]>([]);
  get404Error() {
    this.http.get(this.baseUrl + 'buggy/not-found').subscribe({
      error: err => console.log(err)
    })
  }
  get500Error() {
    this.http.get(this.baseUrl + 'buggy/server-error').subscribe({
      error: err => console.log(err)
    })
  }
  get401Error() {
    this.http.get(this.baseUrl + 'buggy/auth').subscribe({
      error: err => console.log(err)
    })
  }
  get400Error() {
    this.http.get(this.baseUrl + 'buggy/bad-request').subscribe({
      error: err => console.log(err)
    })
  }
  Validationerror() {
    this.http.post(this.baseUrl + 'account/register', {}).subscribe({
      error: err =>{
        console.log(err)
        this.validationErrors.set(err);
      }
    })
  }
}
