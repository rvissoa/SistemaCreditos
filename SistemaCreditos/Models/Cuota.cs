using System;
using System.Collections.Generic;

namespace SistemaCreditos.Models
{
    public partial class Cuota
    {
        public Cuota()
        {
            Abonos = new HashSet<Abono>();
        }

        public int IdCuota { get; set; }
        public int? IdPrestamo { get; set; }
        public DateTime? FechaCuota { get; set; }
        public DateTime? FechaPago { get; set; }
        public decimal? MontoCuota { get; set; }
        public int? CantidadAbonos { get; set; }
        public decimal? Mora { get; set; }
        public int? DiasMora { get; set; }
        public string? Observaciones { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioIngresa { get; set; }
        public string? UsuarioModifica { get; set; }
        public DateTime? FechaPagoMora { get; set; }
        public virtual Prestamo? IdPrestamoNavigation { get; set; }
        public virtual ICollection<Abono> Abonos { get; set; }
    }
}
