using pokeBbyzApp.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace pokeBbyzApp.DataAccess.Repositories
{
    public class PokemonTypesRepository : IPokemonTypesRepository
    {
        private readonly pokeEntities _context;

        public PokemonTypesRepository(pokeEntities context)
        {
            _context = context;
        }

        public List<PokemonType> GetAllTypes()
        {
            return _context.PokemonTypes.ToList();
        }

        public PokemonType GetType(int typeId)
        {
            return _context.PokemonTypes.FirstOrDefault(t => t.ID == typeId);
        }
    }
}
