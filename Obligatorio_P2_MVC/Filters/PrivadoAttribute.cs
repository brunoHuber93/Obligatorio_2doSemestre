using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Obligatorio_P2_MVC.Filters
{
    public class PrivadoAttribute : ActionFilterAttribute
    {
        public string Roles { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var emailUsuario = context.HttpContext.Session.GetString("email");
            var rolUsuario = context.HttpContext.Session.GetString("rol");

            if (!string.IsNullOrEmpty(emailUsuario)) 
            {
                if (string.IsNullOrEmpty(this.Roles))
                    base.OnActionExecuting(context);               
                else if (this.Roles.Contains(rolUsuario))
                    base.OnActionExecuting(context);
                else                    
                    context.Result = new RedirectToActionResult("Login", "Login", null);

            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "Login", null);
            }
        }
    }
   
}
