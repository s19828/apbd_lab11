using LAB_11.Data;
using LAB_11.DTOs;
using LAB_11.Models;
using Microsoft.EntityFrameworkCore;

namespace LAB_11.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<int> AddPrescription(PrescriptionPatientMedicamentDoctorDTO prescription)
    {
        if (await _context.Patients.FindAsync(prescription.Patient.IdPatient) == null)
        {
            await _context.Patients.AddAsync(new Patient()
            {
                IdPatient = prescription.Patient.IdPatient,
                FirstName = prescription.Patient.FirstName,
                LastName = prescription.Patient.LastName,
                Birthdate = prescription.Patient.Birthdate
            });
            
            await _context.SaveChangesAsync();
        }

        foreach (var prescriptionMedicament in prescription.Medicaments)
        {
            if (await _context.Medicaments.FindAsync(prescriptionMedicament.IdMedicament) == null)
            {
                throw new Exception("Medicament not found");
            }
        }

        if (prescription.Medicaments.Count > 10)
        {
            throw new Exception("Too many medicaments");
        }

        if (prescription.Date > prescription.DueDate)
        {
            throw new Exception("Due date must be greater than or equal to date");
        }

        var newPrescription = new Prescription()
        {
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdPatient = prescription.Patient.IdPatient,
            IdDoctor = prescription.Doctor.IdDoctor
        };
        
        await _context.Prescriptions.AddAsync(newPrescription);
        await _context.SaveChangesAsync();

        return newPrescription.IdPrescription;
    }

    public async Task<PatientPrescriptionDTO> GetPatientById(int id)
    {
        if (await _context.Patients.FindAsync(id) == null)
        {
            throw new Exception("Patient not found");
        }

        var patient = await _context.Patients
            .Where(p => p.IdPatient == id)
            .Select(e =>
                new PatientPrescriptionDTO()
                {
                    IdPatient = e.IdPatient,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Birthdate = e.Birthdate,
                    Prescriptions = e.Prescriptions
                        .OrderBy(a => a.DueDate)
                        .Select(a =>
                            new PrescriptionDTO()
                            {
                                IdPrescription = a.IdPrescription,
                                Date = a.Date,
                                DueDate = a.DueDate,
                                Medicaments = a.PrescriptionMedicaments.Select(b =>
                                    new MedicamentDTO()
                                    {
                                        IdMedicament = b.Medicament.IdMedicament,
                                        Name = b.Medicament.Name,
                                        Dose = b.Dose,
                                        Description = b.Medicament.Description
                                    }).ToList(),
                                Doctor = new DoctorDTO()
                                    {
                                        IdDoctor = a.Doctor.IdDoctor,
                                        FirstName = a.Doctor.FirstName,
                                        LastName = a.Doctor.LastName,
                                        Email = a.Doctor.Email
                                    }
                            }).ToList()
                })
            .FirstAsync();

        return patient;
    }
}