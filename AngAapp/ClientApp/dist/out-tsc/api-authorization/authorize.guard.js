import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
import { tap } from 'rxjs/operators';
import { ApplicationPaths, QueryParameterNames } from './api-authorization.constants';
let AuthorizeGuard = class AuthorizeGuard {
    constructor(authorize, router) {
        this.authorize = authorize;
        this.router = router;
    }
    canActivate(_next, state) {
        return this.authorize.isAuthenticated()
            .pipe(tap(isAuthenticated => this.handleAuthorization(isAuthenticated, state)));
    }
    handleAuthorization(isAuthenticated, state) {
        if (!isAuthenticated) {
            this.router.navigate(ApplicationPaths.LoginPathComponents, {
                queryParams: {
                    [QueryParameterNames.ReturnUrl]: state.url
                }
            });
        }
    }
};
AuthorizeGuard = __decorate([
    Injectable({
        providedIn: 'root'
    })
], AuthorizeGuard);
export { AuthorizeGuard };
//# sourceMappingURL=authorize.guard.js.map