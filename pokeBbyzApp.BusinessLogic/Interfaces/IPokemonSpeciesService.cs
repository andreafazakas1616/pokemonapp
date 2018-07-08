using pokeBbyzApp.DataAccess;
using System.Collections.Generic;

namespace pokeBbyzApp.BusinessLogic.Interfaces
{
    public interface IPokemonSpeciesService
    {
        List<PokemonSpecy> GetAllSpecies();
        PokemonSpecy FindPokemonSpeciesById(int speciesId);
        void UpdatePokemonSpecies(PokemonSpecy pokemon);
        void Delete(int speciesId);
        void Add(PokemonSpecy pokemonSpecies);
        void SetStarterPokemonStatus(int pokemonId, bool isStarter);
        bool GetStarterPokemonValue(string starterValue);
        bool CheckIfPokemonSpeciesExists(string pokemonName);
        void AddPokemonList(List<PokemonSpecy> pokemonSpeciesList);
    }
}
