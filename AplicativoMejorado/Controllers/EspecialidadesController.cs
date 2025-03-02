using CapaDatos;
using CapaEntidad;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AplicativoMejorado.Controllers
{
    public class EspecialidadesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<EspecialidadesCLS> ListarEspecialidades()
        {
            EspecialidadesDAL obj = new EspecialidadesDAL();
            return obj.ListarEspecialidades();
        }

        public int GuardarEspecialidad(EspecialidadesCLS objEspecialidad)
        {
            EspecialidadesDAL obj = new EspecialidadesDAL();
            return obj.GuardarEspecialidad(objEspecialidad);
        }

        public EspecialidadesCLS RecuperarEspecialidad(int id)
        {
            EspecialidadesDAL obj = new EspecialidadesDAL();
            var especialidad = obj.RecuperarEspecialidad(id);
            return especialidad ?? new EspecialidadesCLS(); // Devuelve un objeto vacío en caso de null
        }

        public string GuardarCambiosEspecialidad(EspecialidadesCLS objEspecialidad)
        {
            try
            {
                EspecialidadesDAL obj = new EspecialidadesDAL();
                int resultado = obj.GuardarCambiosEspecialidad(objEspecialidad);

                if (resultado > 0)
                    return "1"; // Éxito
                else
                    return "0"; // No se modificó nada
            }
            catch (Exception ex)
            {
                return "Error en Controller: " + ex.Message;
            }
        }



        [HttpGet]
        public JsonResult EliminarEspecialidad(int id)
        {
            EspecialidadesDAL obj = new EspecialidadesDAL();
            int resultado = obj.EliminarEspecialidad(id);

            return Json(new { success = (resultado > 0) });
        }
    }
}
