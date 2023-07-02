using System.Security.Claims;
using AutoMapper;
using ISH.Data.Authentication;
using ISH.Repository;
using ISH.Service.Dtos.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ISH.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this._userRepository = userRepository;
            _mapper = mapper;
        }

        public void DeleteUser(string username) =>
            _userRepository.DeleteUserByUsername(username);

        public UserDto GetUser(string username) =>
            _mapper.Map<UserDto>(_userRepository.GetUserByUsername(username));

        public UserDto GetUserById(string id) =>
            _mapper.Map<UserDto>(_userRepository.GetUserById(id));

        public UserDto? GetUserByClaims(ClaimsPrincipal claimsPrincipal)
        {
            var usernameClaim = claimsPrincipal.Identity!.Name;
            return usernameClaim == null ? null : GetUser(usernameClaim);
        }

        public List<UserDto> GetUsers() =>
            _userRepository.GetAllUsers().ConvertAll(_mapper.Map<UserDto>);

        public void UpdateUser(UserDto userDto) =>
            _userRepository.UpdateUser(_mapper.Map<User>(userDto));
    }
}
