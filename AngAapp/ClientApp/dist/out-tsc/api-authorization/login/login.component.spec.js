import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { LoginComponent } from './login.component';
import { ActivatedRoute, UrlSegment, convertToParamMap, Router } from '@angular/router';
import { LoginActions } from '../api-authorization.constants';
import { AuthorizeService } from '../authorize.service';
import { HomeComponent } from 'src/app/home/home.component';
describe('LoginComponent', () => {
    let component;
    let fixture;
    let router;
    beforeEach(() => {
        let tempParams = { id: '1234' };
        let segment0 = new UrlSegment('segment0', {});
        let segment1 = new UrlSegment(LoginActions.Login, {});
        let urlSegments = [segment0, segment1];
        TestBed.configureTestingModule({
            imports: [
                RouterTestingModule.withRoutes([
                    { path: 'authentication/login-failed', component: HomeComponent }
                ])
            ],
            declarations: [LoginComponent, HomeComponent],
            providers: [{
                    provide: ActivatedRoute, useValue: {
                        snapshot: {
                            paramMap: convertToParamMap(tempParams),
                            url: urlSegments,
                            queryParams: tempParams
                        }
                    }
                }]
        }).compileComponents();
        router = TestBed.inject(Router);
        spyOn(router, 'navigate').and.returnValue(Promise.resolve(true));
    });
    beforeEach(() => {
        let authService = TestBed.inject(AuthorizeService);
        spyOn(authService, 'createUserManager').and.callFake(() => {
            const userManager = jasmine.createSpyObj([]);
            return Promise.resolve(userManager);
        });
        fixture = TestBed.createComponent(LoginComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=login.component.spec.js.map