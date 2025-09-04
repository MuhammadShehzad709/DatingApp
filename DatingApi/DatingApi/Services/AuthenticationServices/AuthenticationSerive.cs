using DatingApi.Common.Extensions.AppUserMapper;
using DatingApi.Common.Extensions.Mappers.AuthenticationMapper;
using DatingApi.Common.Response;
using DatingApi.Dtos.AppUserDtos;
using DatingApi.Dtos.AuthenticationDtos;
using DatingApi.Repositories.AuthenticationRepositories;
using DatingApi.Services.TokenService;
using System.Text;

namespace DatingApi.Services.AuthenticationServices
{
    public class AuthenticationSerive(IAuthenticationRepository repository,ITokenService Token) : IAuthenticationService
    {
        public async Task<ApiResponse<GetUserDto>> Login(LoginDto loginDto)
        {
            var user = await repository.Login(loginDto.Email);
            if (user is null || !VerifyPasswordHashAndSalt(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return ApiResponse<GetUserDto>.ErrorResponse("Invalid email or password");
            }

            var mappedUser = user.Map();
            mappedUser.Token = Token.CreateToken(user);

            return ApiResponse<GetUserDto>.SuccessResponse(mappedUser);
        }

        public async Task<ApiResponse<GetUserDto>> Register(RegistorDto userDto)
        {
            var user = userDto.Map();
            CreatePasswordHashAndSalt(userDto.Password, out byte[] PasswordHash,out byte[] PasswordSalt);
            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;
            var (data,IsExist)=await repository.Register(user);
            if(IsExist)
            {
                return ApiResponse<GetUserDto>.ErrorResponse("User Already Exist");
            }
            var mappedUser = user.Map();
            mappedUser.Token = Token.CreateToken(user);
            return ApiResponse<GetUserDto>.SuccessResponse(mappedUser);
        }
        public void CreatePasswordHashAndSalt(string Password,out byte[] PasswordHash,out byte[] PasswordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
        }
        public bool VerifyPasswordHashAndSalt(string Password,byte[] PasswordHash,byte[] PasswordSalt)
        {
            using var hmac=new System.Security.Cryptography.HMACSHA512(PasswordSalt);
            var computash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
            return computash.SequenceEqual(PasswordHash);
        }
    }
}
