using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class TratamientosBL
    {
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

        public TratamientosCLS RecuperarTratamiento(int idTratamiento)
        {
            TratamientosDAL obj = new TratamientosDAL();
            return obj.RecuperarTratamiento(idTratamiento);
        }

        public int GuardarCambiosTratamiento(TratamientosCLS objTratamiento)
        {
            TratamientosDAL obj = new TratamientosDAL();
            return obj.GuardarCambiosTratamiento(objTratamiento);
        }

        public int EliminarTratamiento(int id)
        {
            TratamientosDAL obj = new TratamientosDAL();
            return obj.EliminarTratamiento(id);
        }

    }
}
