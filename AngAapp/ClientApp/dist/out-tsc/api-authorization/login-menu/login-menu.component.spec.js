import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { LoginMenuComponent } from './login-menu.component';
import { AuthorizeService } from '../authorize.service';
import { of } from 'rxjs';
describe('LoginMenuComponent', () => {
    let component;
    let fixture;
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [RouterTestingModule],
            declarations: [LoginMenuComponent]
        })
            .compileComponents();
    });
    beforeEach(() => {
        let authService = TestBed.inject(AuthorizeService);
        spyOn(authService, 'createUserManager').and.callFake(() => {
            const userManager = jasmine.createSpyObj([]);
            return Promise.resolve(userManager);
        });
        spyOn(authService, 'getUserFromStorage').and.returnValue(of(null));
        fixture = TestBed.createComponent(LoginMenuComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });
    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
//# sourceMappingURL=login-menu.component.spec.js.map