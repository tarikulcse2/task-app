import { Component, OnInit } from '@angular/core';
import { JobService } from 'src/app/services/job.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
    jobs: any[] = [];
    constructor(private jobService: JobService, private toster: ToastrService) {
    }

    ngOnInit() {
        this.jobService.getJobByCurrentUser().subscribe((s: any) => this.jobs = s);
    }

    delete(jobId: number) {
        if  (confirm('Are you sure to delete')) {
            this.jobService.delete(jobId).subscribe((s: any) => {
                if (s.status) {
                    this.jobs = s.data;
                    this.toster.success('Job delete success', 'Success');
                } else {
                    this.toster.error('Job delete faild', 'Error');
                }
            });
        }
    }
}
