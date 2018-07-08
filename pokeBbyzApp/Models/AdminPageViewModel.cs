using System.Collections.Generic;

namespace pokeBbyzApp.Models
{
    public class AdminPageViewModel
    {
        public AdminPageViewModel()
        {
            Errors = new List<string>();
        }
        public List<PokemonSpeciesViewModel> PokemonSpeciesList { get; set; }
        public List<string> Errors { get; set; }
    }
}