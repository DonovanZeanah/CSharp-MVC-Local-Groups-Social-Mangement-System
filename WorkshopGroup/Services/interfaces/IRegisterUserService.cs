using Microsoft.AspNet.Identity;

namespace WorkshopGroup.Services.interfaces
{
    public interface IRegisterUserService
    {
        Task<IdentityResult> RegisterUserAsync(string emailAddress, string password);
    }
}
