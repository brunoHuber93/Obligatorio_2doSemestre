using ObligatorioP2_C_;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Obligatorio_P2_MVC.Models
{
    public class HuespedViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Contraseña { get; set; }
        [Required]
        [DisplayName("Tipo documento")]
        public TipoDocumento TipoDocumento { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(8)]
        [DisplayName("Numero documento")]
        public string NumeroDocumento { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        [DisplayName("Habitación")]
        public int Habitacion { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Fecha nacimiento")]
        public DateTime FechaNacimiento { get; set; }
        [DisplayName("Nivel de fidelización")]
        [Range(1, 4)]
        public int NivelFidelizacion { get; set; }


        
    }
}
