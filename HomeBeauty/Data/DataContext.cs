using HomeBeauty.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeBeauty.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<CareProduct> CareProducts { get; set; }
        public DbSet<Chemicals> Chemicals { get; set; }
        public DbSet<Compound> Compounds { get; set; }
        public DbSet<Cure> Cures { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Illness> Illnesses { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<User> UsersInSystem { get; set; }
        public DbSet<WaterReception> WaterReceptions { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HomeBeauty1;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Allergen>()
                .HasOne(a => a.Chemicals)
                .WithMany(c => c.Allergens)
                .HasForeignKey(a => a.ChemicalId);
            modelBuilder.Entity<Allergen>()
                .HasOne(a => a.User)
                .WithMany(u => u.Allergens)
                .HasForeignKey(a => a.UserId);
            modelBuilder.Entity<Compound>()
                .HasOne(c => c.CareProduct)
                .WithMany(c => c.Compounds)
                .HasForeignKey(c => c.CareProductId);
            modelBuilder.Entity<Compound>()
                .HasOne(c => c.Chemicals)
                .WithMany(c => c.Compounds)
                .HasForeignKey(c => c.ChemicalId);
            modelBuilder.Entity<Cure>()
                .HasOne(c => c.CareProduct)
                .WithMany(c => c.Cures)
                .HasForeignKey(c => c.CareProductId);
            modelBuilder.Entity<Cure>()
                .HasOne(c => c.Treatment)
                .WithMany(t => t.Cures)
                .HasForeignKey(c => c.TreatmentId);
            modelBuilder.Entity<WaterReception>()
                .HasOne(w => w.Device)
                .WithMany(d => d.WaterReceptions)
                .HasForeignKey(w => w.DeviceId);
            modelBuilder.Entity<WaterReception>()
                .HasOne(w => w.User)
                .WithMany(u => u.WaterReceptions)
                .HasForeignKey(w => w.UserId);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Doctors)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
