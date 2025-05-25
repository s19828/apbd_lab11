using LAB_11.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB_11.Data;

public class DatabaseContext : DbContext
{
    //pola
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected DatabaseContext()
    {
        
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(a =>
        {
            a.ToTable("Doctor");

            a.HasKey(e => e.IdDoctor);
            
            a.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            a.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsRequired();
            a.Property(e => e.Email)
                .HasMaxLength(100)
                .IsRequired();
            
            a.HasMany(e => e.Prescriptions)
                .WithOne(e => e.Doctor)
                .HasForeignKey(e => e.IdDoctor)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Patient>(a =>
        {
            a.ToTable("Patient");
            
            a.HasKey(e => e.IdPatient);
            
            a.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            a.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsRequired();
            a.Property(e => e.Birthdate)
                .IsRequired();
            
            a.HasMany(e => e.Prescriptions)
                .WithOne(e => e.Patient)
                .HasForeignKey(e => e.IdPatient)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Medicament>(a =>
        {
            a.ToTable("Medicament");

            a.HasKey(e => e.IdMedicament);

            a.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
            a.Property(e => e.Description)
                .HasMaxLength(100)
                .IsRequired();
            a.Property(e => e.Type)
                .HasMaxLength(100)
                .IsRequired();

            a.HasMany(e => e.PrescriptionMedicaments)
                .WithOne(e => e.Medicament)
                .HasForeignKey(e => e.IdMedicament)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Prescription>(a =>
        {
            a.ToTable("Prescription");

            a.HasKey(e => e.IdPrescription);

            a.Property(e => e.Date)
                .HasColumnType("date")
                .IsRequired();
            a.Property(e => e.DueDate)
                .HasColumnType("date")
                .IsRequired();
        });

        modelBuilder.Entity<PrescriptionMedicament>(a =>
        {
            a.ToTable("Prescription_Medicament");

            a.HasKey(e => new { e.IdPrescription, e.IdMedicament });

            a.Property(e => e.Dose);
            a.Property(e => e.Details)
                .HasMaxLength(100)
                .IsRequired();
        });
        
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>
        {
            new Doctor() { IdDoctor = 1, FirstName = "An", LastName = "Al", Email = "mail1@gmail.com" },
            new Doctor() { IdDoctor = 2, FirstName = "Bn", LastName = "Bl", Email = "mail2@gmail.com" }
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>
        {
            new Patient() { IdPatient = 1, FirstName = "Cn", LastName = "Cl", Birthdate = new DateTime(1999, 01, 01) },
            new Patient() { IdPatient = 2, FirstName = "Dn", LastName = "Dl", Birthdate = new DateTime(2000, 04, 12) },
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>
        {
            new Medicament() { IdMedicament = 1, Name = "M1", Description = "Description1", Type = "Type1"},
            new Medicament() { IdMedicament = 2, Name = "M2", Description = "Description2", Type = "Type2"}
        });

        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>
        {
            new Prescription()
                { IdPrescription = 1, Date = new DateTime(2025, 01, 10), DueDate = new DateTime(2025, 12, 01), IdPatient = 1, IdDoctor = 1 },
            new Prescription()
                { IdPrescription = 2, Date = new DateTime(2025, 01, 15), DueDate = new DateTime(2025, 05, 01), IdPatient = 2, IdDoctor = 2 }
        });

        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>
        {
            new PrescriptionMedicament() { IdMedicament = 1, IdPrescription = 1, Dose = 10, Details = "Details1"},
            new PrescriptionMedicament() { IdMedicament = 2, IdPrescription = 2, Dose = 3, Details = "Details2"}
        });
    }
}