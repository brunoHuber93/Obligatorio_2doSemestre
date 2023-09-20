using System.ComponentModel.DataAnnotations;

namespace Obligatorio_P2_MVC.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Contraseña { get; set; }

    }
}
