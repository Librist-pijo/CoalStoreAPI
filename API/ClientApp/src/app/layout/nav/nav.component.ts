import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
    isLoggedIn: boolean = false;

    constructor(
        private router: Router
    ) {}

    ngOnInit(): void {
        
    }

    navigate(e: any, path: string) {
        this.router.navigate([path]);
    }
}