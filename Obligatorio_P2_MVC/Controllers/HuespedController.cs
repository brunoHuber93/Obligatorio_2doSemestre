using Microsoft.AspNetCore.Mvc;
using Obligatorio_P2_MVC.Filters;
using Obligatorio_P2_MVC.Helpers;
using Obligatorio_P2_MVC.Models;
using ObligatorioP2_C_;
using System.Security.Cryptography.X509Certificates;

namespace Obligatorio_P2_MVC.Controllers
{
    
    public class HuespedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult RegistroHuesped()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult RegistroHuesped(HuespedViewModel model)
        {
            Hostal hostal = Hostal.Instancia;
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Los datos ingresados no son válidos";
                return View(model);
            }

            try
            {
                var huesped = new Huesped(model.Email, model.Contraseña, model.TipoDocumento, model.NumeroDocumento, model.Nombre, model.Apellido, model.Habitacion, model.FechaNacimiento, model.NivelFidelizacion);
                hostal.AltaUsuario(huesped);
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(model);
            }


        }

        [Privado(Roles = "huesped")]
        public IActionResult ListarAgendasDelHuesped(string email)
        {
            var listaAgendas = Hostal.Instancia.ListarAgendasOrdenadasPorFecha();
            var listaAgendasViewModel = new List<AgendaViewModel>();
            
            foreach (var agenda in listaAgendas)
            {
                if (agenda._huesped.Email == email)
                {
                    if(agenda._actividad.Fecha >= DateTime.Today)
                    {
                        var agendaModel = AgendaViewModelMetodo.CrearAgendaViewModel(agenda);

                        listaAgendasViewModel.Add(agendaModel);

                    }

                }
            }

            return View(listaAgendasViewModel);
        }


       [Privado(Roles = "huesped")]
        public IActionResult ListarDatosHuesped(string email)
        {
            var huesped = Hostal.Instancia.ObtenerHuespedPorEmail(email);
            
            List<HuespedViewModel> datosHuesped = new List<HuespedViewModel>();

            if (huesped.Email == email)
            {
                datosHuesped.Add(new HuespedViewModel() {
                    Email = huesped.Email,
                    TipoDocumento = huesped._tipoDocumento,
                    NumeroDocumento = huesped.NumeroDocumento,
                    Nombre = huesped.Nombre,
                    Apellido = huesped.Apellido,
                    Habitacion = huesped.Habitacion,
                    FechaNacimiento = huesped.FechaNacimiento,
                    NivelFidelizacion = huesped.NivelFidelizacion
                });
            }

            return View(datosHuesped);
        }


    }
}
