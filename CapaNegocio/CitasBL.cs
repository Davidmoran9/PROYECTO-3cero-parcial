using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class CitasBL
    {
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

        public CitasCLS RecuperarCita(int idCita)
        {
            CitasDAL obj = new CitasDAL();
            return obj.RecuperarCita(idCita);
        }

        public int GuardarCambiosCita(CitasCLS objCita)
        {
            CitasDAL obj = new CitasDAL();
            return obj.GuardarCambiosCita(objCita);
        }


    }
}
