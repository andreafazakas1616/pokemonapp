using pokeBbyzApp.DataAccess;

namespace pokeBbyzApp.BusinessLogic.Interfaces
{
    public interface IPokemonService
    {
        string GetPokemonGender();
        void SetPokemonStats(Pokemon pokemon);
    }
}
