namespace DatingApi.Dtos.MemberDtos;

public record GetMemberDto(
    Guid? Id,
    string DisplayName,
    DateOnly DateOfBirth,
    string? ImageUrl,
    DateTime? CreatedAt,
    DateTime LastActive,
    string Gender,
    string? Description,
    string City,
    string Country);
