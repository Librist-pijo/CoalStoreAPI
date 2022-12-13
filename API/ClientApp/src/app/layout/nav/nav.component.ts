import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "src/app/services/auth.service";
import { TokenService } from "src/app/services/token.service";

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
    isLoggedIn: boolean;
    token: string | null;

    constructor(
        private router: Router,
        private tokenService: TokenService
    ) {
        this.isLoggedIn = false;
        this.token = null;
    }

    ngOnInit(): void {
        this.token = this.tokenService.getToken();

        if (this.token == null || this.token.trim().length == 0) {
            this.isLoggedIn = false;
        } else {
            this.isLoggedIn = true;
        }
    }

    navigate(e: any, path: string) {
        this.router.navigate([path]);
    }
}