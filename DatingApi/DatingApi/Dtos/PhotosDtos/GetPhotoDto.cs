namespace DatingApi.Dtos.PhotosDtos;

public record GetPhotoDto(int Id,string Url,string? publicId,Guid MemberId);
