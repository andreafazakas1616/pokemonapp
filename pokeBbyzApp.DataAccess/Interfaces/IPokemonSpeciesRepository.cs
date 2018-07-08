using System.Collections.Generic;

namespace pokeBbyzApp.DataAccess.Interfaces
{
    public interface IPokemonSpeciesRepository
    {
        List<PokemonSpecy> GetAllSpecies();
        PokemonSpecy FindPokemonSpeciesById(int speciesId);
        void UpdatePokemonSpecies(PokemonSpecy newPokemon);
        void Delete(int speciesId);
        int Add(PokemonSpecy pokemonSpecies);
        void SetStarterPokemonStatus(int pokemonId, bool isStarter);
        void AddPokemonList(List<PokemonSpecy> pokemonSpeciesList);
    }
}
