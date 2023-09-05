using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditos.Models;
using System;

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
        public IActionResult Marcar()
        {
            //Zona horaria
            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            DateTime DateLima = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

            //Determinar usaurio
            var usuario = @User.Claims.ToArray()[4].Value;
            var IdTrabajador = db.Trabajadors.Where(e => usuario.Contains(e.Usuario)).Select(e => e.IdTrabajador).FirstOrDefault();

            //Validar asistencia de hoy
            var fechaHoy = DateLima;
            var modeloHoy = db.Asistencia.Where(e => e.FechaAsistencia.Value.Date == fechaHoy.Date && e.IdTrabajador == IdTrabajador).FirstOrDefault();
            
            //Variable que determina si tiene marca
            ViewBag.FechaAsistencia = modeloHoy.FechaAsistencia.Value.ToString("dd/MMM/yyyy");
            ViewBag.HoraEntrada = modeloHoy.HoraEntrada;
            ViewBag.HoraAlmuerzo = modeloHoy.HoraAlmuerzo;
            ViewBag.HoraAlmuerzoRegreso = modeloHoy.HoraAlmuerzoRegreso;
            ViewBag.HoraSalida = modeloHoy.HoraSalida;

            return View();
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
                var usuario = @User.Claims.ToArray()[4].Value;
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
                var usuario = @User.Claims.ToArray()[4].Value;
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
                var usuario = @User.Claims.ToArray()[4].Value;
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
                var usuario = @User.Claims.ToArray()[4].Value;
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

    }
}
