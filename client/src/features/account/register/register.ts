import { Component, inject, input, OnInit, output, signal, Signal } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { RegisterCreds, User } from '../../../types/user';
import { AccountService } from '../../../core/services/account-service';
import { JsonPipe } from '@angular/common';
import { InputText } from "../../../shared/input-text/input-text";
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, JsonPipe, InputText],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {

  private accountService = inject(AccountService);
  private router = inject(Router)
  currentStep = signal<number>(1);
  private fb = inject(FormBuilder);
  cancelRegister = output<boolean>();
  protected validationErrors = signal<string[]>([]);
  protected credentialForm: FormGroup;
  protected creds = {} as RegisterCreds;
  protected profileForm: FormGroup;
  constructor() {
    this.profileForm = this.fb.group({
      gender: ['male', Validators.required],
      dateOfBirth: ['', [Validators.required]],
      city: ['', Validators.required],
      country: ['', Validators.required]
    });
    this.credentialForm = this.fb.group({
      email: ['shehzad@test.com', [Validators.required, Validators.email]],
      displayName: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    });
    this.credentialForm.controls['password'].valueChanges.subscribe(() => {
      this.credentialForm.controls['confirmPassword'].updateValueAndValidity();
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const parent = control.parent;
      if (!parent) return null;
      const matchValue = parent.get(matchTo)?.value;
      return control.value === matchValue ? null : { passwordMismatch: true }
    }
  }
  nextStep() {
    if (this.credentialForm.valid) {
      this.currentStep.update(prevStep => prevStep + 1);
    }
  }
  prevStep() {
    this.currentStep.update(prevStep => prevStep - 1);
  }
  getMaxDate() {
    const today = new Date();
    today.setFullYear(today.getFullYear() - 18);
    return today.toISOString().split('T')[0];
  }
  register() {
    if (this.profileForm.valid && this.credentialForm.valid) {
      const formData = { ...this.credentialForm.value, ...this.profileForm.value };
      console.log('FormData', formData)
      this.accountService.register(formData).subscribe({
        next: response => {
          if(response.isSuccess){
            this.router.navigateByUrl('/members')
          }else{
             console.log(response.error);
          }
        },
        error:error=>{
          console.log('Errors',error)
          this.validationErrors.set(error);
        }
      });
    }

  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
