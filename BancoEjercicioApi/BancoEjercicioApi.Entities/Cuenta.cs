using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BancoEjercicioApi.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace BancoEjercicioApi.Entities
{
    public class Cuenta : IBusinessObjectBase
    {
        [NotMapped]
        public object IdBO
        {
            get { return Id; }
            set { Id = (int)value; }
        }

        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "El Numero de la cuenta debe ser de hasta 50 caracteres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El Numero no puede estar vacío")]
        public string Numero { get; set; }

        [StringLength(50, ErrorMessage = "El Tipo de la cuenta debe ser de hasta 50 caracteres")]
        public string Tipo { get; set; }

        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public decimal Saldo { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
