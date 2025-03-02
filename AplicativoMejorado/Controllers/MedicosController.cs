using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AplicativoMejorado.Controllers
{
    public class MedicosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<MedicosCLS> ListarMedicos()
        {
            MedicosDAL obj = new MedicosDAL();
            return obj.ListarMedicos();
        }

        public List<MedicosCLS> FiltrarMedicos(string nombre, string especialidad)
        {
            MedicosDAL obj = new MedicosDAL();
            return obj.FiltrarMedicos(nombre, especialidad);
        }

        public int GuardarMedico(MedicosCLS objMedico)
        {
            MedicosDAL obj = new MedicosDAL();
            return obj.GuardarMedico(objMedico);
        }

        [HttpGet]
        public MedicosCLS RecuperarMedico([FromQuery] int id)
        {
            MedicosDAL obj = new MedicosDAL();
            return obj.RecuperarMedico(id);
        }

        public int GuardarCambiosMedico(MedicosCLS objMedico)
        {
            MedicosDAL obj = new MedicosDAL();
            return obj.GuardarCambiosMedico(objMedico);
        }

        [HttpGet]
        public JsonResult EliminarMedico(int id)
        {
            MedicosDAL obj = new MedicosDAL();
            int resultado = obj.EliminarMedico(id);

            return Json(new { success = (resultado > 0) });
        }
    }
}
