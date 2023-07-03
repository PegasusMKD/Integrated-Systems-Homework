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

        public List<UserDto> GetUsers() =>
            _userRepository.GetAllUsers().ConvertAll(_mapper.Map<UserDto>);

        public void UpdateUser(UserDto userDto) =>
            _userRepository.UpdateUser(_mapper.Map<User>(userDto));
    }
}
