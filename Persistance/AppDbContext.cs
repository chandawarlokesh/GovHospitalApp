using GovHospitalApp.Core.Infrastructure.Persistance.Models;
using Microsoft.EntityFrameworkCore;

namespace GovHospitalApp.Core.Infrastructure.Persistance
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {

        }

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

        public DbSet<SqlHospital> Hospitals { get; set; }
        public DbSet<SqlPatient> Patients { get; set; }
    }
}
