using Microsoft.AspNetCore.Mvc;
using SistemaCreditos.Models;

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
    }
}
