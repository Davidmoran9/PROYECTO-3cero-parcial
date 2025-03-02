using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AplicativoMejorado.Controllers
{
    public class FacturacionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<FacturacionCLS> ListarFacturacion()
        {
            FacturacionDAL obj = new FacturacionDAL();
            return obj.ListarFacturacion();
        }

        public int GuardarFacturacion(FacturacionCLS objFacturacion)
        {
            FacturacionDAL obj = new FacturacionDAL();
            return obj.GuardarFacturacion(objFacturacion);
        }

        [HttpGet]
        public FacturacionCLS RecuperarFacturacion([FromQuery] int id)
        {
            FacturacionDAL obj = new FacturacionDAL();
            return obj.RecuperarFacturacion(id);
        }

        public int GuardarCambiosFacturacion(FacturacionCLS objFacturacion)
        {
            FacturacionDAL obj = new FacturacionDAL();
            return obj.GuardarCambiosFacturacion(objFacturacion);
        }

        [HttpGet]
        public JsonResult EliminarFacturacion(int id)
        {
            FacturacionDAL obj = new FacturacionDAL();
            int resultado = obj.EliminarFacturacion(id);

            return Json(new { success = (resultado > 0) });
        }
    }
}
