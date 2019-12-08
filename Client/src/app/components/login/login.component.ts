import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-component',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    submitted = false;
    constructor(private fb: FormBuilder,
                private authService: AuthService,
                private router: Router,
                private toster: ToastrService) {
    }

    ngOnInit() {
        if (this.authService.currentUserValue) {
            this.router.navigateByUrl('/home');
        }
        this.loginForm = this.fb.group({
            email: ['', Validators.required],
            password: ['', Validators.required],
        });
    }

    onSubmit() {
        this.submitted = true;
        if (this.loginForm.invalid) {
            return;
        }
        this.authService.login(this.loginForm.value).subscribe((user: any) => {
            if (user.isAuthorized) {
                localStorage.setItem('currentUser', JSON.stringify(user));
                this.authService.currentUserSubject.next(user);
                this.router.navigateByUrl('/home');
            } else {
                this.toster.error('user name and password invalid', 'Login Faild');
            }
        });
    }

    public errorHandling = (control: string, error: string) => {
        return this.loginForm.controls[control].hasError(error);
    }
}
