
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    public currentUserSubject: BehaviorSubject<any>;
    public currentUser: Observable<any>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<any>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue() {
        return this.currentUserSubject.value;
    }

    login(userInfo: any) {
        const headers = new HttpHeaders().set('Content-Type', 'application/json');
        return this.http.post<any>(`http://localhost:5005/api/Account/login`, JSON.stringify(userInfo), {headers});
            // .pipe(map(user => {
            //     console.log(user);
            //     console.log('user');
            //     localStorage.setItem('currentUser', JSON.stringify(user));
            //     this.currentUserSubject.next(user);
            //     return user;
            // }));
    }

    reg(userInfo: any) {
        return this.http.post(`http://localhost:5005/api/Account/Registration`, userInfo);
    }

    logout() {
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }

}
