using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto.Models
{
    public class Profesor
    {
        [Key]
        public int ProfesorID { get; set; }
        public string? Nombre { get; set; }
        public int? Documento { get; set; }
        public string? Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }

        [ForeignKey("Carrera")]
        public int CarreraID { get; set; }
        public Carrera? Carrera { get; set; }
    }
}