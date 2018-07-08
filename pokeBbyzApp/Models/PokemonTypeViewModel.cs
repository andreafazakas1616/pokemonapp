using System.ComponentModel.DataAnnotations;

namespace pokeBbyzApp.Models
{
    public class PokemonTypeViewModel
    {
        public int? ID { get; set; }
        [Display(Name ="Type")]
        public string Name { get; set; }
    }
}