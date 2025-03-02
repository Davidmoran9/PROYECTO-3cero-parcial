using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CapaDatos;

namespace AplicativoMejorado.Controllers
{
    public class Login : Controller
    {
        private readonly UsuarioDAL _usuarioDAL;

        public Login()
        {
            _usuarioDAL = new UsuarioDAL();
        }

        // Acción para mostrar el formulario de login
        public IActionResult Index()
        {
            return View();
        }

        // Acción para procesar el login
        [HttpPost]
        public IActionResult Index(string correo, string contrasena)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                TempData["Error"] = "Por favor, complete ambos campos.";
                return View();
            }

            var usuario = _usuarioDAL.VerificarLogin(correo, contrasena);

            if (usuario != null)
            {
                // Guardar el rol en la sesión
                HttpContext.Session.SetString("Rol", usuario.Rol);

                // Iniciar la sesión del usuario (como lo hiciste antes)
                // ... tu código de autenticación con cookies

                return RedirectToAction("Index", "Home"); // Redirigir a la página principal
            }

            TempData["Error"] = "Credenciales incorrectas.";
            return View();
        }
    }
}
