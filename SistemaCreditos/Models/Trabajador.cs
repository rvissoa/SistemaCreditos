using System;
using System.Collections.Generic;

namespace SistemaCreditos.Models
{
    public partial class Trabajador
    {
        public Trabajador()
        {
            Asistencia = new HashSet<Asistencia>();
        }

        public int IdTrabajador { get; set; }
        public string? CodigoTrabajador { get; set; }
        public string? Apellidos { get; set; }
        public string? Nombres { get; set; }
        public string? Celular { get; set; }
        public string? Usuario { get; set; }
        public string? Pass { get; set; }
        public int? TipoTrabajador { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioIngresa { get; set; }
        public string? UsuarioModifica { get; set; }

        public virtual ICollection<Asistencia> Asistencia { get; set; }
    }
}
