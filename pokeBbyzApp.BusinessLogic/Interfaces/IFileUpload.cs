using pokeBbyzApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace pokeBbyzApp.BusinessLogic.Interfaces
{
    public interface IFileUpload
    {
        List<PokemonSpecy> UploadPokemonFromExcel(HttpPostedFileBase file, List<string> errors);
    }
}
