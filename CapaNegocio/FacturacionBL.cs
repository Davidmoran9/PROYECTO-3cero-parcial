using CapaDatos;
using CapaEntidad;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class FacturacionBL
    {
        public List<FacturacionCLS> ListarFacturacion()
        {
            FacturacionDAL obj = new FacturacionDAL();
            return obj.ListarFacturacion();
        }

        public int GuardarFacturacion(FacturacionCLS objFacturacion)
        {
            FacturacionDAL obj = new FacturacionDAL();
            return obj.GuardarFacturacion(objFacturacion);
        }

        public FacturacionCLS RecuperarFacturacion(int id)
        {
            FacturacionDAL obj = new FacturacionDAL();
            return obj.RecuperarFacturacion(id);
        }

        public int GuardarCambiosFacturacion(FacturacionCLS objFacturacion)
        {
            FacturacionDAL obj = new FacturacionDAL();
            return obj.GuardarCambiosFacturacion(objFacturacion);
        }

        public int EliminarFacturacion(int id)
        {
            FacturacionDAL obj = new FacturacionDAL();
            return obj.EliminarFacturacion(id);
        }
    }
}
