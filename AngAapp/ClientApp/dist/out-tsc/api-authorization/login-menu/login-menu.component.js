import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { map } from 'rxjs/operators';
let LoginMenuComponent = class LoginMenuComponent {
    constructor(authorizeService) {
        this.authorizeService = authorizeService;
    }
    ngOnInit() {
        this.isAuthenticated = this.authorizeService.isAuthenticated();
        this.userName = this.authorizeService.getUser().pipe(map(u => u && u.name));
    }
};
LoginMenuComponent = __decorate([
    Component({
        selector: 'app-login-menu',
        templateUrl: './login-menu.component.html',
        styleUrls: ['./login-menu.component.css']
    })
], LoginMenuComponent);
export { LoginMenuComponent };
//# sourceMappingURL=login-menu.component.js.map