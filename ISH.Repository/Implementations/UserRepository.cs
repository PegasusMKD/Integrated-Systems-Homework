using ISH.Data.Authentication;
using Microsoft.AspNetCore.Identity;

namespace ISH.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this._roleManager = roleManager;
        }
        
        // TODO: Implement these 2 after setting things up and making sure they work

        //public void AddUser(RegisterDTO registerDTO)
        //{
        //    var baseUser = new User()
        //    {
        //        Email = registerDTO.Email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = registerDTO.UserName,
        //        FirstName = registerDTO.FirstName,
        //        LastName = registerDTO.LastName,
        //    };

        //    if (registerDTO.Role.Equals(UserRoles.Tutor))
        //        baseUser.Tutor = new Tutor();
        //    else
        //        baseUser.Student = new Student();

        //    userManager.CreateAsync(baseUser).Wait();

        //    AssignRoleToUser(registerDTO, baseUser);
        //}

        //private void AssignRoleToUser(RegisterDTO registerDTO, User user)
        //{
        //    bool doesRoleExist = _roleManager.Roles.Any(role => role.Name.Equals(registerDTO.Role));
        //    if (!doesRoleExist)
        //    {
        //        switch (registerDTO.Role)
        //        {
        //            case UserRoles.Student:
        //                _roleManager.CreateAsync(new IdentityRole(UserRoles.Student)).Wait();
        //                break;
        //            case UserRoles.Tutor:
        //                _roleManager.CreateAsync(new IdentityRole(UserRoles.Tutor)).Wait();
        //                break;
        //        }
        //    }
        //    userManager.AddToRoleAsync(user, registerDTO.Role).Wait();
        //}

        public User GetUserByUsername(string username)
        {
            var user = userManager.FindByNameAsync(username);
            return user.Result!;
        }

        public User? UpdateUser(User user)
        {
            var eUser = userManager.FindByIdAsync(user.Id).Result;
            if (eUser != null)
            {
                eUser.UserName = user.UserName ?? user.UserName;
                eUser.EmailConfirmed = user.EmailConfirmed;
                userManager.UpdateAsync(eUser).Wait();
                return userManager.FindByIdAsync(user.Id).Result;
            }

            return null;
        }

        public void DeleteUserByUsername(string username)
        {
            var user = GetUserByUsername(username);
            if (user != null)
                userManager.DeleteAsync(user).Wait();
        }

        public void DeleteUserById(string id)
        {
            var user = GetUserById(id);
            if (user != null)
                userManager.DeleteAsync(user).Wait();
        }

        public User GetUserById(string id)
        {
            var user = userManager.FindByIdAsync(id);
            return user.Result!;
        }


        public List<User> GetAllUsers()
        {
            return userManager.Users.ToList();
        }
    }
}
