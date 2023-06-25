

using EFCoreCodeFirst.Migrations;
using Microsoft.EntityFrameworkCore;

namespace EFCoreCodeFirst.Models
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
        {
        }

        public MainDbContext(DbContextOptions opt)
        : base(opt)
        {
        }

        public DbSet<Doctor> Doctors { get; set; } // IMPORTANT, without this EF won't know that it has to create this as a table for the DB
        public DbSet<Prescription> Prescriptions { get; set; } 
        public DbSet<Patient> Patients { get; set; } 
        public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies(); // need to use this because related data wouldn't load otherwise
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=master;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // defining the constraint and its specific behavior on deletion 
            modelBuilder.Entity<Doctor>(opt =>
            {
                opt.HasMany(d => d.Prescriptions)
                   .WithOne(p => p.Doctor)
                   .HasForeignKey(p => p.IdDoctor)
                   .OnDelete(DeleteBehavior.Cascade);

                opt.HasData(
                    new Doctor { IdDoctor = 1, FirstName = "Doc", LastName = "Tor", Email = "doc1@example.com"},
                    new Doctor { IdDoctor = 2, FirstName = "Tor", LastName = "Doc", Email = "doc2@example.com"}
                    );
            });
            
            modelBuilder.Entity<Patient>(opt =>
            {
                opt.HasMany(d => d.Prescriptions)
                    .WithOne(p => p.Patient)
                    .HasForeignKey(p => p.IdPatient)
                    .OnDelete(DeleteBehavior.Cascade);

                opt.HasData(
                    new Patient { IdPatient = 1, FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1997, 5, 6)},
                    new Patient { IdPatient = 2, FirstName = "Jane", LastName = "Smith", BirthDate = new DateTime(1999, 10, 13) }
                );
            });

            modelBuilder.Entity<Medicament>(opt =>
            {
                opt.HasData(
                    new Medicament { IdMedicament = 1, Name = "Rat poison", Description = "Maybe don't take it", Type = "Non-lethal, for patients anyway"},
                    new Medicament { IdMedicament = 2, Name = "Human poison", Description = "Your choice frankly", Type = "Lethal, for patients anyway" }
                );
            });

            modelBuilder.Entity<Prescription>(opt =>
            {
                opt.HasData(
                    new Prescription { IdPrescription = 1, Date = DateTime.Now, DueDate = DateTime.Now.AddDays(7), IdPatient = 1, IdDoctor = 1 },
                    new Prescription { IdPrescription = 2, Date = DateTime.Now, DueDate = DateTime.Now.AddDays(14), IdPatient = 1, IdDoctor = 1 }
                );
            });

            modelBuilder.Entity<Prescription_Medicament>(opt =>
            {
                opt.HasKey(e => new { e.IdMedicament, e.IdPrescription }).HasName("Prescription_Medicament_pk");

                opt.HasOne(pm => pm.Prescription)
                    .WithMany(p => p.PrescriptionMedicaments)
                    .HasForeignKey(pm => pm.IdPrescription)
                    .OnDelete(DeleteBehavior.Cascade);
                
                opt.HasOne(pm => pm.Medicament)
                    .WithMany(p => p.PrescriptionMedicaments)
                    .HasForeignKey(pm => pm.IdMedicament)
                    .OnDelete(DeleteBehavior.Cascade);
                
                opt.HasData(
                    new Prescription_Medicament { IdMedicament = 1, IdPrescription = 1, Dose = 2, Details = "Take it please" },
                    new Prescription_Medicament { IdMedicament = 2, IdPrescription = 1, Details = "Roll a dice and take that amount. Use D100." }
                );
            });
            
            

            // Creating the relationship from Prescription's "perspective"
            
            //modelBuilder.Entity<Prescription>(opt =>
            //{
            //    opt.HasOne(p => p.Doctor)
            //        .WithMany(d => d.Prescriptions)
            //        .HasForeignKey(p => p.IdDoctor)
            //        .OnDelete(DeleteBehavior.Cascade);
            //    
            //    opt.HasOne(p => p.Patient)
            //        .WithMany(pt => pt.Prescriptions)
            //        .HasForeignKey(p => p.IdPatient)
            //        .OnDelete(DeleteBehavior.Cascade);
            //});

        }

    }
}