using ISH.Service.Dtos.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ISH.Service
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginDto loginDto);
        Task<UserDto> Register(RegisterDto registerDto);

    }
}
