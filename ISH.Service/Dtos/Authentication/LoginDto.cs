using System.ComponentModel.DataAnnotations;

namespace ISH.Service.Dtos.Authentication
{
    public class LoginDto
    {
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
