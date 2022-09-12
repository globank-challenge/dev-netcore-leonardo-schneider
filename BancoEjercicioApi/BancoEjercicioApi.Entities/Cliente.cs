using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BancoEjercicioApi.Entities
{
    public class Cliente : Persona
    {
        [StringLength(255, ErrorMessage = "El Password debe ser hasta 255 caracteres")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool Estado { get; set; }
    }
}
