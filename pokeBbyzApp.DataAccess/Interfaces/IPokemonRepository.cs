namespace pokeBbyzApp.DataAccess.Interfaces
{
    public interface IPokemonRepository
    {
        int AddPokemon(Pokemon pokemon);
        void SetPokemonBaseStats(Pokemon pokemon);
    }
}
