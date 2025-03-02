using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AplicativoMejorado.Controllers
{
    public class PacientesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<PacientesCLS> ListarPacientes()
        {
            PacientesDAL obj = new PacientesDAL();
            return obj.ListarPacientes();
        }

        public int GuardarPaciente(PacientesCLS objPaciente)
        {
            PacientesDAL obj = new PacientesDAL();
            return obj.GuardarPaciente(objPaciente);
        }

        [HttpGet]
        public PacientesCLS RecuperarPaciente([FromQuery] int id)
        {
            PacientesDAL obj = new PacientesDAL();
            return obj.RecuperarPaciente(id);
        }

        public int GuardarCambiosPaciente(PacientesCLS objPaciente)
        {
            PacientesDAL obj = new PacientesDAL();
            return obj.GuardarCambiosPaciente(objPaciente);
        }

        [HttpGet]
        public JsonResult EliminarPaciente(int id)
        {
            PacientesDAL obj = new PacientesDAL();
            int resultado = obj.EliminarPaciente(id);

            return Json(new { success = (resultado > 0) });
        }
    }
}
