using DatingApi.Data.Entities;
using DatingApi.Dtos.AppUserDtos;

namespace DatingApi.Common.Extensions.AppUserMapper
{
    public static class AppUserToGetUserMapperDto
    {
        public static GetUserDto Map(this AppUser user)
        {
            return new GetUserDto
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
            };
        }
    }
}
