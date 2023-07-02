using System.Security.Claims;
using ISH.Service.Dtos.Authentication;

namespace ISH.Service
{
    public interface IUserService
    {
        UserDto GetUser(string username);
        List<UserDto> GetUsers();
        void UpdateUser(UserDto userDto);
        void DeleteUser(string username);
        UserDto GetUserById(string id);
        UserDto? GetUserByClaims(ClaimsPrincipal  claimsPrincipal);
    }
}
