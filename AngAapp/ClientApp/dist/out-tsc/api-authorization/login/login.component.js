import { __decorate } from "tslib";
import { Component } from '@angular/core';
import { AuthenticationResultStatus } from '../authorize.service';
import { BehaviorSubject } from 'rxjs';
import { LoginActions, QueryParameterNames, ApplicationPaths, ReturnUrlType } from '../api-authorization.constants';
// The main responsibility of this component is to handle the user's login process.
// This is the starting point for the login process. Any component that needs to authenticate
// a user can simply perform a redirect to this component with a returnUrl query parameter and
// let the component perform the login and return back to the return url.
let LoginComponent = class LoginComponent {
    constructor(authorizeService, activatedRoute, router) {
        this.authorizeService = authorizeService;
        this.activatedRoute = activatedRoute;
        this.router = router;
        this.message = new BehaviorSubject(null);
    }
    async ngOnInit() {
        const action = this.activatedRoute.snapshot.url[1];
        switch (action.path) {
            case LoginActions.Login:
                await this.login(this.getReturnUrl());
                break;
            case LoginActions.LoginCallback:
                await this.processLoginCallback();
                break;
            case LoginActions.LoginFailed:
                const message = this.activatedRoute.snapshot.queryParamMap.get(QueryParameterNames.Message);
                this.message.next(message);
                break;
            case LoginActions.Profile:
                this.redirectToProfile();
                break;
            case LoginActions.Register:
                this.redirectToRegister();
                break;
            default:
                throw new Error(`Invalid action '${action}'`);
        }
    }
    async login(returnUrl) {
        const state = { returnUrl };
        const result = await this.authorizeService.signIn(state);
        this.message.next(undefined);
        switch (result.status) {
            case AuthenticationResultStatus.Redirect:
                break;
            case AuthenticationResultStatus.Success:
                await this.navigateToReturnUrl(returnUrl);
                break;
            case AuthenticationResultStatus.Fail:
                await this.router.navigate(ApplicationPaths.LoginFailedPathComponents, {
                    queryParams: { [QueryParameterNames.Message]: result.message }
                });
                break;
            default:
                throw new Error(`Invalid status result ${result.status}.`);
        }
    }
    async processLoginCallback() {
        const url = window.location.href;
        const result = await this.authorizeService.completeSignIn(url);
        switch (result.status) {
            case AuthenticationResultStatus.Redirect:
                // There should not be any redirects as completeSignIn never redirects.
                throw new Error('Should not redirect.');
            case AuthenticationResultStatus.Success:
                await this.navigateToReturnUrl(this.getReturnUrl(result.state));
                break;
            case AuthenticationResultStatus.Fail:
                this.message.next(result.message);
                break;
        }
    }
    redirectToRegister() {
        this.redirectToApiAuthorizationPath(`${ApplicationPaths.IdentityRegisterPath}?returnUrl=${encodeURI('/' + ApplicationPaths.Login)}`);
    }
    redirectToProfile() {
        this.redirectToApiAuthorizationPath(ApplicationPaths.IdentityManagePath);
    }
    async navigateToReturnUrl(returnUrl) {
        // It's important that we do a replace here so that we remove the callback uri with the
        // fragment containing the tokens from the browser history.
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
            ApplicationPaths.DefaultLoginRedirectPath;
    }
    redirectToApiAuthorizationPath(apiAuthorizationPath) {
        // It's important that we do a replace here so that when the user hits the back arrow on the
        // browser they get sent back to where it was on the app instead of to an endpoint on this
        // component.
        const redirectUrl = `${window.location.origin}/${apiAuthorizationPath}`;
        window.location.replace(redirectUrl);
    }
};
LoginComponent = __decorate([
    Component({
        selector: 'app-login',
        templateUrl: './login.component.html',
        styleUrls: ['./login.component.css']
    })
], LoginComponent);
export { LoginComponent };
//# sourceMappingURL=login.component.js.map