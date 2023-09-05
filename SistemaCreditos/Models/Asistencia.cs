using System;
using System.Collections.Generic;

namespace SistemaCreditos.Models
{
    public partial class Asistencia
    {
        public int IdAsistencia { get; set; }
        public int? IdTrabajador { get; set; }
        public DateTime? FechaAsistencia { get; set; }
        public string? HoraEntrada { get; set; }
        public string? HoraSalida { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioIngresa { get; set; }
        public string? UsuarioModifica { get; set; }
        public string? HoraAlmuerzo { get; set; }
        public string? HoraAlmuerzoRegreso { get; set; }

        public virtual Trabajador? IdTrabajadorNavigation { get; set; }
    }
}
