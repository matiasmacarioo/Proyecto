using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Proyecto.Models
{
    public class Tarea
    {
        [Key]
        public int TareaID { get; set; }
        public string? Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public bool Realizada { get; set; }
        public enum Prioridad { Baja, Media, Alta }
        public string? UsuarioID { get; set; }
        public IdentityUser? Usuario { get; set; }
    }
}