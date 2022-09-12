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
    public class Persona : IBusinessObjectBase
    {
        [NotMapped]
        public object IdBO 
        {   get { return Id; }
            set { Id = (int)value; }
        }

        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "El Nombre debe contener hasta 255 caracteres")]
        [Required]
        public string Nombre { get; set; } = string.Empty;

        public string Genero { get; set; } = string.Empty;

        [Range(18, 100)]
        public int Edad { get; set; }

        [StringLength(20, ErrorMessage = "La identificacion debe ser de hasta 20 caracteres")]
        [Required]
        public string Identificacion { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;

        [StringLength(255, ErrorMessage = "El Telefono debe contener hasta 255 caracteres")]
        public string Telefono { get; set; } = string.Empty;
    }
}
