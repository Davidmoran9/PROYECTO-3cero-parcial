using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AplicativoMejorado.Controllers
{
    public class AccountController : Controller
    {
        // Acción para cerrar sesión
        public IActionResult Logout()
        {
            // Eliminar todos los datos de la sesión
            HttpContext.Session.Clear();

            // Redirigir al usuario al login
            return RedirectToAction("Index", "Login"); // Redirige a la acción Index del controlador Login
        }
    }
}
