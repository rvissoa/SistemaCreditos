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
                            dni=c.Dni,
                            cliente=c.NombreCliente+" "+c.ApellidosCliente,
                            direccion=c.Direccion+" "+c.Distrito,
                            celular=c.Celular
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

        public class cliente
        {
            public int idDistrito { get; set; }
            public string zona { get; set; }
            public int gestor { get; set; }
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
