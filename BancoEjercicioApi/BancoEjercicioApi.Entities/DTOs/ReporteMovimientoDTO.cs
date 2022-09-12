using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoEjercicioApi.Entities.DTOs
{
    public class ReporteMovimientoDTO
    {
        public string Fecha { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public decimal Movimiento { get; set; }
        public decimal SaldoDisponible { get; set; }
    }
}
