using pokeBbyzApp.DataAccess;
using System.Collections.Generic;

namespace pokeBbyzApp.BusinessLogic.Interfaces
{
    public interface IPokemonTypesService
    {
        List<PokemonType> GetAllTypes();
        int FindPokemonTypeByTypeName(string pokemonType);
    }
}
