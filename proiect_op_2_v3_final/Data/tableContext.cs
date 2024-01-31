using Microsoft.EntityFrameworkCore;
using proiect_op_2_v3_final.Models;

namespace proiect_op_2_v3_final.Data
{
    public class tableContext : DbContext
    {

        public tableContext() { }
        public DbSet<CargoTrailer> CargoTrailers { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Goods> Goodss { get; set; }
        public DbSet<Routes> Routess { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<ModelsRelationTRCT> ModelsRelationsTRCT { get; set; }
        public DbSet<ModelsRelationTRRT> ModelsRelationsTRRT { get; set; }

        public DbSet<User> Users { get; set; }

        public tableContext(DbContextOptions<tableContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=proiect_optional_an2_final;TrustServerCertificate=True;");

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //One-to-One Goods-CargoTrailer
            modelBuilder.Entity<Goods>()
                        .HasOne(gd => gd.CargoTrailer)
                        .WithOne(ct => ct.Goods)
                        .HasForeignKey<CargoTrailer>(ct => ct.GoodsId);

            // One-to-Many City-Driver
            modelBuilder.Entity<City>()
                        .HasMany(ci => ci.Drivers)
                        .WithOne(d => d.City);

            //One-to-One Driver-Truck
            modelBuilder.Entity<Driver>()
                        .HasOne(d => d.Truck)
                        .WithOne(tr => tr.Driver)
                        .HasForeignKey<Truck>(tr => tr.DriverId);

            //many-to-many Truck-CargoTrailer
            modelBuilder.Entity<ModelsRelationTRCT>().HasKey(mrTRCT => new { mrTRCT.CargoTrailerId, mrTRCT.TruckId });

            //One-to-many for many-to-many
            modelBuilder.Entity<ModelsRelationTRCT>()
                        .HasOne(mrTRCT => mrTRCT.Truck)
                        .WithMany(tr => tr.ModelsRelationsTRCT)
                        .HasForeignKey(mrTRCT => mrTRCT.TruckId);

            modelBuilder.Entity<ModelsRelationTRCT>()
                        .HasOne(mrTRCT => mrTRCT.CargoTrailer)
                        .WithMany(ct => ct.ModelsRelationsTRCT)
                        .HasForeignKey(mrTRCT => mrTRCT.CargoTrailerId);

            base.OnModelCreating(modelBuilder);

            //many-to-many Truck-Routes
            modelBuilder.Entity<ModelsRelationTRRT>().HasKey(mrTRRT => new { mrTRRT.RoutesId, mrTRRT.TruckId });

            //One-to-many for many-to-many
            modelBuilder.Entity<ModelsRelationTRRT>()
                        .HasOne(mrTRRT => mrTRRT.Truck)
                        .WithMany(tr => tr.ModelsRelationsTRRT)
                        .HasForeignKey(mrTRRT => mrTRRT.TruckId);

            modelBuilder.Entity<ModelsRelationTRRT>()
                        .HasOne(mrTRRT => mrTRRT.Routes)
                        .WithMany(rt => rt.ModelsRelationsTRRT)
                        .HasForeignKey(mrTRRT=> mrTRRT.RoutesId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
