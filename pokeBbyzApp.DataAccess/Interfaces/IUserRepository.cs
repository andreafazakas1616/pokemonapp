using System.Collections.Generic;

namespace pokeBbyzApp.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        User FindUserById(int id);
        int AddNewUser(User user);
        List<User> GetUsers();
        void SetUserStarterPokemon(User user);
        bool CheckIfAdmin(int userId);
        bool HasStarterPokemon(int userId);
    }
}
