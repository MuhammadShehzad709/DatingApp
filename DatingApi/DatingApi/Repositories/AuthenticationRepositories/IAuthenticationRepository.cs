using DatingApi.Data.Entities;

namespace DatingApi.Repositories.AuthenticationRepositories
{
    public interface IAuthenticationRepository
    {
        Task<AppUser>Login(string UserIdentifier);
        Task<(AppUser,bool IsExist)> Register(AppUser user);
    }
}
