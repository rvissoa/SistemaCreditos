using System;
using System.Collections.Generic;

namespace SistemaCreditos.Models
{
    public partial class Distrito
    {
        public int IdDistrito { get; set; }
        public int? CodigoDepartamento { get; set; }
        public int? CodigoProvincia { get; set; }
        public int? CodigoDistrito { get; set; }
        public string? NombreDistrito { get; set; }
        public decimal? Latitud { get; set; }
        public decimal? Longitud { get; set; }
    }
}
