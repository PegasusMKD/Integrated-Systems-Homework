using ISH.Service.Dtos.Authentication;

namespace Integrated_Systems_Homework.ViewControllers.Models
{
    public class UpdateUserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string? NewPassword { get; set; } = null;
        public string? CurrentPassword { get; set; } = null;
        public bool EmailConfirmed { get; set; }
    }
}
