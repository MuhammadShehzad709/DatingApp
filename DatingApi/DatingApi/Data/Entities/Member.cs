using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApi.Data.Entities
{
    public class Member
    {
        public Guid? Id { get; set; }
        public required string DisplayName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime LastActive {  get; set; }
        public required string Gender { get; set; }
        public string? Description { get; set; }
        public required string City { get; set; }
        public required string  Country { get; set; }

        // Navigation Properties
        public List<Photo> photos { get; set; } = [];
        [ForeignKey(nameof(Id))]
        public AppUser User { get; set; } = null!;
    }
}
