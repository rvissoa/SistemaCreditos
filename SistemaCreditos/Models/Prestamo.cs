using System;
using System.Collections.Generic;

namespace SistemaCreditos.Models
{
    public partial class Prestamo
    {
        public Prestamo()
        {
            Cuota = new HashSet<Cuota>();
        }

        public int IdPrestamo { get; set; }
        public int? IdCliente { get; set; }
        public string? NumeroCredito { get; set; }
        public string? CodigoGestor { get; set; }
        public string? Autorizacion { get; set; }
        public decimal? FondoProvisional { get; set; }
        public string? DiaPago { get; set; }
        public int? NumeroCuotas { get; set; }
        public decimal? MontoCuota { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaTermino { get; set; }
        public int? DiasDeGracia { get; set; }
        public decimal? Capital { get; set; }
        public decimal? CapitalPendiente { get; set; }
        public decimal? Interes { get; set; }
        public string? Liquidacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioIngresa { get; set; }
        public string? UsuarioModifica { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual ICollection<Cuota> Cuota { get; set; }
    }
}
