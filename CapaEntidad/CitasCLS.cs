using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class CitasCLS
    {
        public int? Id { get; set; }
        public int? PacienteId { get; set; }
        public int? MedicoId { get; set; }
        public DateTime? FechaHora { get; set; }
        public string Estado { get; set; }
        public int? LaboratorioId { get; set; } // Si aplica
    }

}