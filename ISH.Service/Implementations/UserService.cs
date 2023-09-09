using System.Security.Claims;
using System.Text;
using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using ISH.Data.Authentication;
using ISH.Repository;
using ISH.Service.Dtos.Authentication;
using ISH.Service.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace ISH.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, UserManager<User> userManager)
        {
            this._userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public void DeleteUser(string id) =>
            _userRepository.DeleteUserById(id);

        public UserDto GetUser(string username) =>
            _mapper.Map<UserDto>(_userRepository.GetUserByUsername(username));

        public UserDto GetUserById(string id)
        {
            var user = _userRepository.GetUserById(id);
            var dto = _mapper.Map<UserDto>(user);
            dto.Roles = _userManager.GetRolesAsync(user).Result.ToArray();
            return dto;
        }

        public UserDto? GetUserByClaims(ClaimsPrincipal claimsPrincipal)
        {
            var usernameClaim = claimsPrincipal.Identity!.Name;
            return usernameClaim == null ? null : GetUser(usernameClaim);
        }

        public async Task ImportUsers(IExcelDataReader reader)
        {
            while (reader.Read()) //Each row of the file
            {
                await ImportUser(new ImportUserDto
                {
                    Email = reader.GetValue(0).ToString()!,
                    Password = reader.GetValue(1).ToString()!,
                    Role = reader.GetValue(2) != null ? reader.GetValue(2).ToString() : null,
                });
            }
        }

        public async Task ImportUser(ImportUserDto importDto)
        {
            var userExists = await _userManager.FindByNameAsync(importDto.Email);
            if (userExists != null)
                return;

            User user = new()
            {
                Email = importDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = importDto.Email
            };

            var result = await _userManager.CreateAsync(user, importDto.Password);
            if (!result.Succeeded)
            {
                StringBuilder stringBuilderErrorMessages = new();
                foreach (var errorMessage in result.Errors)
                {
                    stringBuilderErrorMessages.AppendLine(errorMessage.Code + ", Message:" + errorMessage.Description);
                }

                return;
            }

            UserRoles role = UserRoles.User;
            if (importDto.Role != null && importDto.Role.ToLower() == "administrator")
                role = UserRoles.Administrator;

            await _userManager.AddToRoleAsync(user, role.GetDisplay());

            UserDto dto = _mapper.Map<UserDto>(user);
            dto.Roles = (await _userManager.GetRolesAsync(user)).ToArray();
        }

        public List<UserDto> GetUsers()
        {
            var dtos = new List<UserDto>();
            var users = _userRepository.GetAllUsers();
            foreach (var user in users)
            {
                var dto = _mapper.Map<UserDto>(user);
                dto.Roles = _userManager.GetRolesAsync(user).Result.ToArray();
                dtos.Add(dto);
            }

            return dtos;
        }

        public void UpdateUser(UserDto userDto, string currentPassword, string newPassword)
        {
            var copyOfUser = _userRepository.GetUserById(userDto.Id);
            var user = _mapper.Map<User>(userDto);
            user = _userRepository.UpdateUser(user);
            if (newPassword != null && currentPassword != null)
                _userManager.ChangePasswordAsync(user, currentPassword, newPassword).Wait();

            if (userDto.Roles != null && userDto.Roles.Length > 0)
            {
                _userManager.RemoveFromRolesAsync(copyOfUser, new List<string> { "User", "Administrator" }).Wait();
                _userManager.AddToRolesAsync(user, userDto.Roles).Wait();
            }
        }
    }
}
