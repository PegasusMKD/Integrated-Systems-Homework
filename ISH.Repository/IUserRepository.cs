using ISH.Data.Authentication;

namespace ISH.Repository
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        void UpdateUser(User user);
        void DeleteUserByUsername(string username);
        List<User> GetAllUsers();
        User GetUserById(string id);
    }
}
