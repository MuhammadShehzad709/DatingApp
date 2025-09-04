using DatingApi.Common.Response;
using DatingApi.Dtos.AppUserDtos;
using DatingApi.Dtos.AuthenticationDtos;

namespace DatingApi.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<GetUserDto>> Register(RegistorDto userDto);
        Task<ApiResponse<GetUserDto>> Login(LoginDto loginDto);
    }
}
