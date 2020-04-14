using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<SqlHospital> Hospitals { get; set; }
        public DbSet<SqlPatient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configures own-entity mapping
            modelBuilder.Entity<SqlHospital>().OwnsOne(h => h.Address);
            modelBuilder.Entity<SqlPatient>().OwnsOne(p => p.Address);

            // configures one-to-many relationship
            modelBuilder.Entity<SqlHospital>()
                .HasMany(h => h.Patients)
                .WithOne(p => p.Hospital)
                .HasForeignKey(x => x.HospitalId)
                .IsRequired(false);
        }
    }
}