using System.ComponentModel.DataAnnotations;

namespace pokeBbyzApp.Models
{
    public class UserViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public bool Gender { get; set; }

        [Display(Name ="Remember me?")]
        public bool RememberMe { get; set; }
    }
}