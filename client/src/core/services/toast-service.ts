import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  private containerCreated = false;

  private createToastContainer() {
    if (this.containerCreated) return;

    const container = document.createElement('div');
    container.id = 'toast-container';
    container.className = "toast toast-bottom toast-end";
    document.body.appendChild(container);

    this.containerCreated = true;
  }

  private createToastElements(message: string, alertClass: string, duration: number) {
    this.createToastContainer(); // 👈 ab yahan call hoga, constructor me nahi

    const toastContainer = document.getElementById('toast-container');
    if (!toastContainer) return;

    const toast = document.createElement('div');
    toast.classList.add('alert', alertClass, 'shadow-lg');
    toast.innerHTML = `
      <span>${message}</span>
      <button class="ml-4 btn btn-sm btn-ghost">x</button>
    `;
    toast.querySelector('button')?.addEventListener('click', () => {
      toastContainer.removeChild(toast);
    });
    toastContainer.appendChild(toast);

    setTimeout(() => {
      if (toastContainer.contains(toast)) {
        toastContainer.removeChild(toast);
      }
    }, duration);
  }

  success(message: string, duration: number = 5000) {
    this.createToastElements(message, 'alert-success', duration);
  }
  error(message: string, duration: number = 5000) {
    this.createToastElements(message, 'alert-error', duration);
  }
  warning(message: string, duration: number = 5000) {
    this.createToastElements(message, 'alert-warning', duration);
  }
  info(message: string, duration: number = 5000) {
    this.createToastElements(message, 'alert-info', duration);
  }
}

