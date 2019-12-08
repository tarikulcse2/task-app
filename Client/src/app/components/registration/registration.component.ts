import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
    selector: 'app-registration',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.scss'],
})
export class RegistrationComponent implements OnInit {
    regForm: FormGroup;
    submitted = false;
    file: File = null;
    previewUrl: any  = 'assets/default.png';
    constructor(
        private fb: FormBuilder,
        private authService: AuthService,
        private toastr: ToastrService,
        private router: Router) {
    }

    ngOnInit() {
        this.regForm = this.fb.group({
            fullName: ['', Validators.required],
            email: ['', Validators.required],
            password: ['', Validators.required],
            date: [''],
        });
    }

    onSubmit() {
        this.submitted = true;
        if (this.regForm.invalid) {
            return;
        }

        const formData = new FormData();
        if  (this.file != null) {
            formData.append('file', this.file, this.file.name);
        }
        formData.append('modelData', JSON.stringify(this.regForm.value));
        this.authService.reg(formData).subscribe((s: any) => {
            if  (s.status)  {
                this.toastr.success('Registration has been success', 'Success');
                this.router.navigateByUrl('/login');
            } else {
                this.toastr.error('Registration Faild, try again?', 'Error');
            }
        });
    }

    fileChange(fileInput: any) {
        const file = fileInput.target.files[0] as File;
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
            this.previewUrl = reader.result;
        };
        this.file = file;
    }

    public errorHandling = (control: string, error: string) => {
        return this.regForm.controls[control].hasError(error);
    }
}
