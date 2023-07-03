namespace ISH.Service.Dtos.Authentication
{
    public class ImportUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
