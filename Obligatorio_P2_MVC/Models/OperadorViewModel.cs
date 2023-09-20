using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Obligatorio_P2_MVC.Models
{
    public class OperadorViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Contraseña { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [DisplayName("Fecha Ingreso")]
        public DateTime FechaIngresoTrabajo { get; set; }

    }
}
