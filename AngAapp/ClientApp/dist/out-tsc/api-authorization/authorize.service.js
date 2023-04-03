import { __decorate } from "tslib";
import { Injectable } from '@angular/core';
import { UserManager } from 'oidc-client';
import { BehaviorSubject, concat, from } from 'rxjs';
import { filter, map, mergeMap, take, tap } from 'rxjs/operators';
import { ApplicationPaths, ApplicationName } from './api-authorization.constants';
export var AuthenticationResultStatus;
(function (AuthenticationResultStatus) {
    AuthenticationResultStatus[AuthenticationResultStatus["Success"] = 0] = "Success";
    AuthenticationResultStatus[AuthenticationResultStatus["Redirect"] = 1] = "Redirect";
    AuthenticationResultStatus[AuthenticationResultStatus["Fail"] = 2] = "Fail";
})(AuthenticationResultStatus || (AuthenticationResultStatus = {}));
let AuthorizeService = class AuthorizeService {
    constructor() {
        // By default pop ups are disabled because they don't work properly on Edge.
        // If you want to enable pop up authentication simply set this flag to false.
        this.popUpDisabled = true;
        this.userSubject = new BehaviorSubject(null);
    }
    isAuthenticated() {
        return this.getUser().pipe(map(u => !!u));
    }
    getUser() {
        return concat(this.userSubject.pipe(take(1), filter(u => !!u)), this.getUserFromStorage().pipe(filter(u => !!u), tap(u => this.userSubject.next(u))), this.userSubject.asObservable());
    }
    getAccessToken() {
        return from(this.ensureUserManagerInitialized())
            .pipe(mergeMap(() => from(this.userManager.getUser())), map(user => user && user.access_token));
    }
    // We try to authenticate the user in three different ways:
    // 1) We try to see if we can authenticate the user silently. This happens
    //    when the user is already logged in on the IdP and is done using a hidden iframe
    //    on the client.
    // 2) We try to authenticate the user using a PopUp Window. This might fail if there is a
    //    Pop-Up blocker or the user has disabled PopUps.
    // 3) If the two methods above fail, we redirect the browser to the IdP to perform a traditional
    //    redirect flow.
    async signIn(state) {
        await this.ensureUserManagerInitialized();
        let user = null;
        try {
            user = await this.userManager.signinSilent(this.createArguments());
            this.userSubject.next(user.profile);
            return this.success(state);
        }
        catch (silentError) {
            // User might not be authenticated, fallback to popup authentication
            console.log('Silent authentication error: ', silentError);
            try {
                if (this.popUpDisabled) {
                    throw new Error('Popup disabled. Change \'authorize.service.ts:AuthorizeService.popupDisabled\' to false to enable it.');
                }
                user = await this.userManager.signinPopup(this.createArguments());
                this.userSubject.next(user.profile);
                return this.success(state);
            }
            catch (popupError) {
                if (popupError.message === 'Popup window closed') {
                    // The user explicitly cancelled the login action by closing an opened popup.
                    return this.error('The user closed the window.');
                }
                else if (!this.popUpDisabled) {
                    console.log('Popup authentication error: ', popupError);
                }
                // PopUps might be blocked by the user, fallback to redirect
                try {
                    await this.userManager.signinRedirect(this.createArguments(state));
                    return this.redirect();
                }
                catch (redirectError) {
                    console.log('Redirect authentication error: ', redirectError);
                    return this.error(redirectError);
                }
            }
        }
    }
    async completeSignIn(url) {
        try {
            await this.ensureUserManagerInitialized();
            const user = await this.userManager.signinCallback(url);
            this.userSubject.next(user && user.profile);
            return this.success(user && user.state);
        }
        catch (error) {
            console.log('There was an error signing in: ', error);
            return this.error('There was an error signing in.');
        }
    }
    async signOut(state) {
        try {
            if (this.popUpDisabled) {
                throw new Error('Popup disabled. Change \'authorize.service.ts:AuthorizeService.popupDisabled\' to false to enable it.');
            }
            await this.ensureUserManagerInitialized();
            await this.userManager.signoutPopup(this.createArguments());
            this.userSubject.next(null);
            return this.success(state);
        }
        catch (popupSignOutError) {
            console.log('Popup signout error: ', popupSignOutError);
            try {
                await this.userManager.signoutRedirect(this.createArguments(state));
                return this.redirect();
            }
            catch (redirectSignOutError) {
                console.log('Redirect signout error: ', redirectSignOutError);
                return this.error(redirectSignOutError);
            }
        }
    }
    async completeSignOut(url) {
        await this.ensureUserManagerInitialized();
        try {
            const response = await this.userManager.signoutCallback(url);
            this.userSubject.next(null);
            return this.success(response && response.state);
        }
        catch (error) {
            console.log(`There was an error trying to log out '${error}'.`);
            return this.error(error);
        }
    }
    createArguments(state) {
        return { useReplaceToNavigate: true, data: state };
    }
    error(message) {
        return { status: AuthenticationResultStatus.Fail, message };
    }
    success(state) {
        return { status: AuthenticationResultStatus.Success, state };
    }
    redirect() {
        return { status: AuthenticationResultStatus.Redirect };
    }
    async ensureUserManagerInitialized() {
        var _a;
        (_a = this.userManager) !== null && _a !== void 0 ? _a : (this.userManager = await this.createUserManager());
    }
    async createUserManager() {
        const response = await fetch(ApplicationPaths.ApiAuthorizationClientConfigurationUrl);
        if (!response.ok) {
            throw new Error(`Could not load settings for '${ApplicationName}'`);
        }
        const settings = await response.json();
        settings.automaticSilentRenew = true;
        settings.includeIdTokenInSilentRenew = true;
        const userManager = new UserManager(settings);
        userManager.events.addUserSignedOut(async () => {
            await this.userManager.removeUser();
            this.userSubject.next(null);
        });
        return userManager;
    }
    getUserFromStorage() {
        return from(this.ensureUserManagerInitialized())
            .pipe(mergeMap(() => this.userManager.getUser()), map(u => u && u.profile));
    }
};
AuthorizeService = __decorate([
    Injectable({
        providedIn: 'root'
    })
], AuthorizeService);
export { AuthorizeService };
//# sourceMappingURL=authorize.service.js.map