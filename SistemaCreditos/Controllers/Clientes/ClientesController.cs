using Microsoft.AspNetCore.Mvc;
using SistemaCreditos.Models;
using static SistemaCreditos.Controllers.Clientes.ClientesController;
using static System.Net.Mime.MediaTypeNames;

namespace SistemaCreditos.Controllers.Clientes
{
    public class ClientesController : Controller
    {
        private Model db = new Model();
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
            var model = db.Distritos.Where(e => e.CodigoDepartamento == 15 && e.CodigoProvincia == 1).ToList();
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
    }
}
