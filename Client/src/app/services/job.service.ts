
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class JobService {

    constructor(public http: HttpClient) { }

    getJobById(id: number) {
        return this.http.get(`http://localhost:5005/api/Job/GetById/${id}`);
    }

    getJobByCurrentUser() {
        return this.http.get(`http://localhost:5005/api/Job/GetByCurrentUser`);
    }

    add(job: any) {
        return this.http.post(`http://localhost:5005/api/Job`, JSON.stringify(job));
    }

    update(job: any) {
        return this.http.put(`http://localhost:5005/api/Job`, JSON.stringify(job));
    }

    delete(id: number) {
        return this.http.delete(`http://localhost:5005/api/Job/${id}`);
    }
}
