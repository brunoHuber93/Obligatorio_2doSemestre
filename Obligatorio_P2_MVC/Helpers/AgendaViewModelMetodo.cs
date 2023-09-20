using Obligatorio_P2_MVC.Models;
using ObligatorioP2_C_;

namespace Obligatorio_P2_MVC.Helpers
{
   
        public static class AgendaViewModelMetodo
        {
            public static AgendaViewModel CrearAgendaViewModel(Agenda agenda)
            {
                
                var agendaViewModel = new AgendaViewModel
                {
                    Id = agenda.Id,
                    Nombre = agenda._huesped.Nombre,
                    Apellido = agenda._huesped.Apellido,
                    Actividad = agenda._actividad.Nombre,
                    FechaActividad = agenda._actividad.Fecha,
                    Lugar = (agenda._actividad is ActividadHostal actHostal) ? actHostal.LugarEnHostal : string.Empty,
                    CostoActividad = agenda._actividad.ObtenerCosto(),
                    Estado = agenda.Estado,
                    Proveedor = (agenda._actividad is ActividadTerciarizada terciarizada) ? terciarizada._proveedor?.NombreProveedor : null
                };

                return agendaViewModel;
            }
        }

    
}
