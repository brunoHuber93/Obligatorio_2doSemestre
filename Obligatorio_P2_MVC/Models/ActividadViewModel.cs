using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Obligatorio_P2_MVC.Models
{
    public class ActividadViewModel
    {
       
        public int Id { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Fecha { get; set; }
        [DisplayName("Cupo Máximo")]
        public int CupoMaximo { get; set; }
        [DisplayName("Edad Mínima")]
        public int EdadMinima { get; set; }
        [DisplayName("Costo ($U)")]
        public decimal CostoActividad { get; set; }

    }
}
