using System.ComponentModel.DataAnnotations;

namespace pokeBbyzApp.Models
{
    public class PokemonViewModel
    {
        public int ID { get; set; }
        public int Species { get; set; }
        public string Gender { get; set; }
        [Required(ErrorMessage = "You must set a nickname for your pokemon.")]
        public string Nickname { get; set; }
        public int OwnerID { get; set; }
    }
}