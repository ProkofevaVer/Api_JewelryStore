using System.ComponentModel.DataAnnotations;

namespace Api_JewelryStore.Models
{
    public class RegisterDto
    {
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
