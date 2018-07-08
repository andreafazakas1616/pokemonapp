using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace pokeBbyzApp.Models
{
    public class PokemonSpeciesViewModel
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public PokemonTypeViewModel PokemonType { get; set; }
        public PokemonTypeViewModel PokemonType1 { get; set; }
        public List<PokemonTypeViewModel> Types { get; set; }
        [Display(Name ="Front image")]
        public string FrontImage { get; set; }
        [Display(Name ="Back image")]
        public string BackImage { get; set; }
        [Display(Name = "Choose a front picture for this pokemon.")]
        public HttpPostedFileBase UploadedFrontImage { get; set; }
        [Display(Name = "Choose a back picture for this pokemon.")]
        public HttpPostedFileBase UploadedBackImage { get; set; }
        public bool IsStarterPokemon { get; set; }
    }
}