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

        [HttpPost]
        public ActionResult VerNormalizacion([FromBody] FormularioNormalizacion form )
        {
            try
            {
                string[] fechas = form.rangoFechas.Split(" - ");
                var fechaInicio = Convert.ToDateTime(fechas[0]);
                var fechaFin = Convert.ToDateTime(fechas[1]);

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

        public class FormularioNormalizacion
        {
            public int idDistrito { get; set; }
            public string zona { get; set; }
            public string rangoFechas { get; set; }
        }
    }
}
