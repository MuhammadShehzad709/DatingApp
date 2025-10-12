namespace DatingApi.Data.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public required string Url { get; set; }
        public string? publicID { get; set; }
        // Navigation Properties
        public Member member { get; set; } = null!;
        public Guid MemberID { get; set; }
    }
}
