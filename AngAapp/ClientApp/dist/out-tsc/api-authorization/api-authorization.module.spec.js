import { ApiAuthorizationModule } from './api-authorization.module';
describe('ApiAuthorizationModule', () => {
    let apiAuthorizationModule;
    beforeEach(() => {
        apiAuthorizationModule = new ApiAuthorizationModule();
    });
    it('should create an instance', () => {
        expect(apiAuthorizationModule).toBeTruthy();
    });
});
//# sourceMappingURL=api-authorization.module.spec.js.map