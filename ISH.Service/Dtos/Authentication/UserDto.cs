namespace ISH.Service.Dtos.Authentication
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string[] Roles { get; set; } = null!;
    }
}
