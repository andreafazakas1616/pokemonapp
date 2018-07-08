using pokeBbyzApp.BusinessLogic.Interfaces;
using pokeBbyzApp.DataAccess;
using pokeBbyzApp.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace pokeBbyzApp.BusinessLogic.Services
{
    public class PokemonSpeciesService: IPokemonSpeciesService
    {
        private readonly IPokemonSpeciesRepository _pokemonSpeciesRepository;

        public PokemonSpeciesService(IPokemonSpeciesRepository pokemonSpeciesRepository)
        {
            _pokemonSpeciesRepository = pokemonSpeciesRepository;
        }

        public List<PokemonSpecy> GetAllSpecies()
        {
           return _pokemonSpeciesRepository.GetAllSpecies();
        }

        public PokemonSpecy FindPokemonSpeciesById(int speciesId)
        {
            return _pokemonSpeciesRepository.FindPokemonSpeciesById(speciesId);
        }

        public void UpdatePokemonSpecies(PokemonSpecy pokemon)
        {
            _pokemonSpeciesRepository.UpdatePokemonSpecies(pokemon);
        }

        public void Delete(int speciesId)
        {
            _pokemonSpeciesRepository.Delete(speciesId);
        }

        public void Add(PokemonSpecy pokemonSpecies)
        {
            _pokemonSpeciesRepository.Add(pokemonSpecies);
        }

        public void SetStarterPokemonStatus(int pokemonId, bool isStarter)
        {
            _pokemonSpeciesRepository.SetStarterPokemonStatus(pokemonId, isStarter);
        }

        public bool GetStarterPokemonValue(string starterValue)
        {
            starterValue = starterValue.ToLower();
            string[] isStarter = new string[] { "yes", "y", "1", "true", "t" };
            string[] isNotStarter = new string[] { "no", "n", "0", "false", "f"};
            if(isStarter.Contains(starterValue))
            {
                return true;
            }
            else if(isNotStarter.Contains(starterValue))
            {
                return false;
            }
            else
            {
                throw new Exception();
            }
        }

        public bool CheckIfPokemonSpeciesExists(string pokemonName)
        {
            foreach (var name in _pokemonSpeciesRepository.GetAllSpecies().Select(p => p.Name))
            {
                if (pokemonName.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public void AddPokemonList(List<PokemonSpecy> pokemonSpeciesList)
        {
            _pokemonSpeciesRepository.AddPokemonList(pokemonSpeciesList);
        }
    }
}
