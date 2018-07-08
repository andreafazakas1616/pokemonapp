using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using pokeBbyzApp.DataAccess.Interfaces;

namespace pokeBbyzApp.DataAccess.Repositories
{
    public class PokemonSpeciesRepository : IPokemonSpeciesRepository
    {
        private readonly pokeEntities _context;
        private readonly IPokemonTypesRepository _typesRepository;

        public PokemonSpeciesRepository(pokeEntities context, IPokemonTypesRepository typesRepository)
        {
            _context = context;
            _typesRepository = typesRepository;
        }

        public List<PokemonSpecy> GetAllSpecies()
        {
            return _context.PokemonSpecies.Include(x=>x.PokemonType).Include(x=>x.PokemonType1).ToList();
        }

        public PokemonSpecy FindPokemonSpeciesById(int speciesId)
        {
            PokemonSpecy pokemon =  _context.PokemonSpecies.Where(ps => ps.ID == speciesId).First();
            return pokemon;
        }

        public void UpdatePokemonSpecies(PokemonSpecy newPokemon)
        {
            PokemonSpecy oldPokemon = _context.PokemonSpecies.FirstOrDefault(x=>x.ID==newPokemon.ID);
            oldPokemon.Name = newPokemon.Name;
            if (newPokemon.FrontImage != null)
            {
                oldPokemon.FrontImage = newPokemon.FrontImage;
            }
            else
            {
                _context.Entry(oldPokemon).Property(p => p.FrontImage).IsModified = false;
            }
            if (newPokemon.BackImage != null)
            {
                oldPokemon.BackImage = newPokemon.BackImage;
            }
            else
            {
                _context.Entry(oldPokemon).Property(p => p.BackImage).IsModified = false;
            }

            oldPokemon.Type1 = newPokemon.PokemonType.ID;
            if (newPokemon.PokemonType1.ID != 0)
            {
                oldPokemon.Type2 = newPokemon.PokemonType1.ID;
            }
            else
            {
                oldPokemon.Type2 = null;
            }
            _context.SaveChanges();
        }

        public void Delete(int speciesId)
        {
            PokemonSpecy pokemon = new PokemonSpecy() { ID = speciesId};
            _context.PokemonSpecies.Attach(pokemon);
            _context.PokemonSpecies.Remove(pokemon);
            _context.SaveChanges();
        }

        public int Add(PokemonSpecy pokemonSpecies)
        {
            _context.PokemonSpecies.Add(pokemonSpecies);
            _context.SaveChanges();
            return pokemonSpecies.ID;
        }

        public void SetStarterPokemonStatus(int pokemonId, bool isStarter)
        {
            PokemonSpecy pokemon = FindPokemonSpeciesById(pokemonId);
            pokemon.IsStarterPokemon = isStarter;
            _context.SaveChanges();
        }

        public void AddPokemonList(List<PokemonSpecy> pokemonSpeciesList)
        {
            _context.PokemonSpecies.AddRange(pokemonSpeciesList);
            _context.SaveChanges();
        }
    }
}
