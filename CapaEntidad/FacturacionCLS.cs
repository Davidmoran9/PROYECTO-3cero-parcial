using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FacturacionCLS
{
    public int Id { get; set; }
    public int PacienteId { get; set; }
    public decimal Monto { get; set; }
    public string MetodoPago { get; set; }
    public DateTime FechaPago { get; set; }
}
