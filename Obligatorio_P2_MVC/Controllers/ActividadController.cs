using Microsoft.AspNetCore.Mvc;
using Obligatorio_P2_MVC.Filters;
using Obligatorio_P2_MVC.Models;
using System.Reflection;

namespace Obligatorio_P2_MVC.Controllers
{
    public class ActividadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


      //  [Privado(Roles ="huesped, operador")]
        public IActionResult ListarActividadesPorFecha()
        {
            Hostal hostal = Hostal.Instancia;
            List<ActividadViewModel> listaActividades = new List<ActividadViewModel>();

            foreach (var actividad in hostal.ObtenerActividades())
            {
                if (actividad.Fecha == DateTime.Today)
                {
                    listaActividades.Add(new ActividadViewModel() { Id = actividad.Id, Nombre = actividad.Nombre, Descripcion = actividad.Descripcion, Fecha = actividad.Fecha, CupoMaximo = actividad.CantidadMaximaDePersonas, EdadMinima = actividad.EdadMinimaRequerida, CostoActividad = actividad.CostoDeActividad });
                }
            }
            return View(listaActividades);
        }



        [HttpPost]
       // [Privado(Roles = "huesped, operador")]
        public IActionResult ListarActividadesPorFecha(DateTime fecha)
        {
            Hostal hostal = Hostal.Instancia;
            List<ActividadViewModel> listaActividades = new List<ActividadViewModel>();           

            foreach (var actividad in hostal.ObtenerActividades())
            {
                if (actividad.Fecha == fecha)
                {
                    listaActividades.Add(new ActividadViewModel() { Id = actividad.Id, Nombre = actividad.Nombre, Descripcion = actividad.Descripcion, Fecha = actividad.Fecha, CupoMaximo = actividad.CantidadMaximaDePersonas, EdadMinima = actividad.EdadMinimaRequerida, CostoActividad = actividad.CostoDeActividad });
                }
                if (listaActividades.Count == 0)
                {
                    ViewBag.Error = "No hay actividades en esa fecha";
                    
                }
            }
            return View(listaActividades);
        }

    }
}
