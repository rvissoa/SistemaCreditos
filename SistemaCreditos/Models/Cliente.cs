using System;
using System.Collections.Generic;

namespace SistemaCreditos.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Prestamos = new HashSet<Prestamo>();
        }

        public int IdCliente { get; set; }
        public string? CodigoCliente { get; set; }
        public string? CodigoGestor { get; set; }
        public string? NombreCliente { get; set; }
        public string? ApellidosCliente { get; set; }
        public string? Direccion { get; set; }
        public string? UbicacionReferencia { get; set; }
        public int? Distrito { get; set; }
        public string? Zona { get; set; }
        public string? Dni { get; set; }
        public string? Celular { get; set; }
        public string? TrabajoOcupacion { get; set; }
        public string? Hijos { get; set; }
        public int? TieneConyuge { get; set; }
        public string? DniConyuge { get; set; }
        public string? NombreConyuge { get; set; }
        public string? ApellidosConyuge { get; set; }
        public string? DireccionConyuge { get; set; }
        public string? UbicacionReferenciaConyuge { get; set; }
        public string? TrabajoConyuge { get; set; }
        public string? TelefonoConyuge { get; set; }
        public int? TieneAval { get; set; }
        public string? NombreAval { get; set; }
        public string? ApellidosAval { get; set; }
        public string? DniAval { get; set; }
        public string? DireccionAval { get; set; }
        public string? UbicacionReferenciaAval { get; set; }
        public string? TrabajoAval { get; set; }
        public string? TelefonoAval { get; set; }
        public string? ParentescoAval { get; set; }
        public string? Observaaciones { get; set; }
        public DateTime? FechaIngresa { get; set; }
        public DateTime? FechaModifica { get; set; }
        public string? UsuarioIngresa { get; set; }
        public string? UsuarioModifica { get; set; }

        public virtual ICollection<Prestamo> Prestamos { get; set; }
    }
}
