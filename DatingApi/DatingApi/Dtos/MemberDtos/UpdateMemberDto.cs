namespace DatingApi.Dtos.MemberDtos;

public record UpdateMemberDto(
    string? DisplayName,
    string? Description,
    string? City,
    string? Country);

