using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class EspecialidadesBL
    {
        public List<EspecialidadesCLS> ListarEspecialidades()
        {
            EspecialidadesDAL obj = new EspecialidadesDAL();
            return obj.ListarEspecialidades();
        }

        public EspecialidadesCLS RecuperarEspecialidad(int id)
        {
            EspecialidadesDAL obj = new EspecialidadesDAL();
            return obj.RecuperarEspecialidad(id);
        }

        public int GuardarEspecialidad(EspecialidadesCLS objEspecialidad)
        {
            EspecialidadesDAL obj = new EspecialidadesDAL();
            return obj.GuardarEspecialidad(objEspecialidad);
        }
        public int GuardarCambiosEspecialidad(EspecialidadesCLS objEspecialidad)
        {
            EspecialidadesDAL obj = new EspecialidadesDAL();
            return obj.GuardarCambiosEspecialidad(objEspecialidad);
        }


        public int EliminarEspecialidad(int id)
        {
            EspecialidadesDAL obj = new EspecialidadesDAL();
            return obj.EliminarEspecialidad(id);
        }
    }
}
