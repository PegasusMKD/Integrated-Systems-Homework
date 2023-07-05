namespace ISH.Service.Dtos.Authentication
{
    public class LoginResponseDto
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
        public IList<string>? roles { get; set; }
        public string UserName { get; set; }
    }
}
