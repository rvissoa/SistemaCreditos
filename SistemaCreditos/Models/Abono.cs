using System;
using System.Collections.Generic;

namespace SistemaCreditos.Models
{
    public partial class Abono
    {
        public int IdAbono { get; set; }
        public int? IdCuota { get; set; }
        public DateTime? FechaAbono { get; set; }
        public decimal? MontoAbono { get; set; }
        public byte[]? FotoAbono { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioIngresa { get; set; }
        public string? UsuarioModifica { get; set; }
        public string? TipoArchivo { get; set; }
        public int? Banco { get; set; }
        public int? TipoAbono { get; set; }
        public string? Codigo { get; set; }
        public decimal? MontoMora { get; set; }
        public string? CodigoGestor { get; set; }
        public virtual Cuota? IdCuotaNavigation { get; set; }
    }
}
