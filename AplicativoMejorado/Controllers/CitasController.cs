using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AplicativoMejorado.Controllers
{
    public class CitasController : Controller
    {
        public IActionResult Index()
        {
            return View();

        }

        public List<CitasCLS> ListarCitas()
        {
            CitasDAL obj = new CitasDAL();
            return obj.ListarCitas();
        }

        public List<CitasCLS> FiltrarCitas(int? pacienteId, int? medicoId, string estado)
        {
            CitasDAL obj = new CitasDAL();
            return obj.FiltrarCitas(pacienteId, medicoId, estado);
        }

        public int GuardarCita(CitasCLS objCita)
        {
            CitasDAL obj = new CitasDAL();
            return obj.GuardarCita(objCita);
        }

        [HttpGet]
        public CitasCLS recuperarCita([FromQuery] int id)
        {
            CitasDAL obj = new CitasDAL();
            return obj.RecuperarCita(id);
        }

        public int GuardarCambiosCita(CitasCLS objCita)
        {
            CitasDAL obj = new CitasDAL();
            return obj.GuardarCambiosCita(objCita);
        }

        [HttpGet]
        public JsonResult EliminarCita(int id)
        {
            CitasDAL obj = new CitasDAL();
            int resultado = obj.EliminarCita(id);

            return Json(new { success = (resultado > 0) }); // 🔹 Evalúa correctamente si se eliminó
        }


    }
}
