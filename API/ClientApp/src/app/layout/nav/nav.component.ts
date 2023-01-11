import { Component, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { interval, Subscription, timer } from "rxjs";
import { AuthService } from "src/app/services/auth.service";
import { OrdersService } from "src/app/services/orders.service";
import { TokenService } from "src/app/services/token.service";

@Component({
    selector: 'app-nav',
    templateUrl: './nav.component.html',
    styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit, OnDestroy {
    isLoggedIn: boolean;
    token: string | null;
    basketButtonLabel: string = "Koszyk";
    private subscription: Subscription;

    constructor(
        private router: Router,
        private tokenService: TokenService,
        private authService: AuthService,
        private ordersService: OrdersService
    ) {
        this.isLoggedIn = false;
        this.token = null;

        this.subscription = interval(1000)
            .subscribe(x => { this.getProductsAmountInCart(); });
    }

    ngOnInit(): void {
        this.token = this.tokenService.getToken();

        if (this.token == null || this.token.trim().length == 0) {
            this.isLoggedIn = false;
        } else {
            this.isLoggedIn = true;
        }
    }

    ngOnDestroy(): void {
        this.subscription.unsubscribe();
    }

    navigate(e: any, path: string) {
        this.router.navigate([path]);
    }

    logout(e: any) {
        this.authService.logout();
        this.isLoggedIn = false;
        this.router.navigate(['./']);
    }

    getProductsAmountInCart() {
        var ordersProducts = this.ordersService.getCurrentOrderProducts();

        if (ordersProducts && ordersProducts.length > 0)
            this.basketButtonLabel = `Koszyk (${ordersProducts.length})`;
        else
            this.basketButtonLabel = "Koszyk";
    }
}