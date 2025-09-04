using DatingApi.Data;
using DatingApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApi.Repositories.AuthenticationRepositories
{
    public class AuthenticationRepository(AppDbContext context): IAuthenticationRepository
    {
        public async Task<AppUser> Login(string UserIdentifier)
        {
            var data= await context.Users.FirstOrDefaultAsync(u=>u.Email== UserIdentifier);
            if (data is null)
            {
                return null;
            }
            return data;
        }

        public async Task<(AppUser, bool IsExist)> Register(AppUser user)
        {
            var data = await context.Users.FirstOrDefaultAsync(u=>u.Email== user.Email);
            if (data is null)
            {
                context.Users.Add(user);
                await context.SaveChangesAsync();
                return (user, false);
            }
            return (user, true);
        }
    }
}
