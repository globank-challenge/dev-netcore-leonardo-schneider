using BancoEjercicioApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BancoEjercicioApi.DataAccess
{
    public class AppDbContext : DbContext
    {
        #region Constructor

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }

        #endregion Constructor

        #region Override

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #endregion Override

        #region DbSet Properties

        // Por cada entidad se debe generar una propiedad del tipo DbSet
        DbSet<Persona>? Persona { get; set; }
        DbSet<Cliente>? Cliente { get; set; }
        DbSet<Cuenta>? Cuenta { get; set; }
        DbSet<Movimiento>? Movimiento { get; set; }
        DbSet<ReporteMovimiento> ReporteMovimientos { get; set; }

        #endregion DbSet Properties
    }
}