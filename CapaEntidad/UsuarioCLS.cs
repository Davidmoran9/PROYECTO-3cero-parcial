using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class UsuarioCLS
    {
        public int Id { get; set; }            // Id del usuario
        public string Correo { get; set; }     // Correo del usuario
        public string Contrasena { get; set; } // Contraseña del usuario (sin cifrar)
        public string Rol { get; set; }        // Rol del usuario ('Medico', 'Admin', etc.)
    }
}