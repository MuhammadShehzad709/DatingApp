import { Component, computed, inject } from '@angular/core';
import { ApiError } from '../../../types/ApiError';
import { Router } from '@angular/router';
import { createMayBeForwardRefExpression } from '@angular/compiler';

@Component({
  selector: 'app-server-error',
  imports: [],
  templateUrl: './server-error.html',
  styleUrl: './server-error.css'
})
export class ServerError {
 // protected error:ApiError;
  protected showDetails=false;
  private router=inject(Router);
    protected error = computed<ApiError | null>(() => {
    // naya signal-based API
    const nav = this.router.currentNavigation();
    return nav?.extras?.state?.['error'] ?? history.state['error'] ?? null;
  });
  detailsToggle(){
    this.showDetails=!this.showDetails;
  }
}
