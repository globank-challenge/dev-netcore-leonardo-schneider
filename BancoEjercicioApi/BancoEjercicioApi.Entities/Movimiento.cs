using BancoEjercicioApi.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoEjercicioApi.Entities
{
    public class Movimiento : IBusinessObjectBase
    {
        [NotMapped]
        public object IdBO
        {
            get { return Id; }
            set { Id = (int)value; }
        }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal Valor { get; set; }
        public decimal SaldoDisponible { get; set; }

        public Cuenta Cuenta { get; set; }
        public int CuentaId { get; set; }

    }
}
