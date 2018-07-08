using System.Collections.Generic;

namespace pokeBbyzApp.DataAccess.Interfaces
{
    public interface IPokemonTypesRepository
    {
        List<PokemonType> GetAllTypes();
        PokemonType GetType(int typeId);
    }
}
