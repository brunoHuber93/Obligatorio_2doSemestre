using Microsoft.AspNetCore.Mvc;
using Obligatorio_P2_MVC.Models;
using ObligatorioP2_C_;
using Obligatorio_P2_MVC.Helpers;
using Obligatorio_P2_MVC.Filters;

namespace Obligatorio_P2_MVC.Controllers
{
   [Privado(Roles = "operador")]
    public class OperadorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       

        public IActionResult ListarDatosOperador(string email)
        {
            var operador = Hostal.Instancia.ObtenerOperadorPorEmail(email);
            OperadorViewModel datosOperador = null;

            if (operador != null && operador.Email == email)
            {
                datosOperador = new OperadorViewModel()
                {
                    Email = operador.Email,
                    Nombre = operador.Nombre,
                    Apellido = operador.Apellido,
                    FechaIngresoTrabajo = operador.FechaIngresoTrabajo
                };
            }

            return View(datosOperador);
        }

        

        public IActionResult ListarAgendaPorFecha()
        {
            Hostal hostal = Hostal.Instancia;
            List<AgendaViewModel> listaAgendas = new List<AgendaViewModel>();

            foreach (var agenda in hostal.ObtenerAgendas())
            {
                if (agenda._actividad.Fecha == DateTime.Today)
                {
                    var agendaModel = AgendaViewModelMetodo.CrearAgendaViewModel(agenda);

                    listaAgendas.Add(agendaModel);                   
                }
            }

            return View(listaAgendas);
        }



        [HttpPost]
        public IActionResult ListarAgendaPorFecha(DateTime fecha)
        {
            Hostal hostal = Hostal.Instancia;
            List<AgendaViewModel> listaAgendas = new List<AgendaViewModel>();

            foreach (var agenda in hostal.ObtenerAgendas())
            {
                if (agenda._actividad.Fecha == fecha)
                {
                   
                    var agendaModel = AgendaViewModelMetodo.CrearAgendaViewModel(agenda);

                    listaAgendas.Add(agendaModel);
                }
            }

            return View(listaAgendas);
        }
       
        public IActionResult ListarAgendaHuesped()
        {
            Hostal hostal = Hostal.Instancia;
            List<AgendaViewModel> listaAgendas = new List<AgendaViewModel>();

            foreach (var agenda in hostal.ObtenerAgendas())
            {
                var agendaModel = AgendaViewModelMetodo.CrearAgendaViewModel(agenda);
                if (agendaModel == null)
                {
                    ViewBag.Error = "No hay agendas para mostrar";
                    return View("Index");
                }
                
                listaAgendas.Add(agendaModel);
            }
            return View(listaAgendas);
        }



        [HttpPost]
        public IActionResult ListarAgendaHuesped(TipoDocumento tipoDocumento, string numeroDocumento)
        {
            var listaAgendasHuesped = Hostal.Instancia.AgendasPorHuesped(tipoDocumento, numeroDocumento);

            List<AgendaViewModel> listaAgendas = new List<AgendaViewModel>();

            foreach (var agenda in listaAgendasHuesped)
            {
                if (agenda._huesped._tipoDocumento == tipoDocumento && agenda._huesped.NumeroDocumento == numeroDocumento)
                {                    
                    var agendaModel = AgendaViewModelMetodo.CrearAgendaViewModel(agenda);

                    listaAgendas.Add(agendaModel);
                }
            }

            return View(listaAgendas);
        }

        
        public IActionResult ConfirmarAgenda(int id)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Error al intentar agendar";
                return View("ListarAgendasPendientesPago");
            }
            try
            {
                Hostal.Instancia.ConfirmarEstadoAgenda(id);
                var agenda = Hostal.Instancia.ObtenerAgendaPorId(id);
                var agendaViewModel = AgendaViewModelMetodo.CrearAgendaViewModel(agenda);

                return View(agendaViewModel);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al confirmar la agenda: " + ex.Message;
                return View("Error");
            }
        }

        public IActionResult ListarAgendasPendientesPago()
        {
            Hostal hostal = Hostal.Instancia;
            List<AgendaViewModel> listaAgendas = new List<AgendaViewModel>();

            foreach (var agenda in hostal.ObtenerAgendas())
            {
                if (agenda.Estado == EstadoAgenda.PENDIENTE_PAGO)
                {
                    var agendaModel = AgendaViewModelMetodo.CrearAgendaViewModel(agenda);

                    listaAgendas.Add(agendaModel);
                }
            }

            return View(listaAgendas);
        }







    }
}