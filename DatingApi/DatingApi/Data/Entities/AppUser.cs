namespace DatingApi.Data.Entities
{
    public class AppUser
    {
        public Guid Id { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public string? ImageUrl { get; set; }
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];
        // Navigation Property
        public Member member { get; set; } = null!;
    }
}
