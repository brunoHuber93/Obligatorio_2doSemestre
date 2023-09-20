using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Obligatorio_P2_MVC.Models
{
    public class ProveedorViewModel
    {
        
        [Required]
        public string Nombre { get; set; }

        [Required]
        [DisplayName("Teléfono")]
        public string Telefono { get; set; }

        [Required]
        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        [Required]
        [DisplayName("Descuento U$")]
        public decimal Descuento { get; set; }

    }

    public class ProveedorDescuentoViewModel
    {

        [Required]
        public string Nombre { get; set; }        
        
        [Required]
        [DisplayName("Descuento U$")]
        public decimal Descuento { get; set; }

    }
}
