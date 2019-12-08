import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'app-start',
    templateUrl: './start.component.html',
    styleUrls: ['./start.component.scss']
})
export class StartComponent implements OnInit {
    isRegPage = false;
    constructor() {
    }

    ngOnInit() {

    }

    loadPage(value: boolean) {
        this.isRegPage = value;
    }
}
