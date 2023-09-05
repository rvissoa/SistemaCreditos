using System;
using System.Collections.Generic;

namespace SistemaCreditos.Models
{
    public partial class Banco
    {
        public int IdBanco { get; set; }
        public string? RazonSocial { get; set; }
        public string? Abreviacion { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
    }
}
