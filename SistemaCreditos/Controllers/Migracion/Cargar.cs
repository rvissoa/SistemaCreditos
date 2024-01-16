using LinqToExcel;
using Microsoft.AspNetCore.Mvc;
using SistemaCreditos.Models;
using System.Text.RegularExpressions;

namespace SistemaCreditos.Controllers.Migracion
{
    public class Cargar : Controller
    {
        private Model db = new Model();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(512*1024*1024)]
        [DisableRequestSizeLimit]
        public IActionResult Migrar(string excel)
        {
            #region EXCEL

            var doc = Convert.FromBase64String(excel);
            System.IO.File.WriteAllBytes(@"D://PROY/TIO CARLOS/NUEVO/SistemaCreditos/SistemaCreditos/App_Data/" + "temp" + ".xlsx", doc);
            //var excelFile = new LinqToExcel.ExcelQueryFactory(@"D://PROY/TIO CARLOS/Creditos1/Prestamos/App_Data/temp.xlsx");
            var excelFile = new ExcelQueryFactory(@"D://PROY/TIO CARLOS/NUEVO/SistemaCreditos/SistemaCreditos/App_Data/temp.xlsx");

            //produccion
            //System.IO.File.WriteAllBytes(@"C://CreditosDiego/App_Data/" + "temp" + ".xlsx", doc);
            //var excelFile = new LinqToExcel.ExcelQueryFactory(@"C://CreditosDiego/App_Data/temp.xlsx");
            var result =
                from row in excelFile.Worksheet("base")
                let item = new
                {
                    //Cliente (OK)
                    CODIGO_CLIENTE = row["COD-CL"].Cast<string>(),
                    CODIGO_GESTOR = row["COD-(G)"].Cast<string>(),
                    //NRO_CREDITO = row["NRO-CREDITO"].Cast<string>(),
                    //ESTADO_CL = row["ESTADO-CL"].Cast<string>(),
                    NOMBRE_CLIENTE = row["CLIENTE-NOMBRE"].Cast<string>(),
                    APELLIDO_CLIENTE = row["CLIENTE-APELLIDO"].Cast<string>(),
                    DIRECCION = row["DIRECCION"].Cast<string>(),
                    UBICACION_REFERENCIA = row["UBICACIÓN"].Cast<string>(),
                    DISTRITO = row["ID_DISTRITO"].Cast<string>(),
                    ZONA = row["ZONA"].Cast<string>(),
                    DNI = row["DNI"].Cast<string>(),
                    CELULAR = row["CELULAR"].Cast<string>(),
                    TRABAJO_OCUPACION = row["TRABAJO"].Cast<string>(),

                    //Prestamo
                    AUTORIZA = row["AUT"].Cast<string>(),
                    FONDO_PROVICIONAL = row["FONDO"].Cast<string>(),
                    //DM = row["DM"].Cast<string>(),

                    //CODIGO_GESTOR = row["COD-(G)"].Cast<string>(),
                    DIA_PAGO = row["DIA-DE-PAGO"].Cast<string>(),
                    FECHA_ENTREGA = row["F-ENTREGA"].Cast<string>(),
                    //ULTIMA_FECHA_PAGO = row["U-FP"].Cast<string>(),
                    CAPITAL = row["CAPITAL"].Cast<string>(),
                    CAPITAL_PENDIENTE = row["CAP-PEND"].Cast<string>(),
                    //SALDO_RESTANTE = row["S-RES"].Cast<string>(),

                    //Cuotas
                    #region Cuotas Excel
                    FECHA_CUOTA1 = row["CUOTA1"].Cast<string>(),
                    ESTADO1 = row["ESTADO1"].Cast<string>(),
                    MONTO_ABONO1 = row["ABONO1"].Cast<string>(),
                    FECHA_PAGO1 = row["F-PAGO1"].Cast<string>(),
                    MORA1 = row["MORAS1"].Cast<string>(),
                    D_M1 = row["D-MORA1"].Cast<string>(),
                    M_PDTE1 = row["M-PDTE1"].Cast<string>(),
                    CODIGO1 = row["NRO-OP1"].Cast<string>(),
                    BANCO1 = row["BANCO1"].Cast<string>(),

                    FECHA_CUOTA2 = row["CUOTA2"].Cast<string>(),
                    ESTADO2 = row["ESTADO2"].Cast<string>(),
                    MONTO_ABONO2 = row["ABONO2"].Cast<string>(),
                    FECHA_PAGO2 = row["F-PAGO2"].Cast<string>(),
                    MORA2 = row["MORAS2"].Cast<string>(),
                    D_M2 = row["D-MORA2"].Cast<string>(),
                    M_PDTE2 = row["M-PDTE2"].Cast<string>(),
                    CODIGO2 = row["NRO-OP2"].Cast<string>(),
                    BANCO2 = row["BANCO2"].Cast<string>(),

                    FECHA_CUOTA3 = row["CUOTA3"].Cast<string>(),
                    ESTADO3 = row["ESTADO3"].Cast<string>(),
                    MONTO_ABONO3 = row["ABONO3"].Cast<string>(),
                    FECHA_PAGO3 = row["F-PAGO3"].Cast<string>(),
                    MORA3 = row["MORAS3"].Cast<string>(),
                    D_M3 = row["D-MORA3"].Cast<string>(),
                    M_PDTE3 = row["M-PDTE3"].Cast<string>(),
                    CODIGO3 = row["NRO-OP3"].Cast<string>(),
                    BANCO3 = row["BANCO3"].Cast<string>(),

                    FECHA_CUOTA4 = row["CUOTA4"].Cast<string>(),
                    ESTADO4 = row["ESTADO4"].Cast<string>(),
                    MONTO_ABONO4 = row["ABONO4"].Cast<string>(),
                    FECHA_PAGO4 = row["F-PAGO4"].Cast<string>(),
                    MORA4 = row["MORAS4"].Cast<string>(),
                    D_M4 = row["D-MORA4"].Cast<string>(),
                    M_PDTE4 = row["M-PDTE4"].Cast<string>(),
                    CODIGO4 = row["NRO-OP4"].Cast<string>(),
                    BANCO4 = row["BANCO4"].Cast<string>(),

                    FECHA_CUOTA5 = row["CUOTA5"].Cast<string>(),
                    ESTADO5 = row["ESTADO5"].Cast<string>(),
                    MONTO_ABONO5 = row["ABONO5"].Cast<string>(),
                    FECHA_PAGO5 = row["F-PAGO5"].Cast<string>(),
                    MORA5 = row["MORAS5"].Cast<string>(),
                    D_M5 = row["D-MORA5"].Cast<string>(),
                    M_PDTE5 = row["M-PDTE5"].Cast<string>(),
                    CODIGO5 = row["NRO-OP5"].Cast<string>(),
                    BANCO5 = row["BANCO5"].Cast<string>(),

                    FECHA_CUOTA6 = row["CUOTA6"].Cast<string>(),
                    ESTADO6 = row["ESTADO6"].Cast<string>(),
                    MONTO_ABONO6 = row["ABONO6"].Cast<string>(),
                    FECHA_PAGO6 = row["F-PAGO6"].Cast<string>(),
                    MORA6 = row["MORAS6"].Cast<string>(),
                    D_M6 = row["D-MORA6"].Cast<string>(),
                    M_PDTE6 = row["M-PDTE6"].Cast<string>(),
                    CODIGO6 = row["NRO-OP6"].Cast<string>(),
                    BANCO6 = row["BANCO6"].Cast<string>(),

                    FECHA_CUOTA7 = row["CUOTA7"].Cast<string>(),
                    ESTADO7 = row["ESTADO7"].Cast<string>(),
                    MONTO_ABONO7 = row["ABONO7"].Cast<string>(),
                    FECHA_PAGO7 = row["F-PAGO7"].Cast<string>(),
                    MORA7 = row["MORAS7"].Cast<string>(),
                    D_M7 = row["D-MORA7"].Cast<string>(),
                    M_PDTE7 = row["M-PDTE7"].Cast<string>(),
                    CODIGO7 = row["NRO-OP7"].Cast<string>(),
                    BANCO7 = row["BANCO7"].Cast<string>(),

                    FECHA_CUOTA8 = row["CUOTA8"].Cast<string>(),
                    ESTADO8 = row["ESTADO8"].Cast<string>(),
                    MONTO_ABONO8 = row["ABONO8"].Cast<string>(),
                    FECHA_PAGO8 = row["F-PAGO8"].Cast<string>(),
                    MORA8 = row["MORAS8"].Cast<string>(),
                    D_M8 = row["D-MORA8"].Cast<string>(),
                    M_PDTE8 = row["M-PDTE8"].Cast<string>(),
                    CODIGO8 = row["NRO-OP8"].Cast<string>(),
                    BANCO8 = row["BANCO8"].Cast<string>(),

                    FECHA_CUOTA9 = row["CUOTA9"].Cast<string>(),
                    ESTADO9 = row["ESTADO9"].Cast<string>(),
                    MONTO_ABONO9 = row["ABONO9"].Cast<string>(),
                    FECHA_PAGO9 = row["F-PAGO9"].Cast<string>(),
                    MORA9 = row["MORAS9"].Cast<string>(),
                    D_M9 = row["D-MORA9"].Cast<string>(),
                    M_PDTE9 = row["M-PDTE9"].Cast<string>(),
                    CODIGO9 = row["NRO-OP9"].Cast<string>(),
                    BANCO9 = row["BANCO9"].Cast<string>(),

                    FECHA_CUOTA10 = row["CUOTA10"].Cast<string>(),
                    ESTADO10 = row["ESTADO10"].Cast<string>(),
                    MONTO_ABONO10 = row["ABONO10"].Cast<string>(),
                    FECHA_PAGO10 = row["F-PAGO10"].Cast<string>(),
                    MORA10 = row["MORAS10"].Cast<string>(),
                    D_M10 = row["D-MORA10"].Cast<string>(),
                    M_PDTE10 = row["M-PDTE10"].Cast<string>(),
                    CODIGO10 = row["NRO-OP10"].Cast<string>(),
                    BANCO10 = row["BANCO10"].Cast<string>(),

                    FECHA_CUOTA11 = row["CUOTA11"].Cast<string>(),
                    ESTADO11 = row["ESTADO11"].Cast<string>(),
                    MONTO_ABONO11 = row["ABONO11"].Cast<string>(),
                    FECHA_PAGO11 = row["F-PAGO11"].Cast<string>(),
                    MORA11 = row["MORAS11"].Cast<string>(),
                    D_M11 = row["D-MORA11"].Cast<string>(),
                    M_PDTE11 = row["M-PDTE11"].Cast<string>(),
                    CODIGO11 = row["NRO-OP11"].Cast<string>(),
                    BANCO11 = row["BANCO11"].Cast<string>(),

                    FECHA_CUOTA12 = row["CUOTA12"].Cast<string>(),
                    ESTADO12 = row["ESTADO12"].Cast<string>(),
                    MONTO_ABONO12 = row["ABONO12"].Cast<string>(),
                    FECHA_PAGO12 = row["F-PAGO12"].Cast<string>(),
                    MORA12 = row["MORAS12"].Cast<string>(),
                    D_M12 = row["D-MORA12"].Cast<string>(),
                    M_PDTE12 = row["M-PDTE12"].Cast<string>(),
                    CODIGO12 = row["NRO-OP12"].Cast<string>(),
                    BANCO12 = row["BANCO12"].Cast<string>(),
                    #endregion


                }
                //where item.RollNumber == "2" || item.RollNumber == "3"
                select item;
            #endregion
            #region LLENADO CLIENTES
            using (var trans = db.Database.BeginTransaction())
            {
                foreach (var item in result)
                {
                    var modelPrestamo = new Prestamo();
                    if (!db.Clientes.Select(e => e.Dni).Contains(item.DNI)|| item.DNI==null)
                    {
                        //Agregando nuevo cliente
                        var modelCliente = new Cliente
                        {
                            CodigoCliente = item.CODIGO_CLIENTE,
                            CodigoGestor = item.CODIGO_GESTOR,
                            NombreCliente = item.NOMBRE_CLIENTE,
                            ApellidosCliente = item.APELLIDO_CLIENTE,
                            Direccion = item.DIRECCION,
                            Distrito= Convert.ToInt32(item.DISTRITO),
                            UbicacionReferencia = item.UBICACION_REFERENCIA,
                            Zona = item.ZONA,
                            Dni= item.DNI!=null?item.DNI.Trim():null,
                            Celular = item.CELULAR!=null? (item.CELULAR.Length>20? item.CELULAR.Substring(0, 20): item.CELULAR):null,
                            TrabajoOcupacion = item.TRABAJO_OCUPACION
                        };
                        db.Clientes.Add(modelCliente);
                        db.SaveChanges();

                        //Agregando prestamo
                        modelPrestamo.IdCliente = modelCliente.IdCliente;
                        modelPrestamo.DiaPago = item.DIA_PAGO;
                        modelPrestamo.Autorizacion = item.AUTORIZA;
                        if (item.FONDO_PROVICIONAL != null && EsDecimal(item.FONDO_PROVICIONAL)) modelPrestamo.FondoProvisional = Convert.ToDecimal(item.FONDO_PROVICIONAL);
                        if (item.FECHA_ENTREGA!=null && EsFecha(item.FECHA_ENTREGA)) modelPrestamo.FechaEntrega = Convert.ToDateTime(item.FECHA_ENTREGA);
                        if (item.CAPITAL != null) modelPrestamo.Capital = Convert.ToDecimal(item.CAPITAL);
                        if (item.CAPITAL_PENDIENTE != null) modelPrestamo.CapitalPendiente = Convert.ToDecimal(item.CAPITAL_PENDIENTE);
                        //if (item.ULTIMA_FECHA_PAGO != null) modelPrestamo.FechaTermino = Convert.ToDateTime(item.ULTIMA_FECHA_PAGO);
                        //SALDO_RESTANTE = row["S-RES"].Cast<string>(),
                        db.Prestamos.Add(modelPrestamo);
                        db.SaveChanges();
                    }
                    else
                    {
                        //Asiganando nuevo cliente
                        var modelCliente = db.Clientes.Where(e => item.DNI.Contains(e.Dni)).FirstOrDefault();
                        //Agregando prestamo
                        modelPrestamo.IdCliente = modelCliente.IdCliente;
                        modelPrestamo.DiaPago = item.DIA_PAGO;
                        modelPrestamo.Autorizacion = item.AUTORIZA;
                        if (item.FONDO_PROVICIONAL != null && EsDecimal(item.FONDO_PROVICIONAL)) modelPrestamo.FondoProvisional = Convert.ToDecimal(item.FONDO_PROVICIONAL);
                        if (item.FECHA_ENTREGA != null && EsFecha(item.FECHA_ENTREGA)) modelPrestamo.FechaEntrega = Convert.ToDateTime(item.FECHA_ENTREGA);
                        if (item.CAPITAL != null) modelPrestamo.Capital = Convert.ToDecimal(item.CAPITAL);
                        if (item.CAPITAL_PENDIENTE != null) modelPrestamo.CapitalPendiente = Convert.ToDecimal(item.CAPITAL_PENDIENTE);
                        //if (item.ULTIMA_FECHA_PAGO != null) modelPrestamo.FechaTermino = Convert.ToDateTime(item.ULTIMA_FECHA_PAGO);
                        //SALDO_RESTANTE = row["S-RES"].Cast<string>(),
                        db.Prestamos.Add(modelPrestamo);
                        db.SaveChanges();
                    } 
                    //Agregando Cuotas y Abonos
                    #region CUOTAS  
                    #region Cuota1
                    //Cuota1
                    var fc = (item.FECHA_CUOTA1 != null && item.FECHA_CUOTA1 != "" && item.FECHA_CUOTA1 != " " && EsFecha(item.FECHA_CUOTA1)) ? (DateTime?)Convert.ToDateTime(item.FECHA_CUOTA1) : null;
                    var fp = (item.FECHA_PAGO1 != null && item.FECHA_PAGO1 != "" && item.FECHA_PAGO1 != " " && EsFecha(item.FECHA_PAGO1)) ? (DateTime?)Convert.ToDateTime(item.FECHA_PAGO1) : null;
                    var modelCuotas = new Cuota
                    {
                        IdPrestamo = modelPrestamo.IdPrestamo,
                        FechaCuota = fc,
                        Observaciones = "Estado: " + item.ESTADO1 + " - Mora pendiente: " + item.M_PDTE1 + " - Pago banco: "+item.BANCO1,
                        FechaPago = fp,
                        Mora = Convert.ToDecimal(item.MORA1),
                        DiasMora = Convert.ToInt32(item.D_M1)
                    };
                    db.Cuotas.Add(modelCuotas);
                    db.SaveChanges();
                    //asignando bancos
                    var banco = 0;
                    switch (item.BANCO1)
                    {
                        case "BCP": banco = 1; break;
                        case "BBVA": banco = 2; break;
                        case "STK": banco = 3; break;
                        case "IBK": banco = 4; break;
                        case "PLIN": banco = 5; break;
                        case "YAPE": banco = 6; break;
                        default: banco = 8; break;
                    }
                    //Abono1
                    var modelAbono = new Abono
                    {
                        IdCuota = modelCuotas.IdCuota,
                        MontoAbono = Convert.ToDecimal(item.MONTO_ABONO1),
                        Codigo = item.CODIGO1,
                        Banco = banco,
                        FechaAbono=fp
                    };
                    db.Abonos.Add(modelAbono);
                    db.SaveChanges();
                    #endregion Cuota1
                    #region Cuota2
                    //Cuota1
                    var fc2 = (item.FECHA_CUOTA2 != null && item.FECHA_CUOTA2 != "" && item.FECHA_CUOTA2 != " " && EsFecha(item.FECHA_CUOTA2)) ? (DateTime?)Convert.ToDateTime(item.FECHA_CUOTA2) : null;
                    var fp2 = (item.FECHA_PAGO2 != null && item.FECHA_PAGO2 != "" && item.FECHA_PAGO2 != " " && EsFecha(item.FECHA_PAGO2)) ? (DateTime?)Convert.ToDateTime(item.FECHA_PAGO2) : null;
                    var modelCuotas2 = new Cuota
                    {
                        IdPrestamo = modelPrestamo.IdPrestamo,
                        FechaCuota = fc2,
                        Observaciones = "Estado: " + item.ESTADO2 + " - Mora pendiente: " + item.M_PDTE2 + " - Pago banco: "+item.BANCO2,
                        FechaPago = fp2,
                        Mora = Convert.ToDecimal(item.MORA2),
                        DiasMora = Convert.ToInt32(item.D_M2)
                    };
                    db.Cuotas.Add(modelCuotas2);
                    db.SaveChanges();
                    //asignando bancos
                    var banco2 = 0;
                    switch (item.BANCO2)
                    {
                        case "BCP": banco = 1; break;
                        case "BBVA": banco = 2; break;
                        case "STK": banco = 3; break;
                        case "IBK": banco = 4; break;
                        case "PLIN": banco = 5; break;
                        case "YAPE": banco = 6; break;
                        default: banco = 8; break;
                    }
                    //Abono1
                    var modelAbono2 = new Abono
                    {
                        IdCuota = modelCuotas2.IdCuota,
                        MontoAbono = Convert.ToDecimal(item.MONTO_ABONO2),
                        Codigo = item.CODIGO2,
                        Banco = banco2,
                        FechaAbono = fp2
                    };
                    db.Abonos.Add(modelAbono2);
                    db.SaveChanges();
                    #endregion Cuota2
                    #region Cuota3
                    //Cuota1
                    var fc3 = (item.FECHA_CUOTA3 != null && item.FECHA_CUOTA3 != "" && item.FECHA_CUOTA3 != " " && EsFecha(item.FECHA_CUOTA3)) ? (DateTime?)Convert.ToDateTime(item.FECHA_CUOTA3) : null;
                    var fp3 = (item.FECHA_PAGO3 != null && item.FECHA_PAGO3 != "" && item.FECHA_PAGO3 != " " && EsFecha(item.FECHA_PAGO3)) ? (DateTime?)Convert.ToDateTime(item.FECHA_PAGO3) : null;
                    var modelCuotas3 = new Cuota
                    {
                        IdPrestamo = modelPrestamo.IdPrestamo,
                        FechaCuota = fc3,
                        Observaciones = "Estado: " + item.ESTADO3 + " - Mora pendiente: " + item.M_PDTE3 + " - Pago banco: " + item.BANCO3,
                        FechaPago = fp3,
                        Mora = Convert.ToDecimal(item.MORA3),
                        DiasMora = Convert.ToInt32(item.D_M3)
                    };
                    db.Cuotas.Add(modelCuotas3);
                    db.SaveChanges();
                    //asignando bancos
                    var banco3 = 0;
                    switch (item.BANCO3)
                    {
                        case "BCP": banco = 1; break;
                        case "BBVA": banco = 2; break;
                        case "STK": banco = 3; break;
                        case "IBK": banco = 4; break;
                        case "PLIN": banco = 5; break;
                        case "YAPE": banco = 6; break;
                        default: banco = 8; break;
                    }
                    //Abono1
                    var modelAbono3 = new Abono
                    {
                        IdCuota = modelCuotas3.IdCuota,
                        MontoAbono = Convert.ToDecimal(item.MONTO_ABONO3),
                        Codigo = item.CODIGO3,
                        Banco = banco3,
                        FechaAbono = fp3
                    };
                    db.Abonos.Add(modelAbono3);
                    db.SaveChanges();
                    #endregion Cuota3
                    #region Cuota4
                    //Cuota4
                    var fc4 = (item.FECHA_CUOTA4 != null && item.FECHA_CUOTA4 != "" && item.FECHA_CUOTA4 != " " && EsFecha(item.FECHA_CUOTA4)) ? (DateTime?)Convert.ToDateTime(item.FECHA_CUOTA4) : null;
                    var fp4 = (item.FECHA_PAGO4 != null && item.FECHA_PAGO4 != "" && item.FECHA_PAGO4 != " " && EsFecha(item.FECHA_PAGO4)) ? (DateTime?)Convert.ToDateTime(item.FECHA_PAGO4) : null;
                    var modelCuotas4 = new Cuota
                    {
                        IdPrestamo = modelPrestamo.IdPrestamo,
                        FechaCuota = fc4,
                        Observaciones = "Estado: " + item.ESTADO4 + " - Mora pendiente: " + item.M_PDTE4 + " - Pago banco: " + item.BANCO4,
                        FechaPago = fp4,
                        Mora = Convert.ToDecimal(item.MORA4),
                        DiasMora = Convert.ToInt32(item.D_M4)
                    };
                    db.Cuotas.Add(modelCuotas4);
                    db.SaveChanges();
                    //asignando bancos
                    var banco4 = 0;
                    switch (item.BANCO4)
                    {
                        case "BCP": banco = 1; break;
                        case "BBVA": banco = 2; break;
                        case "STK": banco = 3; break;
                        case "IBK": banco = 4; break;
                        case "PLIN": banco = 5; break;
                        case "YAPE": banco = 6; break;
                        default: banco = 8; break;
                    }
                    //Abono1
                    var modelAbono4 = new Abono
                    {
                        IdCuota = modelCuotas4.IdCuota,
                        MontoAbono = Convert.ToDecimal(item.MONTO_ABONO4),
                        Codigo = item.CODIGO4,
                        Banco = banco4,
                        FechaAbono = fp4
                    };
                    db.Abonos.Add(modelAbono4);
                    db.SaveChanges();
                    #endregion Cuota4
                    #region Cuota5
                    //Cuota5
                    var fc5 = (item.FECHA_CUOTA5 != null && item.FECHA_CUOTA5 != "" && item.FECHA_CUOTA5 != " " && EsFecha(item.FECHA_CUOTA5)) ? (DateTime?)Convert.ToDateTime(item.FECHA_CUOTA5) : null;
                    var fp5 = (item.FECHA_PAGO5 != null && item.FECHA_PAGO5 != "" && item.FECHA_PAGO5 != " " && EsFecha(item.FECHA_PAGO5)) ? (DateTime?)Convert.ToDateTime(item.FECHA_PAGO5) : null;
                    var modelCuotas5 = new Cuota
                    {
                        IdPrestamo = modelPrestamo.IdPrestamo,
                        FechaCuota = fc5,
                        Observaciones = "Estado: " + item.ESTADO5 + " - Mora pendiente: " + item.M_PDTE5 + " - Pago banco: " + item.BANCO5,
                        FechaPago = fp5,
                        Mora = Convert.ToDecimal(item.MORA5),
                        DiasMora = Convert.ToInt32(item.D_M5)
                    };
                    db.Cuotas.Add(modelCuotas5);
                    db.SaveChanges();
                    //asignando bancos
                    var banco5 = 0;
                    switch (item.BANCO5)
                    {
                        case "BCP": banco = 1; break;
                        case "BBVA": banco = 2; break;
                        case "STK": banco = 3; break;
                        case "IBK": banco = 4; break;
                        case "PLIN": banco = 5; break;
                        case "YAPE": banco = 6; break;
                        default: banco = 8; break;
                    }
                    //Abono1
                    var modelAbono5 = new Abono
                    {
                        IdCuota = modelCuotas5.IdCuota,
                        MontoAbono = Convert.ToDecimal(item.MONTO_ABONO5),
                        Codigo = item.CODIGO5,
                        Banco = banco5,
                        FechaAbono = fp5
                    };
                    db.Abonos.Add(modelAbono5);
                    db.SaveChanges();
                    #endregion Cuota5
                    #region Cuota6
                    //Cuota6
                    var fc6 = (item.FECHA_CUOTA6 != null && item.FECHA_CUOTA6 != "" && item.FECHA_CUOTA6 != " " && EsFecha(item.FECHA_CUOTA6)) ? (DateTime?)Convert.ToDateTime(item.FECHA_CUOTA6) : null;
                    var fp6 = (item.FECHA_PAGO6 != null && item.FECHA_PAGO6 != "" && item.FECHA_PAGO6 != " " && EsFecha(item.FECHA_PAGO6)) ? (DateTime?)Convert.ToDateTime(item.FECHA_PAGO6) : null;
                    var modelCuotas6 = new Cuota
                    {
                        IdPrestamo = modelPrestamo.IdPrestamo,
                        FechaCuota = fc6,
                        Observaciones = "Estado: " + item.ESTADO6 + " - Mora pendiente: " + item.M_PDTE6 + " - Pago banco: " + item.BANCO6,
                        FechaPago = fp6,
                        Mora = Convert.ToDecimal(item.MORA6),
                        DiasMora = Convert.ToInt32(item.D_M6)
                    };
                    db.Cuotas.Add(modelCuotas6);
                    db.SaveChanges();
                    //asignando bancos
                    var banco6 = 0;
                    switch (item.BANCO6)
                    {
                        case "BCP": banco = 1; break;
                        case "BBVA": banco = 2; break;
                        case "STK": banco = 3; break;
                        case "IBK": banco = 4; break;
                        case "PLIN": banco = 5; break;
                        case "YAPE": banco = 6; break;
                        default: banco = 8; break;
                    }
                    //Abono1
                    var modelAbono6 = new Abono
                    {
                        IdCuota = modelCuotas6.IdCuota,
                        MontoAbono = Convert.ToDecimal(item.MONTO_ABONO6),
                        Codigo = item.CODIGO6,
                        Banco = banco6,
                        FechaAbono = fp6
                    };
                    db.Abonos.Add(modelAbono6);
                    db.SaveChanges();
                    #endregion Cuota6
                    #region Cuota7
                    //Cuota7
                    var fc7 = (item.FECHA_CUOTA7 != null && item.FECHA_CUOTA7 != "" && item.FECHA_CUOTA7 != " " && EsFecha(item.FECHA_CUOTA7)) ? (DateTime?)Convert.ToDateTime(item.FECHA_CUOTA7) : null;
                    var fp7 = (item.FECHA_PAGO7 != null && item.FECHA_PAGO7 != "" && item.FECHA_PAGO7 != " " && EsFecha(item.FECHA_PAGO7)) ? (DateTime?)Convert.ToDateTime(item.FECHA_PAGO7) : null;
                    var modelCuotas7 = new Cuota
                    {
                        IdPrestamo = modelPrestamo.IdPrestamo,
                        FechaCuota = fc7,
                        Observaciones = "Estado: " + item.ESTADO7 + " - Mora pendiente: " + item.M_PDTE7 + " - Pago banco: " + item.BANCO7,
                        FechaPago = fp7,
                        Mora = Convert.ToDecimal(item.MORA7),
                        DiasMora = Convert.ToInt32(item.D_M7)
                    };
                    db.Cuotas.Add(modelCuotas7);
                    db.SaveChanges();
                    //asignando bancos
                    var banco7 = 0;
                    switch (item.BANCO7)
                    {
                        case "BCP": banco = 1; break;
                        case "BBVA": banco = 2; break;
                        case "STK": banco = 3; break;
                        case "IBK": banco = 4; break;
                        case "PLIN": banco = 5; break;
                        case "YAPE": banco = 6; break;
                        default: banco = 8; break;
                    }
                    //Abono1
                    var modelAbono7 = new Abono
                    {
                        IdCuota = modelCuotas7.IdCuota,
                        MontoAbono = Convert.ToDecimal(item.MONTO_ABONO7),
                        Codigo = item.CODIGO7,
                        Banco = banco7,
                        FechaAbono = fp7
                    };
                    db.Abonos.Add(modelAbono7);
                    db.SaveChanges();
                    #endregion Cuota7
                    #region Cuota8
                    //Cuota8
                    var fc8 = (item.FECHA_CUOTA8 != null && item.FECHA_CUOTA8 != "" && item.FECHA_CUOTA8 != " " && EsFecha(item.FECHA_CUOTA8)) ? (DateTime?)Convert.ToDateTime(item.FECHA_CUOTA8) : null;
                    var fp8 = (item.FECHA_PAGO8 != null && item.FECHA_PAGO8 != "" && item.FECHA_PAGO8 != " " && EsFecha(item.FECHA_PAGO8)) ? (DateTime?)Convert.ToDateTime(item.FECHA_PAGO8) : null;
                    var modelCuotas8 = new Cuota
                    {
                        IdPrestamo = modelPrestamo.IdPrestamo,
                        FechaCuota = fc8,
                        Observaciones = "Estado: " + item.ESTADO8 + " - Mora pendiente: " + item.M_PDTE8 + " - Pago banco: " + item.BANCO8,
                        FechaPago = fp8,
                        Mora = Convert.ToDecimal(item.MORA8),
                        DiasMora = Convert.ToInt32(item.D_M8)
                    };
                    db.Cuotas.Add(modelCuotas8);
                    db.SaveChanges();
                    //asignando bancos
                    var banco8 = 0;
                    switch (item.BANCO8)
                    {
                        case "BCP": banco = 1; break;
                        case "BBVA": banco = 2; break;
                        case "STK": banco = 3; break;
                        case "IBK": banco = 4; break;
                        case "PLIN": banco = 5; break;
                        case "YAPE": banco = 6; break;
                        default: banco = 8; break;
                    }
                    //Abono1
                    var modelAbono8 = new Abono
                    {
                        IdCuota = modelCuotas8.IdCuota,
                        MontoAbono = Convert.ToDecimal(item.MONTO_ABONO8),
                        Codigo = item.CODIGO8,
                        Banco = banco8,
                        FechaAbono = fp8
                    };
                    db.Abonos.Add(modelAbono8);
                    db.SaveChanges();
                    #endregion Cuota8
                    #region Cuota9
                    //Cuota9
                    var fc9 = (item.FECHA_CUOTA9 != null && item.FECHA_CUOTA9 != "" && item.FECHA_CUOTA9 != " " && EsFecha(item.FECHA_CUOTA9)) ? (DateTime?)Convert.ToDateTime(item.FECHA_CUOTA9) : null;
                    var fp9 = (item.FECHA_PAGO9 != null && item.FECHA_PAGO9 != "" && item.FECHA_PAGO9 != " " && EsFecha(item.FECHA_PAGO9)) ? (DateTime?)Convert.ToDateTime(item.FECHA_PAGO9) : null;
                    var modelCuotas9 = new Cuota
                    {
                        IdPrestamo = modelPrestamo.IdPrestamo,
                        FechaCuota = fc9,
                        Observaciones = "Estado: " + item.ESTADO9 + " - Mora pendiente: " + item.M_PDTE9 + " - Pago banco: " + item.BANCO9,
                        FechaPago = fp9,
                        Mora = Convert.ToDecimal(item.MORA9),
                        DiasMora = Convert.ToInt32(item.D_M9)
                    };
                    db.Cuotas.Add(modelCuotas9);
                    db.SaveChanges();
                    //asignando bancos
                    var banco9 = 0;
                    switch (item.BANCO9)
                    {
                        case "BCP": banco = 1; break;
                        case "BBVA": banco = 2; break;
                        case "STK": banco = 3; break;
                        case "IBK": banco = 4; break;
                        case "PLIN": banco = 5; break;
                        case "YAPE": banco = 6; break;
                        default: banco = 8; break;
                    }
                    //Abono1
                    var modelAbono9 = new Abono
                    {
                        IdCuota = modelCuotas9.IdCuota,
                        MontoAbono = Convert.ToDecimal(item.MONTO_ABONO9),
                        Codigo = item.CODIGO9,
                        Banco = banco9,
                        FechaAbono = fp9
                    };
                    db.Abonos.Add(modelAbono9);
                    db.SaveChanges();
                    #endregion Cuota9
                    #region Cuota10
                    //Cuota10
                    var fc10 = (item.FECHA_CUOTA10 != null && item.FECHA_CUOTA10 != "" && item.FECHA_CUOTA10 != " " && EsFecha(item.FECHA_CUOTA10)) ? (DateTime?)Convert.ToDateTime(item.FECHA_CUOTA10) : null;
                    var fp10 = (item.FECHA_PAGO10 != null && item.FECHA_PAGO10 != "" && item.FECHA_PAGO10 != " " && EsFecha(item.FECHA_PAGO10)) ? (DateTime?)Convert.ToDateTime(item.FECHA_PAGO10) : null;
                    var modelCuotas10 = new Cuota
                    {
                        IdPrestamo = modelPrestamo.IdPrestamo,
                        FechaCuota = fc10,
                        Observaciones = "Estado: " + item.ESTADO10 + " - Mora pendiente: " + item.M_PDTE10 + " - Pago banco: " + item.BANCO10,
                        FechaPago = fp10,
                        Mora = Convert.ToDecimal(item.MORA10),
                        DiasMora = Convert.ToInt32(item.D_M10)
                    };
                    db.Cuotas.Add(modelCuotas10);
                    db.SaveChanges();
                    //asignando bancos
                    var banco10 = 0;
                    switch (item.BANCO10)
                    {
                        case "BCP": banco = 1; break;
                        case "BBVA": banco = 2; break;
                        case "STK": banco = 3; break;
                        case "IBK": banco = 4; break;
                        case "PLIN": banco = 5; break;
                        case "YAPE": banco = 6; break;
                        default: banco = 8; break;
                    }
                    //Abono1
                    var modelAbono10 = new Abono
                    {
                        IdCuota = modelCuotas10.IdCuota,
                        MontoAbono = Convert.ToDecimal(item.MONTO_ABONO10),
                        Codigo = item.CODIGO10,
                        Banco = banco10,
                        FechaAbono = fp10
                    };
                    db.Abonos.Add(modelAbono10);
                    db.SaveChanges();
                    #endregion Cuota10
                    #region Cuota11
                    //Cuota11
                    var fc11 = (item.FECHA_CUOTA11 != null && item.FECHA_CUOTA11 != "" && item.FECHA_CUOTA11 != " " && EsFecha(item.FECHA_CUOTA11)) ? (DateTime?)Convert.ToDateTime(item.FECHA_CUOTA11) : null;
                    var fp11 = (item.FECHA_PAGO11 != null && item.FECHA_PAGO11 != "" && item.FECHA_PAGO11 != " " && EsFecha(item.FECHA_PAGO11)) ? (DateTime?)Convert.ToDateTime(item.FECHA_PAGO11) : null;
                    var modelCuotas11 = new Cuota
                    {
                        IdPrestamo = modelPrestamo.IdPrestamo,
                        FechaCuota = fc11,
                        Observaciones = "Estado: " + item.ESTADO11 + " - Mora pendiente: " + item.M_PDTE11 + " - Pago banco: " + item.BANCO11,
                        FechaPago = fp11,
                        Mora = Convert.ToDecimal(item.MORA11),
                        DiasMora = Convert.ToInt32(item.D_M11)
                    };
                    db.Cuotas.Add(modelCuotas11);
                    db.SaveChanges();
                    //asignando bancos
                    var banco11 = 0;
                    switch (item.BANCO11)
                    {
                        case "BCP": banco = 1; break;
                        case "BBVA": banco = 2; break;
                        case "STK": banco = 3; break;
                        case "IBK": banco = 4; break;
                        case "PLIN": banco = 5; break;
                        case "YAPE": banco = 6; break;
                        default: banco = 8; break;
                    }
                    //Abono1
                    var modelAbono11 = new Abono
                    {
                        IdCuota = modelCuotas11.IdCuota,
                        MontoAbono = Convert.ToDecimal(item.MONTO_ABONO11),
                        Codigo = item.CODIGO11,
                        Banco = banco11,
                        FechaAbono = fp11
                    };
                    db.Abonos.Add(modelAbono11);
                    db.SaveChanges();
                    #endregion Cuota11
                    #region Cuota12
                    //Cuota12
                    var fc12 = (item.FECHA_CUOTA12 != null && item.FECHA_CUOTA12 != "" && item.FECHA_CUOTA12 != " " && EsFecha(item.FECHA_CUOTA12)) ? (DateTime?)Convert.ToDateTime(item.FECHA_CUOTA12) : null;
                    var fp12 = (item.FECHA_PAGO12 != null && item.FECHA_PAGO12 != "" && item.FECHA_PAGO12 != " " && EsFecha(item.FECHA_PAGO12)) ? (DateTime?)Convert.ToDateTime(item.FECHA_PAGO12) : null;
                    var modelCuotas12 = new Cuota
                    {
                        IdPrestamo = modelPrestamo.IdPrestamo,
                        FechaCuota = fc12,
                        Observaciones = "Estado: " + item.ESTADO12 + " - Mora pendiente: " + item.M_PDTE12 + " - Pago banco: " + item.BANCO12,
                        FechaPago = fp12,
                        Mora = Convert.ToDecimal(item.MORA12),
                        DiasMora = Convert.ToInt32(item.D_M12)
                    };
                    db.Cuotas.Add(modelCuotas12);
                    db.SaveChanges();
                    //asignando bancos
                    var banco12 = 0;
                    switch (item.BANCO12)
                    {
                        case "BCP": banco = 1; break;
                        case "BBVA": banco = 2; break;
                        case "STK": banco = 3; break;
                        case "IBK": banco = 4; break;
                        case "PLIN": banco = 5; break;
                        case "YAPE": banco = 6; break;
                        default: banco = 8; break;
                    }
                    //Abono1
                    var modelAbono12 = new Abono
                    {
                        IdCuota = modelCuotas12.IdCuota,
                        MontoAbono = Convert.ToDecimal(item.MONTO_ABONO12),
                        Codigo = item.CODIGO12,
                        Banco = banco12,
                        FechaAbono = fp12
                    };
                    db.Abonos.Add(modelAbono12);  
                    db.SaveChanges();
                    #endregion Cuota12
                    #endregion CUOTAS
                }

                db.SaveChanges();
                trans.Commit();
            }

            #endregion LLENADO CLIENTES
            return Json(new { success = true});
        }
        public static Boolean EsFecha(String fecha)
        {
            try
            {
                DateTime.Parse(fecha);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static Boolean EsDecimal(String valor)
        {
            try
            {
                Decimal.Parse(valor);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
