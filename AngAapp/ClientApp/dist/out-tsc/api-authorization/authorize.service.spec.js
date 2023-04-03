import { TestBed, inject } from '@angular/core/testing';
import { AuthorizeService } from './authorize.service';
describe('AuthorizeService', () => {
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [AuthorizeService]
        });
    });
    it('should be created', inject([AuthorizeService], (service) => {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=authorize.service.spec.js.map