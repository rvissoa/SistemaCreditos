using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SistemaCreditos.Models
{
    public partial class Model : DbContext
    {
        public Model()
        {
        }

        public Model(DbContextOptions<Model> options)
            : base(options)
        {
        }

        public virtual DbSet<Abono> Abonos { get; set; } = null!;
        public virtual DbSet<Asistencia> Asistencia { get; set; } = null!;
        public virtual DbSet<Banco> Bancos { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Cuota> Cuotas { get; set; } = null!;
        public virtual DbSet<Distrito> Distritos { get; set; } = null!;
        public virtual DbSet<Prestamo> Prestamos { get; set; } = null!;
        public virtual DbSet<Trabajador> Trabajadors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:srv-prestamos.database.windows.net,1433;Initial Catalog=BD_CREDITOS;Persist Security Info=False;User ID=sa-bd-prestamos;Password=Adm1n@2023;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Abono>(entity =>
            {
                entity.HasKey(e => e.IdAbono)
                    .HasName("PK__ABONO__5856531EDAFBE172");

                entity.ToTable("ABONO");

                entity.Property(e => e.IdAbono).HasColumnName("ID_ABONO");

                entity.Property(e => e.Banco).HasColumnName("BANCO");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO");

                entity.Property(e => e.FechaAbono)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_ABONO");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION");

                entity.Property(e => e.FotoAbono).HasColumnName("FOTO_ABONO");

                entity.Property(e => e.IdCuota).HasColumnName("ID_CUOTA");

                entity.Property(e => e.MontoAbono)
                    .HasColumnType("decimal(10, 4)")
                    .HasColumnName("MONTO_ABONO");

                entity.Property(e => e.MontoMora)
                    .HasColumnType("decimal(10, 4)")
                    .HasColumnName("MONTO_MORA");

                entity.Property(e => e.TipoAbono).HasColumnName("TIPO_ABONO");

                entity.Property(e => e.TipoArchivo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TIPO_ARCHIVO");

                entity.Property(e => e.UsuarioIngresa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_INGRESA");

                entity.Property(e => e.UsuarioModifica)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICA");

                entity.Property(e => e.CodigoGestor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_GESTOR");

                entity.HasOne(d => d.IdCuotaNavigation)
                    .WithMany(p => p.Abonos)
                    .HasForeignKey(d => d.IdCuota)
                    .HasConstraintName("FK__ABONO__ID_CUOTA__68487DD7");
            });

            modelBuilder.Entity<Asistencia>(entity =>
            {
                entity.HasKey(e => e.IdAsistencia)
                    .HasName("PK__ASISTENC__631A11A18DCCF3F1");

                entity.ToTable("ASISTENCIA");

                entity.Property(e => e.IdAsistencia).HasColumnName("ID_ASISTENCIA");

                entity.Property(e => e.FechaAsistencia)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_ASISTENCIA");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION");

                entity.Property(e => e.HoraAlmuerzo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HORA_ALMUERZO");

                entity.Property(e => e.HoraAlmuerzoRegreso)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HORA_ALMUERZO_REGRESO");

                entity.Property(e => e.HoraEntrada)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HORA_ENTRADA");

                entity.Property(e => e.HoraSalida)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HORA_SALIDA");

                entity.Property(e => e.IdTrabajador).HasColumnName("ID_TRABAJADOR");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("OBSERVACIONES");

                entity.Property(e => e.UsuarioIngresa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_INGRESA");

                entity.Property(e => e.UsuarioModifica)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICA");

                entity.HasOne(d => d.IdTrabajadorNavigation)
                    .WithMany(p => p.Asistencia)
                    .HasForeignKey(d => d.IdTrabajador)
                    .HasConstraintName("FK__ASISTENCI__ID_TR__6FE99F9F");
            });

            modelBuilder.Entity<Banco>(entity =>
            {
                entity.HasKey(e => e.IdBanco)
                    .HasName("PK__BANCOS__1579AAABA906AD51");

                entity.ToTable("BANCOS");

                entity.Property(e => e.IdBanco).HasColumnName("ID_BANCO");

                entity.Property(e => e.Abreviacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ABREVIACION");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.RazonSocial)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("RAZON_SOCIAL");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__CLIENTES__23A341306262139F");

                entity.ToTable("CLIENTES");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.ApellidosAval)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("APELLIDOS_AVAL");

                entity.Property(e => e.ApellidosCliente)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("APELLIDOS_CLIENTE");

                entity.Property(e => e.ApellidosConyuge)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("APELLIDOS_CONYUGE");

                entity.Property(e => e.Celular)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CELULAR");

                entity.Property(e => e.CodigoCliente)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_CLIENTE");

                entity.Property(e => e.CodigoGestor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_GESTOR");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION");

                entity.Property(e => e.DireccionAval)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION_AVAL");

                entity.Property(e => e.DireccionConyuge)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DIRECCION_CONYUGE");

                entity.Property(e => e.Distrito).HasColumnName("DISTRITO");

                entity.Property(e => e.Dni)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("DNI");

                entity.Property(e => e.DniAval)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("DNI_AVAL");

                entity.Property(e => e.DniConyuge)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("DNI_CONYUGE");

                entity.Property(e => e.FechaIngresa)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_INGRESA");

                entity.Property(e => e.FechaModifica)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICA");

                entity.Property(e => e.Hijos)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("HIJOS");

                entity.Property(e => e.NombreAval)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_AVAL");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_CLIENTE");

                entity.Property(e => e.NombreConyuge)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_CONYUGE");

                entity.Property(e => e.Observaaciones)
                    .HasMaxLength(1500)
                    .IsUnicode(false)
                    .HasColumnName("OBSERVAACIONES");

                entity.Property(e => e.ParentescoAval)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PARENTESCO_AVAL");

                entity.Property(e => e.TelefonoAval)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO_AVAL");

                entity.Property(e => e.TelefonoConyuge)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONO_CONYUGE");

                entity.Property(e => e.TieneAval).HasColumnName("TIENE_AVAL");

                entity.Property(e => e.TieneConyuge).HasColumnName("TIENE_CONYUGE");

                entity.Property(e => e.TrabajoAval)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TRABAJO_AVAL");

                entity.Property(e => e.TrabajoConyuge)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TRABAJO_CONYUGE");

                entity.Property(e => e.TrabajoOcupacion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("TRABAJO_OCUPACION");

                entity.Property(e => e.UbicacionReferencia)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("UBICACION_REFERENCIA");

                entity.Property(e => e.UbicacionReferenciaAval)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("UBICACION_REFERENCIA_AVAL");

                entity.Property(e => e.UbicacionReferenciaConyuge)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("UBICACION_REFERENCIA_CONYUGE");

                entity.Property(e => e.UsuarioIngresa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_INGRESA");

                entity.Property(e => e.UsuarioModifica)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICA");

                entity.Property(e => e.Zona)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ZONA");
            });

            modelBuilder.Entity<Cuota>(entity =>
            {
                entity.HasKey(e => e.IdCuota)
                    .HasName("PK__CUOTAS__953592867AAD0FA4");

                entity.ToTable("CUOTAS");

                entity.Property(e => e.IdCuota).HasColumnName("ID_CUOTA");

                entity.Property(e => e.CantidadAbonos).HasColumnName("CANTIDAD_ABONOS");

                entity.Property(e => e.DiasMora).HasColumnName("DIAS_MORA");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION");

                entity.Property(e => e.FechaCuota)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CUOTA");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION");

                entity.Property(e => e.FechaPago)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_PAGO");

                entity.Property(e => e.IdPrestamo).HasColumnName("ID_PRESTAMO");

                entity.Property(e => e.MontoCuota)
                    .HasColumnType("decimal(10, 4)")
                    .HasColumnName("MONTO_CUOTA");

                entity.Property(e => e.Mora)
                    .HasColumnType("decimal(10, 4)")
                    .HasColumnName("MORA");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(2500)
                    .IsUnicode(false)
                    .HasColumnName("OBSERVACIONES");

                entity.Property(e => e.UsuarioIngresa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_INGRESA");

                entity.Property(e => e.UsuarioModifica)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICA");

                entity.Property(e => e.FechaPagoMora)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_PAGO_MORA");

                entity.HasOne(d => d.IdPrestamoNavigation)
                    .WithMany(p => p.Cuota)
                    .HasForeignKey(d => d.IdPrestamo)
                    .HasConstraintName("FK__CUOTAS__ID_PREST__693CA210");
            });

            modelBuilder.Entity<Distrito>(entity =>
            {
                entity.HasKey(e => e.IdDistrito)
                    .HasName("PK__DISTRITO__6F133D496ABFA34C");

                entity.ToTable("DISTRITO");

                entity.Property(e => e.IdDistrito).HasColumnName("ID_DISTRITO");

                entity.Property(e => e.CodigoDepartamento).HasColumnName("CODIGO_DEPARTAMENTO");

                entity.Property(e => e.CodigoDistrito).HasColumnName("CODIGO_DISTRITO");

                entity.Property(e => e.CodigoProvincia).HasColumnName("CODIGO_PROVINCIA");

                entity.Property(e => e.Latitud)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("LATITUD");

                entity.Property(e => e.Longitud)
                    .HasColumnType("decimal(18, 5)")
                    .HasColumnName("LONGITUD");

                entity.Property(e => e.NombreDistrito)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRE_DISTRITO");
            });

            modelBuilder.Entity<Prestamo>(entity =>
            {
                entity.HasKey(e => e.IdPrestamo)
                    .HasName("PK__PRESTAMO__3D5A1E6C130C11E0");

                entity.ToTable("PRESTAMOS");

                entity.Property(e => e.IdPrestamo).HasColumnName("ID_PRESTAMO");

                entity.Property(e => e.Autorizacion)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("AUTORIZACION");

                entity.Property(e => e.Capital)
                    .HasColumnType("decimal(10, 4)")
                    .HasColumnName("CAPITAL");

                entity.Property(e => e.CapitalPendiente)
                    .HasColumnType("decimal(10, 4)")
                    .HasColumnName("CAPITAL_PENDIENTE");

                entity.Property(e => e.CodigoGestor)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_GESTOR");

                entity.Property(e => e.DiaPago)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("DIA_PAGO");

                entity.Property(e => e.DiasDeGracia).HasColumnName("DIAS_DE_GRACIA");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION");

                entity.Property(e => e.FechaEntrega)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_ENTREGA");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION");

                entity.Property(e => e.FechaTermino)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_TERMINO");

                entity.Property(e => e.FondoProvisional)
                    .HasColumnType("decimal(10, 4)")
                    .HasColumnName("FONDO_PROVISIONAL");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.Interes)
                    .HasColumnType("decimal(4, 4)")
                    .HasColumnName("INTERES");

                entity.Property(e => e.Liquidacion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LIQUIDACION");

                entity.Property(e => e.MontoCuota)
                    .HasColumnType("decimal(10, 4)")
                    .HasColumnName("MONTO_CUOTA");

                entity.Property(e => e.NumeroCredito)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NUMERO_CREDITO");

                entity.Property(e => e.NumeroCuotas).HasColumnName("NUMERO_CUOTAS");

                entity.Property(e => e.UsuarioIngresa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_INGRESA");

                entity.Property(e => e.UsuarioModifica)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICA");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Prestamos)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK__PRESTAMOS__ID_CL__6A30C649");
            });

            modelBuilder.Entity<Trabajador>(entity =>
            {
                entity.HasKey(e => e.IdTrabajador)
                    .HasName("PK__TRABAJAD__43E8716C1DA2D547");

                entity.ToTable("TRABAJADOR");

                entity.Property(e => e.IdTrabajador).HasColumnName("ID_TRABAJADOR");

                entity.Property(e => e.Apellidos)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("APELLIDOS");

                entity.Property(e => e.Celular)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CELULAR");

                entity.Property(e => e.CodigoTrabajador)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CODIGO_TRABAJADOR");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_CREACION");

                entity.Property(e => e.FechaModificacion)
                    .HasColumnType("datetime")
                    .HasColumnName("FECHA_MODIFICACION");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("NOMBRES");

                entity.Property(e => e.Pass)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("PASS");

                entity.Property(e => e.TipoTrabajador).HasColumnName("TIPO_TRABAJADOR");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO");

                entity.Property(e => e.UsuarioIngresa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_INGRESA");

                entity.Property(e => e.UsuarioModifica)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USUARIO_MODIFICA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
