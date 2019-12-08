import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { JobService } from 'src/app/services/job.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-job',
    templateUrl: './job.component.html',
    styleUrls: ['./job.component.scss']
})
export class JobComponent implements OnInit {
    jobForm: FormGroup;
    submitted = false;
    timeAlert = [
        { text: '5 minutes', value: 5 },
        { text: '10 minutes', value: 10 },
        { text: '15 minutes', value: 15 },
        { text: '20 minutes', value: 20 }
    ];

    colors = [
        { name: 'red', color: 'red' },
        { name: 'purple', color: 'purple' },
        { name: 'yellow', color: 'yellow' },
        { name: 'silver', color: 'silver' },
        { name: 'gray', color: 'gray' },
    ];
    title = 'Add New';
    constructor(private fb: FormBuilder,
                private jobService: JobService,
                private toastr: ToastrService,
                private activeRouter: ActivatedRoute,
                private router: Router) {
    }

    ngOnInit() {
        this.jobForm = this.fb.group({
            id: [0],
            title: ['', Validators.required],
            description: [''],
            date: ['', Validators.required],
            fromTime: ['', Validators.required],
            toTime: [''],
            location: [''],
            notify: [''],
            label: ['', Validators.required]
        });
        const id = this.activeRouter.snapshot.params.id;
        if (id) {
            this.jobService.getJobById(Number(id) || 0).subscribe(s => {
                this.jobForm.patchValue(s);
                this.title = 'Update';
            });
        }
    }

    onSubmit() {
        this.submitted = true;
        if (this.jobForm.invalid) {
            return;
        }
        if  (this.title === 'Update') {
            this.jobService.update(this.jobForm.value).subscribe((s: any) => {
                if  (s.status)  {
                    this.jobForm.reset({
                        id: 0,
                        title: '',
                        date: '',
                        fromTime: '',
                        label: ''
                    });
                    this.submitted = false;
                    this.toastr.success('Job has been updated', 'Success');
                    this.router.navigateByUrl('/home');
                } else {
                    this.toastr.error('Job update faild, try again?', 'Error');
                }
            });
        } else {
        this.jobService.add(this.jobForm.value).subscribe((s: any) => {
            if  (s.status)  {
                this.jobForm.reset({
                    id: 0,
                    title: '',
                    date: '',
                    fromTime: '',
                    label: ''
                });
                this.submitted = false;
                this.toastr.success('Job has been add success', 'Success');
            } else {
                this.toastr.error('Job save faild, try again?', 'Error');
            }
        });
        }
    }

    public errorHandling = (control: string, error: string) => {
        return this.jobForm.controls[control].hasError(error);
    }
}
