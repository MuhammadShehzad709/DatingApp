using System.ComponentModel.DataAnnotations;

namespace DatingApi.Dtos.AuthenticationDtos
{
    public record RegistorDto(
        [Required]
        string DisplayName,
        [Required]
        string Email,
        [Required]
        string Password,
        [Required]
        string Gender,
        [Required]
        string City,
        [Required]
        string Country,
        [Required]
        DateOnly DateOfBirth);
 
}
