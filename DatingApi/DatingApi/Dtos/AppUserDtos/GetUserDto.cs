
namespace DatingApi.Dtos.AppUserDtos
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? Token {  get; set; }
    }
}
