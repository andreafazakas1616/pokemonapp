using pokeBbyzApp.DataAccess.Interfaces;
using System.Linq;

namespace pokeBbyzApp.DataAccess.Repositories
{
    public class PokemonRepository:IPokemonRepository
    {
        private readonly pokeEntities _context;
        public PokemonRepository(pokeEntities context)
        {
            _context = context;
        }

        public int AddPokemon(Pokemon pokemon)
        {
            _context.Pokemons.Add(pokemon);
            _context.SaveChanges();
            return pokemon.ID;
        }

        public void SetPokemonBaseStats(Pokemon pokemon)
        {
            var pokemonSpecies = _context.PokemonSpecies.FirstOrDefault(ps => ps.ID == pokemon.Species);
            pokemon.HP = pokemonSpecies?.HP ?? 0;
            pokemon.Attack = pokemonSpecies?.Attack ?? 0;
            pokemon.Defense = pokemonSpecies?.Defense ?? 0;
            pokemon.Speed = pokemonSpecies?.Speed ?? 0;
        }
    }
}
