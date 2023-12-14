using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using SistemaCreditos.Models;
namespace SistemaCreditos.Controllers.Reportes
{
    [Authorize]
    public class ReportesController : Controller
    {
        private Model db = new Model();
        public IActionResult Normalizacion()
        {
            return View();
        }
        public IActionResult EstadodeCuenta()
        {
            return View();
        }

        public IActionResult CuotasVencidas()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VerNormalizacion([FromBody] FormularioNormalizacion form )
        {
            try
            {
                string[] fechas = form.rangoFechas.Split(" - ");
                var fechaInicio = Util.convertirFecha(fechas[0]);
                var fechaFin = Util.convertirFecha(fechas[1]);

                //var cuotas = db.Cuotas.Where(e=>e.FechaCuota>=fechaInicio&&e.FechaCuota<=fechaFin).ToList();
                var cuotas = (from cl in db.Clientes
                              from p in db.Prestamos.Where(e => e.IdCliente == cl.IdCliente)
                              from c in db.Cuotas.Where(e => e.IdPrestamo == p.IdPrestamo && e.FechaCuota >= fechaInicio && e.FechaCuota <= fechaFin)
                              where form.zona!=null?cl.Zona== form.zona:true
                              where form.idDistrito != 0 ? cl.Distrito == form.idDistrito : true
                              select (c)).ToList();
                decimal contador = 0;
                foreach (var item in cuotas)
                {
                    if (DateTime.Now > item.FechaCuota && item.FechaPago == null)
                    {
                        contador++;
                    }
                }
                var resultado = 100 - (contador / cuotas.Count()) * 100;
                var normalizacion = (int)resultado;
                var NoNorma = 100 - (int)resultado;

                return Json(new { success = true, reporte = new { normalizacion,NoNorma } });
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public ActionResult CuotasVencidas([FromBody] FormularioNormalizacion form)
        {
            //Fechas de rango
            string[] fechas = form.rangoFechas.Split(" - ");
            var fechaInicio = Util.convertirFecha(fechas[0]);
            var fechaFin = Util.convertirFecha(fechas[1]);

            //Calculo de moras
            var CuotasVencidas = db.Cuotas.Where(a => a.FechaCuota < DateTime.Now && a.FechaPago == null && a.FechaCreacion >= fechaInicio && a.FechaCreacion <= fechaFin).ToList();
            var diasMora = 0;
            decimal Mora = 0;
            foreach (var item in CuotasVencidas)
            {
                diasMora = (int)DateTime.Now.Subtract((DateTime)item.FechaCuota).TotalDays;
                if (diasMora > 7) diasMora = 7;
                Mora = diasMora * 5;
                //Seteo de valores de mora
                item.Mora = Mora;
                item.DiasMora = diasMora;
            }
            db.SaveChanges();
            //---------------

            //Instanciamiento de Clientes
            var sql = (from c in db.Clientes
                       from p in db.Prestamos.Where(e => e.IdCliente == c.IdCliente && e.FechaCreacion >= fechaInicio && e.FechaCreacion <= fechaFin)
                       where form.zona != null ? c.Zona == form.zona : true
                       where form.idDistrito != 0 ? c.Distrito == form.idDistrito : true
                       select new
                       {
                           IdPrestamo=p.IdPrestamo,
                           Dni=c.Dni,
                           Cliente = c.ApellidosCliente + " " + c.NombreCliente,
                           NroCuotasVencidas = db.Cuotas.Where(a => a.FechaCuota.Value < DateTime.Now && a.FechaPago.Value == null && a.IdPrestamo == p.IdPrestamo).Count(),
                           CuotasVencidasLista = db.Cuotas.Where(a => a.FechaCuota.Value < DateTime.Now && a.FechaPago.Value == null && a.IdPrestamo == p.IdPrestamo).ToList()
                       }).Where(e=>e.NroCuotasVencidas>0).ToList();
            return Json(new { success = true, cuotasVencidas=sql });
        }
        public class FormularioNormalizacion
        {
            public int idDistrito { get; set; }
            public string zona { get; set; }
            public string rangoFechas { get; set; }
        }
        public class ListaVencidos
        {
            public int IdPrestamo { get; set; }
            public string Dni { get; set; }
            public string Cliente { get; set; }
            public int NroCuotasVencidas { get; set; }
            //public List<Cuota> CuotasVencidasLista { get; set; }
        }
    }
}
