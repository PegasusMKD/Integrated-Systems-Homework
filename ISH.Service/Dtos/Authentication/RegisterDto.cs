using System.ComponentModel.DataAnnotations;

namespace ISH.Service.Dtos.Authentication
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; } = null!;

        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Role { get; set; } = null!;
    }
}
