using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Obligatorio_P2_MVC.Filters;
using Obligatorio_P2_MVC.Models;
using ObligatorioP2_C_;
using System.Threading;

namespace Obligatorio_P2_MVC.Controllers
{
    [Privado(Roles = "operador")]
    public class ProveedorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListarProveedores()
        {
            Hostal hostal = Hostal.Instancia;
            List<ProveedorViewModel> listaProveedores = new List<ProveedorViewModel>();

            foreach (var proveedor in hostal.ObtenerProveedores())
            {           
               listaProveedores.Add(new ProveedorViewModel() {Nombre = proveedor.NombreProveedor, Telefono = proveedor.Telefono, Direccion = proveedor.Direccion, Descuento = proveedor.Descuento });
               
            }
            return View(listaProveedores);
        }

        public ActionResult AplicarDescuento(string nombre)
        {
            Hostal hostal = Hostal.Instancia;
            Proveedor proveedor = hostal.ObtenerProveedor(nombre);
            ProveedorDescuentoViewModel proveedorModel = new ProveedorDescuentoViewModel() { Nombre = proveedor.NombreProveedor, Descuento = proveedor.Descuento };
            return View(proveedorModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AplicarDescuento(ProveedorDescuentoViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                ViewBag.Error = "Los datos del formulario no son válidos.";
                return View(model);
            }

            try
            {                
                var proveedor = Hostal.Instancia.ObtenerProveedor(model.Nombre);              
                Hostal.Instancia.ActualizarDescuentoProveedor(proveedor, model.Descuento);

                return RedirectToAction("ListarProveedores");

            }
            catch (System.Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(model);
            }
            
        }

       




    }
}
