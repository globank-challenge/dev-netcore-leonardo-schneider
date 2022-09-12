using BancoEjercicioApi.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoEjercicioApi.Entities
{
    [Keyless]
    public class ReporteMovimiento : IBusinessObjectBase
    {
        [NotMapped]
        public object IdBO { get; set; }

        public DateTime Fecha { get; set; }
        public string Nombre { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public decimal Movimiento { get; set; }
        public decimal SaldoDisponible { get; set; }
    }
}
