using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditos.Models;
using System;
using System.Data.Common;

namespace SistemaCreditos.Controllers.Asistencias
{
    [Authorize]
    public class AsistenciaController : Controller
    {
        private Model db = new Model();
        private readonly ILogger<AsistenciaController> _logger;

        public AsistenciaController(ILogger<AsistenciaController> logger)
        {
            _logger = logger;
        }

        [Authorize(Policy = "Admin")]
        public IActionResult ReporteAsistencia()
        {
            return View();
        }
        [Authorize(Policy = "Admin")]
        [HttpPost]  
        public IActionResult ReporteAsistencias(string request)
        {
            string[] fechas = request.Split(" - ");
            var fechaInicio = Util.convertirFecha(fechas[0]);
            var fechaFin = fechaInicio;
            if (fechas.Count()>1)
              fechaFin = Util.convertirFecha(fechas[1]);

            var model = (from a in db.Asistencia.Where(e => e.FechaAsistencia>=fechaInicio&&e.FechaAsistencia<=fechaFin)
                        from t in db.Trabajadors.Where(e => e.IdTrabajador == a.IdTrabajador).DefaultIfEmpty()
                        select new
                        {
                            trabajador = t.Nombres + " " + t.Apellidos,
                            a.FechaAsistencia,
                            a.HoraEntrada,
                            a.HoraSalida,
                            a.HoraAlmuerzo,
                            a.HoraAlmuerzoRegreso
                        }).ToList();
            //var draw = 0;
            //var recordsTotal = model.Count;
            //var recordsFiltered = model.Count;
            return new JsonResult( model);
        }

        #region Marcar asistencia
        public IActionResult Marcar()
        {
            //Zona horaria
            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            DateTime DateLima = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

            //Determinar usaurio
            var usuario = @User?.Claims.Where(e => e.Type == "preferred_username").Select(e => e.Value).FirstOrDefault();
            var IdTrabajador = db.Trabajadors.Where(e => usuario.Contains(e.Usuario)).Select(e => e.IdTrabajador).FirstOrDefault();

            //Validar asistencia de hoy
            var fechaHoy = DateLima;
            var modeloHoy = db.Asistencia.Where(e => e.FechaAsistencia.Value.Date == fechaHoy.Date && e.IdTrabajador == IdTrabajador).FirstOrDefault();

            if (modeloHoy!=null)
            {
                //Variable que determina si tiene marca
                ViewBag.FechaAsistencia = modeloHoy.FechaAsistencia?.ToString("dd/MMM/yyyy");
                ViewBag.HoraEntrada = modeloHoy.HoraEntrada;
                ViewBag.HoraAlmuerzo = modeloHoy.HoraAlmuerzo;
                ViewBag.HoraAlmuerzoRegreso = modeloHoy.HoraAlmuerzoRegreso;
                ViewBag.HoraSalida = modeloHoy.HoraSalida;
            }
            else
            {
                //Variable que determina si tiene marca
                ViewBag.FechaAsistencia = null;
                ViewBag.HoraEntrada = null;
                ViewBag.HoraAlmuerzo = null;
                ViewBag.HoraAlmuerzoRegreso = null;
                ViewBag.HoraSalida = null;
            }
            

            return View();
        }
        [HttpPost]
        public IActionResult ConsultaMarcacion()
        {
            //Zona horaria
            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            DateTime DateLima = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

            //Determinar usaurio
            var usuario = @User?.Claims.Where(e => e.Type == "preferred_username").Select(e => e.Value).FirstOrDefault();
            var IdTrabajador = db.Trabajadors.Where(e => usuario.Contains(e.Usuario)).Select(e => e.IdTrabajador).FirstOrDefault();

            //Validar asistencia de hoy
            var fechaHoy = DateLima;
            var modeloHoy = db.Asistencia.Where(e => e.FechaAsistencia.Value.Date == fechaHoy.Date && e.IdTrabajador == IdTrabajador).FirstOrDefault();

            var FechaAsistencia = "";
            var HoraEntrada = "";
            var HoraAlmuerzo = "";
            var HoraAlmuerzoRegreso = "";
            var HoraSalida = "";

            if (modeloHoy != null)
            {
                //Variable que determina si tiene marca
                FechaAsistencia = modeloHoy.FechaAsistencia?.ToString("dd/MMM/yyyy");
                HoraEntrada = modeloHoy.HoraEntrada;
                HoraAlmuerzo = modeloHoy.HoraAlmuerzo;
                HoraAlmuerzoRegreso = modeloHoy.HoraAlmuerzoRegreso;
                HoraSalida = modeloHoy.HoraSalida;
            }


            return new JsonResult(new { success = true, FechaAsistencia, HoraEntrada, HoraAlmuerzo, HoraAlmuerzoRegreso, HoraSalida });
        }

        [HttpPost]
        public IActionResult InsertEntrada()
        {
            try
            {
                //Zona horaria
                DateTime timeUtc = DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                DateTime DateLima = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

                //Determinar usaurio
                var usuario = @User?.Claims.Where(e => e.Type == "preferred_username").Select(e => e.Value).FirstOrDefault();
                var IdTrabajador = db.Trabajadors.Where(e => usuario.Contains(e.Usuario)).Select(e => e.IdTrabajador).FirstOrDefault();

                //Validar asistencia de hoy
                var fechaHoy = DateLima;
                var modeloHoy = db.Asistencia.Where(e => e.FechaAsistencia.Value.Date == fechaHoy.Date && e.IdTrabajador == IdTrabajador).FirstOrDefault();

                //Insertar horario de ingreso
                if (modeloHoy == null)
                {
                    var model = new Asistencia
                    {
                        FechaAsistencia = DateLima,
                        HoraEntrada = DateLima.ToShortTimeString(),
                        IdTrabajador = IdTrabajador,
                        //Observaciones = "",
                        UsuarioIngresa = "App",
                        FechaCreacion = DateLima
                    };
                    db.Asistencia.Add(model);

                    db.SaveChanges();
                    return new JsonResult(new { success = true, insert = true });
                }
                else
                    return new JsonResult(new { success = true, insert = false });
            }
            catch (Exception e)
            {
                return new JsonResult(new { success = false, error = e.Message });
            }
        
        }

        [HttpPost]
        public IActionResult InsertAlmuerzo()
        {
            try
            {
                //Zona horaria
                DateTime timeUtc = DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                DateTime DateLima = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

                //Determinar usaurio
                var usuario = @User?.Claims.Where(e => e.Type == "preferred_username").Select(e => e.Value).FirstOrDefault();
                var IdTrabajador = db.Trabajadors.Where(e => usuario.Contains(e.Usuario)).Select(e => e.IdTrabajador).FirstOrDefault();

                //Validar asistencia de hoy
                var fechaHoy = DateLima;
                var modeloHoy = db.Asistencia.Where(e => e.FechaAsistencia.Value.Date == fechaHoy.Date && e.IdTrabajador == IdTrabajador).FirstOrDefault();

                //Insertar horario de ingreso
                if (modeloHoy != null && modeloHoy.HoraAlmuerzo==null)
                {
                    modeloHoy.HoraAlmuerzo = DateLima.ToShortTimeString();
                    modeloHoy.FechaModificacion = DateLima;
                    modeloHoy.UsuarioModifica = "app";

                    db.SaveChanges();
                    return new JsonResult(new { success = true, insert = true });
                }
                else
                    return new JsonResult(new { success = true, insert = false });
            }
            catch (Exception e)
            {
                return new JsonResult(new { success = false, error = e.Message });
            }

        }

        [HttpPost]
        public IActionResult InsertAlmuerzoRegreso()
        {
            try
            {
                //Zona horaria
                DateTime timeUtc = DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                DateTime DateLima = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

                //Determinar usaurio
                var usuario = @User?.Claims.Where(e => e.Type == "preferred_username").Select(e => e.Value).FirstOrDefault();
                var IdTrabajador = db.Trabajadors.Where(e => usuario.Contains(e.Usuario)).Select(e => e.IdTrabajador).FirstOrDefault();

                //Validar asistencia de hoy
                var fechaHoy = DateLima;
                var modeloHoy = db.Asistencia.Where(e => e.FechaAsistencia.Value.Date == fechaHoy.Date && e.IdTrabajador == IdTrabajador).FirstOrDefault();

                //Insertar horario de ingreso
                if (modeloHoy != null && modeloHoy.HoraAlmuerzoRegreso == null && modeloHoy.HoraAlmuerzo != null)
                {
                    modeloHoy.HoraAlmuerzoRegreso = DateLima.ToShortTimeString();
                    modeloHoy.FechaModificacion = DateLima;
                    modeloHoy.UsuarioModifica = "app";

                    db.SaveChanges();
                    return new JsonResult(new { success = true, insert = true });
                }
                else
                    return new JsonResult(new { success = true, insert = false });
            }
            catch (Exception e)
            {
                return new JsonResult(new { success = false, error = e.Message });
            }

        }
        [HttpPost]
        public IActionResult InsertSalida()
        {
            try
            {
                //Zona horaria
                DateTime timeUtc = DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                DateTime DateLima = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

                //Determinar usaurio
                var usuario = @User?.Claims.Where(e => e.Type == "preferred_username").Select(e => e.Value).FirstOrDefault();
                var IdTrabajador = db.Trabajadors.Where(e => usuario.Contains(e.Usuario)).Select(e => e.IdTrabajador).FirstOrDefault();

                //Validar asistencia de hoy
                var fechaHoy = DateLima;
                var modeloHoy = db.Asistencia.Where(e => e.FechaAsistencia.Value.Date == fechaHoy.Date && e.IdTrabajador == IdTrabajador).FirstOrDefault();

                //Insertar horario de ingreso
                if (modeloHoy != null && modeloHoy.HoraSalida == null)
                {
                    modeloHoy.HoraSalida = DateLima.ToShortTimeString();
                    modeloHoy.FechaModificacion = DateLima;
                    modeloHoy.UsuarioModifica = "app";

                    db.SaveChanges();
                    return new JsonResult(new { success = true, insert = true });
                }
                else
                    return new JsonResult(new { success = true, insert = false });
            }
            catch (Exception e)
            {
                return new JsonResult(new { success = false, error = e.Message });
            }

        }

        public class body
        {
            public string fecha { get; set; }     
        }
        #endregion
    }
}

