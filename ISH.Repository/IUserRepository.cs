using ISH.Data.Authentication;

namespace ISH.Repository
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        User UpdateUser(User user);
        void DeleteUserByUsername(string username);
        List<User> GetAllUsers();
        User GetUserById(string id);
        void DeleteUserById(string id);
    }
}
