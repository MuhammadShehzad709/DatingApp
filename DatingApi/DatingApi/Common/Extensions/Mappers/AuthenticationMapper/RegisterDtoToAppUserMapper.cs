using DatingApi.Data.Entities;
using DatingApi.Dtos.AuthenticationDtos;

namespace DatingApi.Common.Extensions.Mappers.AuthenticationMapper
{
    public static class RegisterDtoToAppUserMapper
    {
        public static AppUser Map(this RegistorDto dto)
        {
            return new AppUser
            {
                Id= Guid.NewGuid(),
                DisplayName = dto.DisplayName,
                Email = dto.Email
            };
        }
    }
}
