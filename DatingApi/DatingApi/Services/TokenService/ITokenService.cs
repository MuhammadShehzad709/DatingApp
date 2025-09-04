using DatingApi.Data.Entities;

namespace DatingApi.Services.TokenService
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
