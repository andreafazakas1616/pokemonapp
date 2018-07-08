namespace pokeBbyzApp.DataAccess.Interfaces
{
    public interface IUserService
    {
        bool CheckIfValidUsernameAndEmail(User user);
        bool HasStarterPokemon(int userId);
        User FindUserById(object userId);
    }
}
