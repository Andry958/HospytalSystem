using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Models
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Diagnosis> Diagnosiss { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<PrescribedMedication> PatientMedications { get; set; }
        public DbSet<Disease> Disease { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<Visit>().ToTable("Visits");
            modelBuilder.Entity<Diagnosis>().ToTable("Diagnosis");
            modelBuilder.Entity<Medication>().ToTable("Medication");
            modelBuilder.Entity<PrescribedMedication>().ToTable("PrescribedMedication");
            modelBuilder.Entity<Disease>().ToTable("Disease");
            // Additional configurations can be added here
        }
    }
}
