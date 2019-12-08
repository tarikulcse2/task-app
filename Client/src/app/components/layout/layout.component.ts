import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-layout',
    templateUrl: './layout.component.html',
    styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
    user = {};
    constructor(public auth: AuthService, private router: Router) {
    }

    ngOnInit() {
        // this.auth.currentUser.subscribe(u => this.user = u);
        this.user = this.auth.currentUserValue || {};
    }

    logOut() {
        this.auth.logout();
        this.router.navigate(['/login']);
    }
}
