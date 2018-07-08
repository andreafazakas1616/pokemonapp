using pokeBbyzApp.BusinessLogic.Interfaces;
using System;
using pokeBbyzApp.DataAccess;
using pokeBbyzApp.DataAccess.Repositories;

namespace pokeBbyzApp.BusinessLogic.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly PokemonRepository _pokemonRepository;

        public PokemonService(PokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        public string GetPokemonGender()
        {
            var randomGender =  new Random().Next(100) % 2 == 0;
            if (randomGender == true)
            {
                return "Female";
            }
            else
            {
                return "Male";
            }
        }

        public DateTime GetPokemonBirthDate(int pokemonId)
        {
            return DateTime.Now;
        }
       
        public void SetPokemonStats(Pokemon pokemon)
        {
            Random random = new Random();
            pokemon.Happiness = 100;
            pokemon.BirthDate = DateTime.Now;
            pokemon.Growthrate = Convert.ToDecimal(random.NextDouble() * (1 - 0.7) + 0.7);
            _pokemonRepository.SetPokemonBaseStats(pokemon);
            _pokemonRepository.AddPokemon(pokemon);
        }
    }
}
