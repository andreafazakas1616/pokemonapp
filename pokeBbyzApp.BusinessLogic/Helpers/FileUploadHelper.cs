using OfficeOpenXml;
using pokeBbyzApp.BusinessLogic.Interfaces;
using pokeBbyzApp.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pokeBbyzApp.BusinessLogic.Helpers
{
    public class FileUploadHelper : IFileUpload
    {
        private readonly IPokemonSpeciesService _pokemonSpeciesService;
        private readonly IPokemonTypesService _pokemonTypesService;

        public FileUploadHelper(IPokemonSpeciesService pokemonSpeciesService, IPokemonTypesService pokemonTypesService)
        {
            _pokemonSpeciesService = pokemonSpeciesService;
            _pokemonTypesService = pokemonTypesService;
        }

        public List<PokemonSpecy> UploadPokemonFromExcel(HttpPostedFileBase file, List<string> errors)
        {
            var pokemonSpeciesList = new List<PokemonSpecy>();
            if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
            {
                string fileName = file.FileName;
                string fileContentType = file.ContentType;
                byte[] fileBytes = new byte[file.ContentLength];
                var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
               
                using (var package = new ExcelPackage(file.InputStream))
                {
                    var currentSheet = package.Workbook.Worksheets;
                    var workSheet = currentSheet.First();
                    var noOfCol = workSheet.Dimension.End.Column;
                    var noOfRow = workSheet.Dimension.End.Row;

                    for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                    {
                        var pokemon = new PokemonSpecy();
                        pokemon.Name = workSheet.Cells[rowIterator, 1].Value.ToString();
                        if (_pokemonSpeciesService.CheckIfPokemonSpeciesExists(pokemon.Name))
                        {
                            errors.Add(pokemon.Name);
                        }
                        else
                        {
                            pokemon.Type1 = _pokemonTypesService.FindPokemonTypeByTypeName(workSheet.Cells[rowIterator, 2].Value.ToString());
                            if (workSheet.Cells[rowIterator, 3] != null && !string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 3].Value?.ToString()))
                            {
                                pokemon.Type2 = _pokemonTypesService.FindPokemonTypeByTypeName(workSheet.Cells[rowIterator, 3].Value.ToString());
                            }
                            pokemon.IsStarterPokemon = _pokemonSpeciesService.GetStarterPokemonValue(workSheet.Cells[rowIterator, 4].Value.ToString());
                            pokemon.Attack = Int32.Parse(workSheet.Cells[rowIterator, 5].Value.ToString());
                            pokemon.Defense = Int32.Parse(workSheet.Cells[rowIterator, 6].Value.ToString());
                            pokemon.HP = Int32.Parse(workSheet.Cells[rowIterator, 7].Value.ToString());
                            pokemon.Speed = Int32.Parse(workSheet.Cells[rowIterator, 8].Value.ToString());
                            pokemonSpeciesList.Add(pokemon);
                        }
                    }
                }
            }
            return pokemonSpeciesList;
        }
    }
}
    
