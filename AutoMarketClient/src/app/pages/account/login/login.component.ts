import { User } from './../../../models/account/user';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup = new FormGroup ({});
  submitted = false;
  errorMessages: string[] = [];

  isTextFieldType = false;

  constructor(private accountService: AccountService, 
    private formBuilder: FormBuilder,
    private router: Router) {
      this.accountService.user$.pipe(take(1)).subscribe({
        next: (user: User | null ) => {
          if (user) {
            this.router.navigateByUrl('/');
            console.log(user);
          }
        }
      });
    }
    
  ngOnInit(): void {
    this.initializeForm();
  }
  
  initializeForm() {
    this.loginForm = this.formBuilder.group({
      userName: ['', Validators.required],
      password: ['', Validators.required],
    })
  }

  login() {
    this.submitted = true;
    this.errorMessages = [];

    if (this.loginForm.valid){
      this.accountService.login(this.loginForm.value).subscribe({
        next: (response: any) => {
          this.router.navigateByUrl('/');
        },
        error: error => {
          if (error.error.error){
            this.errorMessages = error.error.error;
          } else {
            this.errorMessages.push(error.error)
          }
        }
      })
    }
  }

  togglePasswordFieldType(){
    this.isTextFieldType = !this.isTextFieldType;
  }

  resendEmailConfirmationLink() {
    this.router.navigateByUrl('/account/send-email/resend-email-confirmation-link');
  }
} 
