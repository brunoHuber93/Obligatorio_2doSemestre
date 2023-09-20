using ObligatorioP2_C_;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Obligatorio_P2_MVC.Models
{
    public class AgendaViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Actividad { get; set; }
        [DisplayName("Fecha de actividad")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime FechaActividad { get; set; }
        public string Lugar { get; set; }
        [DisplayName("Costo de actividad")]
        public string CostoActividad { get; set; }
        public EstadoAgenda Estado { get; set; }

        public string Proveedor { get; set; }
        
        [DisplayName("Tipo de documento")]
        public TipoDocumento TipoDocumento { get; set; }








    }
}
