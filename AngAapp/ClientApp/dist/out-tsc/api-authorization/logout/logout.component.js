import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { AuthenticationResultStatus } from '../authorize.service';
import { BehaviorSubject } from 'rxjs';
import { take } from 'rxjs/operators';
import { LogoutActions, ApplicationPaths, ReturnUrlType } from '../api-authorization.constants';
// The main responsibility of this component is to handle the user's logout process.
// This is the starting point for the logout process, which is usually initiated when a
// user clicks on the logout button on the LoginMenu component.
let LogoutComponent = class LogoutComponent {
    constructor(authorizeService, activatedRoute, router) {
        this.authorizeService = authorizeService;
        this.activatedRoute = activatedRoute;
        this.router = router;
        this.message = new BehaviorSubject(null);
    }
    async ngOnInit() {
        const action = this.activatedRoute.snapshot.url[1];
        switch (action.path) {
            case LogoutActions.Logout:
                if (!!window.history.state.local) {
                    await this.logout(this.getReturnUrl());
                }
                else {
                    // This prevents regular links to <app>/authentication/logout from triggering a logout
                    this.message.next('The logout was not initiated from within the page.');
                }
                break;
            case LogoutActions.LogoutCallback:
                await this.processLogoutCallback();
                break;
            case LogoutActions.LoggedOut:
                this.message.next('You successfully logged out!');
                break;
            default:
                throw new Error(`Invalid action '${action}'`);
        }
    }
    async logout(returnUrl) {
        const state = { returnUrl };
        const isauthenticated = await this.authorizeService.isAuthenticated().pipe(take(1)).toPromise();
        if (isauthenticated) {
            const result = await this.authorizeService.signOut(state);
            switch (result.status) {
                case AuthenticationResultStatus.Redirect:
                    break;
                case AuthenticationResultStatus.Success:
                    await this.navigateToReturnUrl(returnUrl);
                    break;
                case AuthenticationResultStatus.Fail:
                    this.message.next(result.message);
                    break;
                default:
                    throw new Error('Invalid authentication result status.');
            }
        }
        else {
            this.message.next('You successfully logged out!');
        }
    }
    async processLogoutCallback() {
        const url = window.location.href;
        const result = await this.authorizeService.completeSignOut(url);
        switch (result.status) {
            case AuthenticationResultStatus.Redirect:
                // There should not be any redirects as the only time completeAuthentication finishes
                // is when we are doing a redirect sign in flow.
                throw new Error('Should not redirect.');
            case AuthenticationResultStatus.Success:
                await this.navigateToReturnUrl(this.getReturnUrl(result.state));
                break;
            case AuthenticationResultStatus.Fail:
                this.message.next(result.message);
                break;
            default:
                throw new Error('Invalid authentication result status.');
        }
    }
    async navigateToReturnUrl(returnUrl) {
        await this.router.navigateByUrl(returnUrl, {
            replaceUrl: true
        });
    }
    getReturnUrl(state) {
        const fromQuery = this.activatedRoute.snapshot.queryParams.returnUrl;
        // If the url is coming from the query string, check that is either
        // a relative url or an absolute url
        if (fromQuery &&
            !(fromQuery.startsWith(`${window.location.origin}/`) ||
                /\/[^\/].*/.test(fromQuery))) {
            // This is an extra check to prevent open redirects.
            throw new Error('Invalid return url. The return url needs to have the same origin as the current page.');
        }
        return (state && state.returnUrl) ||
            fromQuery ||
            ApplicationPaths.LoggedOut;
    }
};
LogoutComponent = __decorate([
    Component({
        selector: 'app-logout',
        templateUrl: './logout.component.html',
        styleUrls: ['./logout.component.css']
    })
], LogoutComponent);
export { LogoutComponent };
//# sourceMappingURL=logout.component.js.map