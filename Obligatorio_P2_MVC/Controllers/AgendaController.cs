using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Obligatorio_P2_MVC.Filters;
using Obligatorio_P2_MVC.Models;
using ObligatorioP2_C_;
using System.Reflection;
using Obligatorio_P2_MVC.Helpers;

namespace Obligatorio_P2_MVC.Controllers
{
    [Privado(Roles = "huesped")]
    public class AgendaController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.GetString("rol");
            if (HttpContext.Session.GetString("rol")== "huesped")
            {
               return RedirectToAction("Index", "Huesped");
            }
            else
            {
                return RedirectToAction("Index", "Operador");
            }
            
        }

        
        public IActionResult Agendar(int IdActividad)
        {   
            var huesped = Hostal.Instancia.ObtenerHuespedPorEmail(HttpContext.Session.GetString("email"));
            var actividad = Hostal.Instancia.ObtenerActividad(IdActividad);            
            var agenda = new Agenda(huesped, actividad);           
            
            Hostal.Instancia.AltaAgenda(agenda);

            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Los datos del formulario no son válidos.";
                return View("ListarActividadesPorFecha", "Actividad");
            }
            var agendaModel = AgendaViewModelMetodo.CrearAgendaViewModel(agenda);
            ViewBag.ActividadId = IdActividad;
            return View(agendaModel);
        }

        





    }
}
