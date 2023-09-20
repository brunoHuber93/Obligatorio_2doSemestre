using Microsoft.AspNetCore.Mvc;
using Obligatorio_P2_MVC.Filters;
using Obligatorio_P2_MVC.Models;

namespace Obligatorio_P2_MVC.Controllers
{
    
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                List<Usuario> listaUsuarios = Hostal.Instancia.ObtenerUsuarios();

                foreach (Usuario usuario in listaUsuarios)
                {
                    if (usuario.Email == model.Email && usuario.Contraseña == model.Contraseña) 
                    {
                        string rolUsuario = Hostal.Instancia.ObtenerRol(model.Email);
                        HttpContext.Session.SetString("email", model.Email);
                        HttpContext.Session.SetString("rol", rolUsuario);
                        if (usuario is Huesped)
                        {
                             return RedirectToAction("Index", "Huesped");                            
                        }
                        if (usuario is Operador) 
                        {
                            return RedirectToAction("Index", "Operador");
                        }
                    }
                    else
                    {
                        if (usuario == null)
                        {
                            ViewBag.Error = "Los datos del usuario no son correctos";
                            return View(model);
                            
                        }
                    }
                    
                }
            }

            ViewBag.Error = "Credenciales invalidas";
            return View(model);

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        





    }
}
