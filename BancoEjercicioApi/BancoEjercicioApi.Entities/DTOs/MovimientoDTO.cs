using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoEjercicioApi.Entities.DTOs
{
    public class MovimientoDTO
    {
        public string Fecha { get; set; } // Formato DD/MM/YYYY
        public int CuentaId { get; set; }
        public decimal Valor { get; set; } // Si es < 0 es un Débito, sino es Crédito
    }
}
