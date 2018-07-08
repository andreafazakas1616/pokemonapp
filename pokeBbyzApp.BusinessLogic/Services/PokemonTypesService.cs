using pokeBbyzApp.BusinessLogic.Interfaces;
using pokeBbyzApp.DataAccess.Interfaces;
using System.Collections.Generic;
using pokeBbyzApp.DataAccess;
using System.Linq;

namespace pokeBbyzApp.BusinessLogic.Services
{
    public class PokemonTypesService : IPokemonTypesService
    {
        private readonly IPokemonTypesRepository _typesRepository;

        public PokemonTypesService(IPokemonTypesRepository typesRepository)
        {
            _typesRepository = typesRepository;
        }

        public List<PokemonType> GetAllTypes()
        {
            return _typesRepository.GetAllTypes();
        }

        public int FindPokemonTypeByTypeName(string pokemonType)
        {
            return  _typesRepository.GetAllTypes().Where(t => t.Name == pokemonType).Select(t => t.ID).First();
        }
    }
}
