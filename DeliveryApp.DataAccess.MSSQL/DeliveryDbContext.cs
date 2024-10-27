using Microsoft.EntityFrameworkCore;
using DeliveryApp.Models;
using DeliveryApp.DataAccess.MSSQL.Configurations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace DeliveryApp.DataAccess.MSSQL
{
    public class DeliveryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-5IKF70R\SQLEXPRESS;Database=DeliveryDb;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<DistrictEntity> Districts { get; set; }
        public DbSet<PackageEntity> Packages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DistrictConfiguration());
            modelBuilder.ApplyConfiguration(new PackageConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
