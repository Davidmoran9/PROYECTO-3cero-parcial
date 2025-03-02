using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class MedicosBL
    {
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

        public MedicosCLS RecuperarMedico(int idMedico)
        {
            MedicosDAL obj = new MedicosDAL();
            return obj.RecuperarMedico(idMedico);
        }

        public int GuardarCambiosMedico(MedicosCLS objMedico)
        {
            MedicosDAL obj = new MedicosDAL();
            return obj.GuardarCambiosMedico(objMedico);
        }

        public int EliminarMedico(int id)
        {
            MedicosDAL obj = new MedicosDAL();
            return obj.EliminarMedico(id);
        }
    }
}
