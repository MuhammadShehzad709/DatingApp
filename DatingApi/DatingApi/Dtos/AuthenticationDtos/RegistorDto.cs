using System.ComponentModel.DataAnnotations;

namespace DatingApi.Dtos.AuthenticationDtos
{
    public record RegistorDto(
        [Required]
        string DisplayName,
        [Required]
        string Email,
        [Required]
        string Password);
 
}
