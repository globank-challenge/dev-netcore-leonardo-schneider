using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoEjercicioApi.Entities.DTOs
{
    public class CuentaDTO
    {
        public int Id { get; set; }
        public string Numero { get; set; } = String.Empty;
        public string Tipo { get; set; } = String.Empty;
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public int ClienteId { get; set; }
    }
}
