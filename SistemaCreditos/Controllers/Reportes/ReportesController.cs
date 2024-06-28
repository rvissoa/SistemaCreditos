using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis.CSharp;
using SistemaCreditos.Models;
using System.Data;

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
        public IActionResult ListaEstadosdeCuenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ExportarEstadosExcel([FromBody] FormularioNormalizacion form)
        {
            string[] fechas = form.rangoFechas.Split(" - ");
            var fechaInicio = Util.convertirFecha(fechas[0]);
            var fechaFin = Util.convertirFecha(fechas[1]);
            //---------------------------------------------
            var ListaPrestamos = (from cl in db.Clientes
                                  from p in db.Prestamos.Where(e => e.IdCliente == cl.IdCliente && e.FechaTermino == null && e.FechaEntrega >= fechaInicio && e.FechaEntrega <= fechaFin && (form.autorizador != null ? e.Autorizacion == form.autorizador : true))

                                  where form.zona != null ? cl.Zona == form.zona : true
                                  where form.idDistrito != 0 ? cl.Distrito == form.idDistrito : true
                                  where form.gestor != null ? cl.CodigoGestor == form.gestor : true
                                  select p
                                  ).Take(50).ToList();

            var prestamo = GetPrestamo(ListaPrestamos);

            using (XLWorkbook wb = new XLWorkbook())
            {
                var shet1 = wb.AddWorksheet(prestamo, "Estado Cuenta");

                using (MemoryStream ms=new MemoryStream())
                {
                    wb.SaveAs(ms);
                    //byte[] bindata = System.Text.Encoding.ASCII.GetBytes(ms.ToString());
                    return File(ms.ToArray(), "application/ms-excel", "Sample.xlsx");
                }
            }
        }

        private DataTable GetPrestamo(List<Prestamo> prestamos)
        {
            DataTable dt = new DataTable();
            dt.TableName = "Prestamo Cliente";
            //--numero de columnas
            dt.Columns.Add("", typeof(string));
            dt.Columns.Add("", typeof(string));
            dt.Columns.Add("", typeof(string));
            dt.Columns.Add("", typeof(string));
            dt.Columns.Add("", typeof(string));
            dt.Columns.Add("", typeof(string));
            dt.Columns.Add("", typeof(string));
            dt.Columns.Add("", typeof(string));
            dt.Columns.Add("", typeof(string));
            dt.Columns.Add("", typeof(string));
            dt.Columns.Add("", typeof(string));

            //Recorrer prestamos
            foreach (var p in prestamos)
            {
                dt.Rows.Add();
                //DataRow clienteRow = dt.NewRow();
                
                dt.Rows.Add("DATOS DEL CLIENTE");
                dt.Rows.Add();
                //cliente
                var cliente = db.Clientes.Find(p.IdCliente);
                dt.Rows.Add("Número de créditos: ", "1");
                dt.Rows.Add("Nombre: ",cliente.NombreCliente+ " "+cliente.ApellidosCliente);
                dt.Rows.Add("Dirección: ",cliente.Direccion);
                dt.Rows.Add("Celular: ",cliente.Celular,"DNI: ", cliente.Dni);
                dt.Rows.Add("Ubicación: ", cliente.UbicacionReferencia);
                dt.Rows.Add("Trabajo: ", cliente.TrabajoOcupacion);
                dt.Rows.Add();
                dt.Rows.Add("ESTADO DE CUENTA");
                dt.Rows.Add();
                dt.Rows.Add("Fecha entrega: ", p.FechaEntrega,"Modalidad","","Capital: ", p.Capital,"Fondo Provisional", p.FondoProvisional);
                dt.Rows.Add("Capital Inicial: ", p.CapitalPendiente, "Número de créditos", "1", "Días de Pago: ",p.DiaPago);
                dt.Rows.Add();
                dt.Rows.Add("IT", "FECHA", "ABONO", "CAPITAL PENDIENTE", "ABONO MORA", "FECHA PAGO", "NRO OPERACIÓN", "BANCO", "MORAS PENDIENTES", "DÍAS MORA", "OBSERVACIÓN");

                //dt.Columns.Add("IT", typeof(int));
                //dt.Columns.Add("FECHA", typeof(string));
                //dt.Columns.Add("ABONO", typeof(decimal));
                //dt.Columns.Add("CAPITAL PENDIENTE", typeof(decimal));
                //dt.Columns.Add("ABONO MORA", typeof(decimal));
                //dt.Columns.Add("FECHA PAGO", typeof(string));
                //dt.Columns.Add("NRO OPERACIÓN", typeof(string));
                //dt.Columns.Add("BANCO", typeof(string));
                //dt.Columns.Add("MORAS PENDIENTES", typeof(int));
                //dt.Columns.Add("DÍAS MORA", typeof(int));
                //dt.Columns.Add("OBSERVACIÓN", typeof(string));

                //------
                var prestamo = db.Prestamos.Find(p.IdPrestamo);
                //var cuotas = db.Cuotas.Where(e => e.IdPrestamo == idPrestamo).ToList();
                var modelo = (from c in db.Cuotas.Where(e => e.IdPrestamo == p.IdPrestamo)
                              from a in db.Abonos.Where(e => e.IdCuota == c.IdCuota).DefaultIfEmpty()
                              from b in db.Bancos.Where(e => e.IdBanco == a.Banco).DefaultIfEmpty()

                              group new { c, a, b } by new { c.DiasMora, c.FechaPago, c.IdCuota, c.FechaCuota, c.MontoCuota, c.Mora, c.Observaciones, } into g
                              select new
                              {
                                  g.Key.FechaPago,
                                  g.Key.IdCuota,
                                  g.Key.FechaCuota,
                                  g.Key.MontoCuota,
                                  g.Key.Mora,
                                  DiasMora = g.Key.DiasMora > 0 ? g.Key.DiasMora : 0,
                                  g.Key.Observaciones,
                                  Abono = g.Sum(e => e.a.MontoAbono),
                                  AbonoMora = g.Sum(e => e.a.MontoMora),
                                  NroOperacion = string.Join(",", g.Select(i => i.a.Codigo)),
                                  Banco = string.Join(",", g.Select(i => i.b.RazonSocial)),
                                  CapitalPendiente = prestamo.CapitalPendiente

                              }).OrderBy(a => a.FechaCuota).ToList();
                var abonoTotal = modelo.Sum(e => e.Abono);
                var abonoMoraTotal = modelo.Sum(e => e.AbonoMora);
                var moraTotal = modelo.Sum(e => e.Mora);
                var diasMoraTotal = modelo.Sum(e => e.DiasMora);
                //------
                if (modelo.Count > 0)
                {
                    var it = 0;
                    foreach (var item in modelo)
                    {
                        it++;
                        dt.Rows.Add(it, item.FechaCuota, item.Abono, item.CapitalPendiente, item.AbonoMora, item.FechaPago, item.NroOperacion, item.Banco, item.Mora, item.DiasMora, item.Observaciones);

                    }
                }
            }



            //dt.Columns.Add("IT", typeof(int));
            //dt.Columns.Add("FECHA", typeof(string));
            //dt.Columns.Add("ABONO", typeof(decimal));
            //dt.Columns.Add("CAPITAL PENDIENTE", typeof(decimal));
            //dt.Columns.Add("ABONO MORA", typeof(decimal));
            //dt.Columns.Add("FECHA PAGO", typeof(string));
            //dt.Columns.Add("NRO OPERACIÓN", typeof(string));
            //dt.Columns.Add("BANCO", typeof(string));
            //dt.Columns.Add("MORAS PENDIENTES", typeof(int));
            //dt.Columns.Add("DÍAS MORA", typeof(int));
            //dt.Columns.Add("OBSERVACIÓN", typeof(string));

            //------
            //var prestamo = db.Prestamos.Find(idPrestamo);
            ////var cuotas = db.Cuotas.Where(e => e.IdPrestamo == idPrestamo).ToList();
            //var modelo = (from c in db.Cuotas.Where(e => e.IdPrestamo == idPrestamo)
            //              from a in db.Abonos.Where(e => e.IdCuota == c.IdCuota).DefaultIfEmpty()
            //              from b in db.Bancos.Where(e => e.IdBanco == a.Banco).DefaultIfEmpty()

            //              group new { c, a, b } by new { c.DiasMora, c.FechaPago, c.IdCuota, c.FechaCuota, c.MontoCuota, c.Mora, c.Observaciones, } into g
            //              select new
            //              {
            //                  g.Key.FechaPago,
            //                  g.Key.IdCuota,
            //                  g.Key.FechaCuota,
            //                  g.Key.MontoCuota,
            //                  g.Key.Mora,
            //                  DiasMora = g.Key.DiasMora > 0 ? g.Key.DiasMora : 0,
            //                  g.Key.Observaciones,
            //                  Abono = g.Sum(e => e.a.MontoAbono),
            //                  AbonoMora = g.Sum(e => e.a.MontoMora),
            //                  NroOperacion = string.Join(",", g.Select(i => i.a.Codigo)),
            //                  Banco = string.Join(",", g.Select(i => i.b.RazonSocial)),
            //                  CapitalPendiente = prestamo.CapitalPendiente

            //              }).OrderBy(a => a.FechaCuota).ToList();
            //var abonoTotal = modelo.Sum(e => e.Abono);
            //var abonoMoraTotal = modelo.Sum(e => e.AbonoMora);
            //var moraTotal = modelo.Sum(e => e.Mora);
            //var diasMoraTotal = modelo.Sum(e => e.DiasMora);
            ////------
            //if (modelo.Count > 0)
            //{
            //    var it = 0;
            //    foreach (var item in modelo)
            //    { 
            //        it++;
            //        dt.Rows.Add(it,item.FechaCuota,item.Abono,item.CapitalPendiente,item.AbonoMora,item.FechaPago,item.NroOperacion,item.Banco,item.Mora,item.DiasMora,item.Observaciones);

            //    }
            //}
            return dt;
        }
        [HttpPost]
        public ActionResult VerNormalizacion([FromBody] FormularioNormalizacion form )
        {
            try
            {
                string[] fechas = form.rangoFechas.Split(" - ");
                var fechaInicio = Util.convertirFecha(fechas[0]);
                var fechaFin = Util.convertirFecha(fechas[1]);




                #region Normallizacion-antiguo
                //var cuotas = db.Cuotas.Where(e=>e.FechaCuota>=fechaInicio&&e.FechaCuota<=fechaFin).ToList();
                //var cuotas = (from cl in db.Clientes
                //              from p in db.Prestamos.Where(e => e.IdCliente == cl.IdCliente)
                //              from c in db.Cuotas.Where(e => e.IdPrestamo == p.IdPrestamo && e.FechaCuota >= fechaInicio && e.FechaCuota <= fechaFin)
                //              where form.zona != null ? cl.Zona == form.zona : true
                //              where form.idDistrito != 0 ? cl.Distrito == form.idDistrito : true
                //              where form.gestor != null ? cl.CodigoGestor == form.gestor : true
                //              select (c)).ToList();
                //decimal contador = 0;
                //var dif = (int)DateTime.Now.Subtract((DateTime)item.FechaCuota).TotalDays;
                //foreach (var item in cuotas)
                //{
                //    if (DateTime.Now > item.FechaCuota && item.FechaPago == null)
                //    {
                //        contador++;
                //    }
                //}
                //var resultado = 100 - (contador / cuotas.Count()) * 100;
                //var normalizacion = (int)resultado;
                //var NoNorma = 100 - (int)resultado;

                //return Json(new { success = true, reporte = new { normalizacion,NoNorma } });
                #endregion
                //Normalizados
                var prestamos1 = (from cl in db.Clientes
                                  from p in db.Prestamos.Where(e => e.IdCliente == cl.IdCliente && e.FechaTermino==null && e.FechaEntrega >= fechaInicio && e.FechaEntrega <= fechaFin &&(form.autorizador != null ? e.Autorizacion == form.autorizador : true))
                                  from c in db.Cuotas.Where(e => e.IdPrestamo == p.IdPrestamo)
                                  where form.zona != null ? cl.Zona == form.zona : true
                                  where form.idDistrito != 0 ? cl.Distrito == form.idDistrito : true
                                  where form.gestor != null ? cl.CodigoGestor == form.gestor : true
                                  //where form.autorizador != null ? p.Autorizacion == form.autorizador : true
                                  where c.FechaPago!=null ? false : true
                                  group new {p,c } by new {p.IdPrestamo} into g
                              select new
                              {
                                sumaMoras=g.Sum(e=>e.c.DiasMora),
                                IdPrestamo=g.Key.IdPrestamo
                              }).ToList();

                decimal contadorN = 0;
                decimal contador2 = 0;
                decimal contador4 = 0;
                decimal contador8 = 0;
                decimal contador12 = 0;

                foreach (var item in prestamos1)
                {
                    
                    if (item.sumaMoras < 8)
                    {
                        contadorN++;
                    }
                    else if (item.sumaMoras >= 8 && item.sumaMoras <= 21)
                    {
                        contador2++;
                    }
                    else if (item.sumaMoras > 21 && item.sumaMoras <= 49)
                    {
                        contador4++;
                    }
                    else if (item.sumaMoras > 49 && item.sumaMoras <= 84)
                    {
                        contador8++;
                    }
                    else if (item.sumaMoras > 84)
                    {
                        contador12++;
                    }

                }

                return Json(new { success = true, reporte = new { contadorN, contador2, contador4, contador8, contador12 } });
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
        [HttpPost]
        public ActionResult ListaEstadosDeCuenta([FromBody] FormularioNormalizacion form)
        {
            return Json(new { success = true});
        }
        public class FormularioNormalizacion
        {
            public int idDistrito { get; set; }
            public string zona { get; set; }
            public string rangoFechas { get; set; }
            public string gestor { get; set; }
            public string autorizador { get; set; }
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
