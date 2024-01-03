using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using SistemaCreditos.Models;
using System.Net;
using static SistemaCreditos.Controllers.Clientes.ClientesController;
using static System.Net.Mime.MediaTypeNames;

namespace SistemaCreditos.Controllers.Clientes
{
    [Authorize]
    public class ClientesController : Controller
    {
        private Model db = new Model();

        #region Modulo Clientes
        public IActionResult ListaClientes()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ListaClientes(string valor)
        {
            var model = (from c in db.Clientes
                        select new { 
                            id=c.IdCliente,
                            dni=c.Dni,
                            cliente=c.NombreCliente+" "+c.ApellidosCliente,
                            direccion=c.Direccion+" "+c.Distrito,
                            celular=c.Celular,
                            codigoGestor = c.CodigoGestor,
                            zona=c.Zona,
                            trabajo=c.TrabajoOcupacion,
                            ubicacionReferencia=c.UbicacionReferencia
                        }).ToList() ;
            return new JsonResult(new { success = true, model });
        }

        public IActionResult NuevoCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Distritos()
        {
            string[] lista = { "514", "1829", "543", "504", "520", "506", "511", "531", "1834" };
            var model = db.Distritos.Where(e => lista.Contains(e.IdDistrito.ToString())).ToList();
            return Json(new { success = true, distritos = model });
        }

        [HttpPost]
        public ActionResult Gestores()
        {
            var sql = db.Trabajadors.Where(e=>e.TipoTrabajador==3).ToList();
            return Json(new { success = true, gestores = sql });
        }
        [HttpPost]
        public ActionResult NuevoCliente([FromBody] cliente cliente)
        {
            var model = new Cliente
            {
                CodigoGestor = Convert.ToString(cliente.gestor),
                NombreCliente = cliente.nombres,
                ApellidosCliente = cliente.apellidos,
                Direccion = cliente.direccion,
                UbicacionReferencia = cliente.ubicacion,
                Distrito = cliente.idDistrito,
                Zona = cliente.zona,
                Dni = cliente.dni,
                Celular = cliente.celular,
                TrabajoOcupacion = cliente.trabajo,
                Hijos = cliente.nombreHijos,
                TieneConyuge = cliente.tieneConyuge ? 1 : 0,
                DniConyuge = cliente.dniConyuge,
                NombreConyuge = cliente.nombresConyuge,
                ApellidosConyuge = cliente.apellidosConyuges,
                DireccionConyuge = cliente.direccionConyuge,
                UbicacionReferenciaConyuge = cliente.ubicacionConyuge,
                TrabajoConyuge = cliente.trabajoConyuge,
                TelefonoConyuge = cliente.celularConyuge,
                TieneAval = cliente.tieneAval ? 1 : 0,
                DniAval = cliente.dniAval,
                NombreAval = cliente.nombresAval,
                ApellidosAval = cliente.apellidosAval,
                DireccionAval = cliente.direccionAval,
                UbicacionReferenciaAval = cliente.ubicacionAval,
                TrabajoAval = cliente.trabajoAval,
                TelefonoAval = cliente.celularAval,
                ParentescoAval = cliente.parentezcoAval,
                Observaaciones = cliente.observaciones,

                FechaIngresa = DateTime.Now
            };

            var usuario = @User?.Claims.Where(e => e.Type == "preferred_username").Select(e => e.Value).FirstOrDefault();
            string[] user = usuario.Split("@");
            model.UsuarioIngresa = user[0];

            db.Clientes.Add(model);
            db.SaveChanges();
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult Edicion(int id)
        {
            var model = (from c in db.Clientes.Where(e => e.IdCliente == id)
                         select new cliente
                         {
                             //idCliente = c.IdCliente,
                             //codigoCliente = c.CodigoCliente,
                             gestor = c.CodigoGestor,
                             nombres = c.NombreCliente,
                             apellidos = c.ApellidosCliente,
                             direccion = c.Direccion,
                             ubicacion = c.UbicacionReferencia,
                             idDistrito = c.Distrito,
                             zona = c.Zona,
                             dni = c.Dni,
                             celular = c.Celular,
                             trabajo = c.TrabajoOcupacion,
                             nombreHijos = c.Hijos,

                             tieneConyuge = c.TieneConyuge == 1 ? true : false,
                             dniConyuge = c.DniConyuge,
                             nombresConyuge = c.NombreConyuge,
                             apellidosConyuges = c.ApellidosConyuge,
                             direccionConyuge = c.DireccionConyuge,
                             ubicacionConyuge = c.UbicacionReferenciaConyuge,
                             trabajoConyuge = c.TrabajoConyuge,
                             celularConyuge = c.TelefonoConyuge,

                             tieneAval = c.TieneAval == 1 ? true : false,
                             dniAval = c.DniAval,
                             nombresAval = c.NombreAval,
                             apellidosAval = c.ApellidosAval,
                             direccionAval = c.DireccionAval,
                             ubicacionAval = c.UbicacionReferencia,
                             trabajoAval = c.TrabajoAval,
                             celularAval = c.TelefonoAval,
                             parentezcoAval = c.ParentescoAval,
                             observaciones = c.Observaaciones
                         }).FirstOrDefault();

            return Json(new { success = true, clientes = model });

        }

        public IActionResult EditarCliente(int id)
        {
            ViewBag.idCliente = id;
            return View();
        }

        [HttpPost]
        public ActionResult EditarCliente([FromBody] cliente cliente)
        {
            var modelAnterior = db.Clientes.Find(cliente.idCliente);

            modelAnterior.IdCliente = Convert.ToInt32(cliente.idCliente);
            modelAnterior.CodigoGestor = Convert.ToString(cliente.gestor);
            modelAnterior.NombreCliente = cliente.nombres;
            modelAnterior.ApellidosCliente = cliente.apellidos;
            modelAnterior.Direccion = cliente.direccion;
            modelAnterior.UbicacionReferencia = cliente.ubicacion;
            modelAnterior.Distrito = cliente.idDistrito;
            modelAnterior.Zona = cliente.zona;
            modelAnterior.Dni = cliente.dni;
            modelAnterior.Celular = cliente.celular;
            modelAnterior.TrabajoOcupacion = cliente.trabajo;
            modelAnterior.Hijos = cliente.nombreHijos;
            modelAnterior.TieneConyuge = cliente.tieneConyuge ? 1 : 0;
            modelAnterior.DniConyuge = cliente.dniConyuge;
            modelAnterior.NombreConyuge = cliente.nombresConyuge;
            modelAnterior.ApellidosConyuge = cliente.apellidosConyuges;
            modelAnterior.DireccionConyuge = cliente.direccionConyuge;
            modelAnterior.UbicacionReferenciaConyuge = cliente.ubicacionConyuge;
            modelAnterior.TrabajoConyuge = cliente.trabajoConyuge;
            modelAnterior.TelefonoConyuge = cliente.celularConyuge;
            modelAnterior.TieneAval = cliente.tieneAval ? 1 : 0;
            modelAnterior.DniAval = cliente.dniAval;
            modelAnterior.NombreAval = cliente.nombresAval;
            modelAnterior.ApellidosAval = cliente.apellidosAval;
            modelAnterior.DireccionAval = cliente.direccionAval;
            modelAnterior.UbicacionReferenciaAval = cliente.ubicacionAval;
            modelAnterior.TrabajoAval = cliente.trabajoAval;
            modelAnterior.TelefonoAval = cliente.celularAval;
            modelAnterior.ParentescoAval = cliente.parentezcoAval;
            modelAnterior.Observaaciones = cliente.observaciones;
            modelAnterior.FechaModifica = DateTime.Now;

            var usuario = @User?.Claims.Where(e => e.Type == "preferred_username").Select(e => e.Value).FirstOrDefault();
            string[] user = usuario.Split("@");
            modelAnterior.UsuarioModifica = user[0];

            db.SaveChanges();
            return Json(new { success = true });
        }

        public class cliente
        {
            public int? idCliente { get; set; }
            public int? idDistrito { get; set; }
            public string zona { get; set; }
            public string gestor { get; set; }
            public string nombres { get; set; }
            public string apellidos { get; set; }
            public string dni { get; set; }
            public string direccion { get; set; }
            public string celular { get; set; }
            public string ubicacion { get; set; }
            public string trabajo { get; set; }
            public string nombreHijos { get; set; }
            public bool tieneConyuge { get; set; }
            public string nombresConyuge { get; set; }
            public string apellidosConyuges { get; set; }
            public string dniConyuge { get; set; }
            public string celularConyuge { get; set; }
            public string  ubicacionConyuge { get; set; }
            public string  direccionConyuge { get; set; }
            public string  trabajoConyuge { get; set; }
            public bool tieneAval { get; set; }
            public string nombresAval { get; set; }
            public string apellidosAval { get; set; }
            public string dniAval { get; set; }
            public string celularAval { get; set; }
            public string ubicacionAval { get; set; }
            public string direccionAval { get; set; }
            public string trabajoAval { get; set; }
            public string parentezcoAval { get; set; }
            public string observaciones { get; set; }
        }
        #endregion

        #region Cliente prestamo
        public ActionResult Prestamos(int id)
        {
            try
            {
                var clientes = db.Clientes.Find(id);
                if (clientes == null)
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
                return View("Prestamos", clientes);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public ActionResult ListaPrestamos(int idCliente)
        {
            var sql = db.Prestamos.Where(e => e.IdCliente ==idCliente).ToList();
            return Json(new { success = true, prestamos = sql });
        }

        public ActionResult NuevoPrestamo(int idCliente)
        {
            try
            {
                var clientes = db.Clientes.Find(idCliente);
                if (clientes == null)
                {
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
                return View("NuevoPrestamo", clientes);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult Autorizadores()
        {
            var sql = db.Trabajadors.Where(e => e.TipoTrabajador== 4).ToList();
            return Json(new { success=true,autorizadores = sql });
        }

        [HttpPost]
        public ActionResult NuevoPrestamo([FromBody] PrestamoCliente prestamo)
        {
            try
            {
                var modelPrestamo = new Prestamo
                {
                    CodigoGestor = prestamo.codigoGestor,
                    Autorizacion = prestamo.autorizacion,
                    IdCliente = prestamo.IdCliente,
                    FondoProvisional= prestamo.fondo,
                    FechaEntrega = prestamo.fechaEntrega,
                    Capital = prestamo.capital,
                    DiaPago = prestamo.diaPago,
                    NumeroCuotas= prestamo.numeroCuotas,
                    MontoCuota = prestamo.montoCuotas,
                    CapitalPendiente= prestamo.capitalPendiente,
                    DiasDeGracia = prestamo.diasgracia,

                    FechaCreacion = DateTime.Now

                };
                var usuario = @User?.Claims.Where(e => e.Type == "preferred_username").Select(e => e.Value).FirstOrDefault();
                string[] user = usuario.Split("@");
                modelPrestamo.UsuarioIngresa = user[0];

                db.Prestamos.Add(modelPrestamo);
                db.SaveChanges();
                var fcuota = prestamo.fecha1Cuota;
                for (int i = 0; i < prestamo.numeroCuotas; i++)
                {
                    if (i != 0)
                    {
                        fcuota = fcuota.AddDays(7);
                    }
                    var modelCuota = new Cuota
                    {
                        IdPrestamo = modelPrestamo.IdPrestamo,
                        FechaCuota = fcuota,
                        FechaCreacion = DateTime.Now,
                        UsuarioIngresa = user[0],
                        MontoCuota = prestamo.montoCuotas
                    };
                    db.Cuotas.Add(modelCuota);
                }
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost]
        public ActionResult Cuotas(int idPrestamo)
        {
            try
            {
                //Calculo de moras
                ActualizarMoras(idPrestamo);
                //---------------
                //var model = db.Cuotas.Where(e => e.IdPrestamo == idPrestamo).ToList();
                var modelo = (from c in db.Cuotas.Where(e => e.IdPrestamo == idPrestamo)
                              from a in db.Abonos.Where(e => e.IdCuota == c.IdCuota).DefaultIfEmpty()
                              group new { c,a } by new { c.DiasMora, c.FechaPago, c.IdCuota, c.FechaCuota, c.MontoCuota, c.Mora, c.Observaciones, } into g
                              select new
                              {
                                  g.Key.FechaPago,
                                  g.Key.IdCuota,
                                  g.Key.FechaCuota,
                                  g.Key.MontoCuota,
                                  g.Key.Mora,
                                  g.Key.DiasMora,
                                  g.Key.Observaciones,
                                  Abono=g.Sum(e=>e.a.MontoAbono),
                                  AbonoMora = g.Sum(e => e.a.MontoMora)

                              }).ToList();

                return Json(new { success = true, cuotas=modelo });
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult Bancos()
        {
            try
            {
                return Json(new {success=true, bancos = db.Bancos.OrderByDescending(e=>e.IdBanco).ToList() });
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        public ActionResult NuevoAbono([FromBody] formulario abono)
        {
            try
            {
                var model = new Abono
                {
                    IdCuota = abono.idCuota,
                    MontoAbono = abono.monto,
                    FechaAbono = abono.fechaAbono,
                    FechaCreacion = DateTime.Now,
                    Banco = abono.banco,
                    TipoArchivo = abono.tipo,
                    Codigo = abono.numeroVoucher,
                    TipoAbono = abono.tipoAbono,
                    MontoMora=abono.mora,
                    CodigoGestor=abono.gestor
                };
                var usuario = @User?.Claims.Where(e => e.Type == "preferred_username").Select(e => e.Value).FirstOrDefault();
                string[] user = usuario.Split("@");
                model.UsuarioIngresa = user[0];
                if (abono.voucher != null)
                {
                    //model.FotoAbono = Convert.FromBase64String(abono.voucher);
                }
                db.Abonos.Add(model);
                db.SaveChanges();

                //Verificar dia de pago CUOTA
                var cuota = db.Cuotas.Find(abono.idCuota);
                var abonos = db.Abonos.Where(e => e.IdCuota == cuota.IdCuota);
                var presta = db.Prestamos.Find(cuota.IdPrestamo);
                decimal? suma = 0;
                foreach (var item in abonos.ToList())
                {
                    suma += item.MontoAbono;
                }
                if (suma >= cuota.MontoCuota)
                {
                    cuota.FechaPago = abono.fechaAbono;
                    //Verificar fecha termino del prestamo
                    if (db.Cuotas.Where(e=>e.IdPrestamo==presta.IdPrestamo).Max(e=>e.IdCuota) == abono.idCuota)
                    {
                        presta.FechaTermino = abono.fechaAbono;
                        presta.Liquidacion = "NORMAL";
                    }
                }
                cuota.CantidadAbonos = abonos.ToList().Count();

                //Verificar pagos de mora
                var result=ActualizarMoras(presta.IdPrestamo);
                decimal? sumaMora = 0;
                foreach (var item in abonos.ToList())
                {
                    sumaMora += item.MontoMora;
                }
                if (sumaMora >= cuota.Mora && sumaMora!=0)
                {
                    cuota.FechaPagoMora = abono.fechaAbono;
                }
                //-----------------------
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        public bool ActualizarMoras(int idPrestamo)
        {
            try
            {
                //Condición de 7 días para regularizar moras
                var prestamo=db.Prestamos.Find(idPrestamo);
                var diasDiferencia=(int)DateTime.Now.Subtract((DateTime)prestamo.FechaCreacion).TotalDays;
                if (diasDiferencia >= 7)
                {
                    //Calculo de moras
                    var cuotas = db.Cuotas.Where(e => e.IdPrestamo == idPrestamo).ToList();
                    var CuotasVencidas = db.Cuotas.Where(a => a.FechaCuota < DateTime.Now && a.FechaPago == null && a.IdPrestamo == idPrestamo).ToList();
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

                        //ultima cuota con mora
                        if (cuotas.Max(e => e.IdCuota) == item.IdCuota)
                        {
                            diasMora = (int)DateTime.Now.Subtract((DateTime)item.FechaCuota).TotalDays;
                            Mora = diasMora * 5;
                            //Seteo de valores de mora
                            item.Mora = Mora;
                            item.DiasMora = diasMora;
                        }
                    }
                    db.SaveChanges();
                    //---------------
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public class formulario
        {
            public decimal monto { get; set; }
            public decimal mora { get; set; }
            public string voucher { get; set; }
            public string observaciones { get; set; }
            public string tipo { get; set; }
            public int idCuota { get; set; }
            public DateTime fechaAbono { get; set; }
            public int banco { get; set; }
            public int idAbono { get; set; }
            public string numeroVoucher { get; set; }
            public int tipoAbono { get; set; }
            public string gestor { get; set; }
        }
        public class PrestamoCliente { 
            public int IdCliente { get; set; }
            public string codigoGestor { get; set; }
            public string autorizacion { get; set; }
            public decimal fondo { get; set; }
            public DateTime fechaEntrega { get; set; }
            public decimal capital { get; set; }
            public decimal capitalPendiente { get; set; }
            public DateTime fecha1Cuota { get; set; }
            public string diaPago { get; set; }
            public int numeroCuotas { get; set; }
            public decimal montoCuotas { get; set; }
            public int diasgracia { get; set; }
        }
        #endregion

        #region Estado Cuenta
        [HttpPost]
        public ActionResult EstadoCuenta(int idPrestamo)
        {
            try
            {
                //Calculo de moras
                ActualizarMoras(idPrestamo);
                //---------------

                //var cuotas = db.Cuotas.Where(e => e.IdPrestamo == idPrestamo).ToList();
                var modelo = (from c in db.Cuotas.Where(e => e.IdPrestamo == idPrestamo)
                              from a in db.Abonos.Where(e => e.IdCuota == c.IdCuota).DefaultIfEmpty()
                              from b in db.Bancos.Where(e=> e.IdBanco==a.Banco).DefaultIfEmpty()

                              group new { c, a,b } by new { c.DiasMora, c.FechaPago, c.IdCuota, c.FechaCuota, c.MontoCuota, c.Mora, c.Observaciones, } into g
                              select new
                              {
                                  g.Key.FechaPago,
                                  g.Key.IdCuota,
                                  g.Key.FechaCuota,
                                  g.Key.MontoCuota,
                                  g.Key.Mora,
                                  g.Key.DiasMora,
                                  g.Key.Observaciones,
                                  Abono = g.Sum(e => e.a.MontoAbono),
                                  AbonoMora = g.Sum(e => e.a.MontoMora),
                                  NroOperacion= string.Join(",", g.Select(i => i.a.Codigo)),
                                  Banco= string.Join(",", g.Select(i => i.b.RazonSocial))

                              }).OrderBy(a=>a.FechaCuota).ToList();
                var abonoTotal = modelo.Sum(e => e.Abono);
                var abonoMoraTotal = modelo.Sum(e => e.AbonoMora);
                var moraTotal = modelo.Sum(e => e.Mora);
                var diasMoraTotal = modelo.Sum(e => e.DiasMora);
                return Json(new { success = true, cuotas = modelo, abonoTotal, abonoMoraTotal, moraTotal, diasMoraTotal });
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        #endregion
    }
}
