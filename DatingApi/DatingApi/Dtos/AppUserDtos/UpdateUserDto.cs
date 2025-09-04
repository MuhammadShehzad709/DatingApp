namespace DatingApi.Dtos.AppUserDtos
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
    }
}
