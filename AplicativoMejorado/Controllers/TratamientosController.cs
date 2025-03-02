using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AplicativoMejorado.Controllers
{
    public class TratamientosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<TratamientosCLS> ListarTratamientos()
        {
            TratamientosDAL obj = new TratamientosDAL();
            return obj.ListarTratamientos();
        }

        public int GuardarTratamiento(TratamientosCLS objTratamiento)
        {
            TratamientosDAL obj = new TratamientosDAL();
            return obj.GuardarTratamiento(objTratamiento);
        }

        [HttpGet]
        public TratamientosCLS RecuperarTratamiento([FromQuery] int id)
        {
            TratamientosDAL obj = new TratamientosDAL();
            return obj.RecuperarTratamiento(id);
        }

        public int GuardarCambiosTratamiento(TratamientosCLS objTratamiento)
        {
            TratamientosDAL obj = new TratamientosDAL();
            return obj.GuardarCambiosTratamiento(objTratamiento);
        }

        [HttpGet]
        public JsonResult EliminarTratamiento(int id)
        {
            TratamientosDAL obj = new TratamientosDAL();
            int resultado = obj.EliminarTratamiento(id);

            return Json(new { success = (resultado > 0) });
        }
    }
}
