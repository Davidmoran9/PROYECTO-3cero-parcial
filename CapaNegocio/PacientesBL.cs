using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class PacientesBL
    {
        public List<PacientesCLS> ListarPacientes()
        {
            PacientesDAL obj = new PacientesDAL();
            return obj.ListarPacientes();
        }

        public PacientesCLS RecuperarPaciente(int idPaciente)
        {
            PacientesDAL obj = new PacientesDAL();
            return obj.RecuperarPaciente(idPaciente);
        }

        public int GuardarPaciente(PacientesCLS objPaciente)
        {
            PacientesDAL obj = new PacientesDAL();
            return obj.GuardarPaciente(objPaciente);
        }

        public int GuardarCambiosPaciente(PacientesCLS objPaciente)
        {
            PacientesDAL obj = new PacientesDAL();
            return obj.GuardarCambiosPaciente(objPaciente);
        }

        public int EliminarPaciente(int id)
        {
            PacientesDAL obj = new PacientesDAL();
            return obj.EliminarPaciente(id);
        }
    }
}
