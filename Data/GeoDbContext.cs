using GeoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GeoApi.Data
{
    public class GeoDbContext : DbContext
    {
        public GeoDbContext(DbContextOptions<GeoDbContext> options) : base(options)
        {
        }

        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração de índices e restrições
            modelBuilder.Entity<State>()
                .HasIndex(s => s.StatePostalCode)
                .IsUnique();

            modelBuilder.Entity<City>()
                .HasIndex(c => new { c.StateId, c.Name })
                .IsUnique();

            modelBuilder.Entity<City>()
                .HasOne(c => c.State)
                .WithMany(s => s.Cities)
                .HasForeignKey(c => c.StateId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 