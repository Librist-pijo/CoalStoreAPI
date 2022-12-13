import { Component, OnInit } from "@angular/core";
import { DatabaseService } from "src/app/services/database.service";

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    

    constructor(
        private dbService: DatabaseService
    ) {

    }

    ngOnInit(): void {
        this.test();
    }

    test() {
        this.dbService.SetRoute('products');
        this.dbService.Get().subscribe((data) => {
            console.log('test', data);
        }, (err) => {
            console.log(err);
        });
    }
}